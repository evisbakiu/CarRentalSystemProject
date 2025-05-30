﻿using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using Stripe.Checkout;
using System.Globalization;
using System.Text;


public class PaymentController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    private readonly UserManager<User> _userManager;
    private readonly PayPalHttpClient _payPalClient;

    public PaymentController(ApplicationDbContext context, IConfiguration config, UserManager<User> userManager)
    {
        _context = context;
        _config = config;
        _userManager = userManager;

        var clientId = _config["PayPal:ClientId"];
        var clientSecret = _config["PayPal:ClientSecret"];
        var env = _config["PayPal:Environment"] ?? "sandbox";

        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
        {
            throw new Exception("PayPal credentials are not configured properly.");
        }

        _payPalClient = PayPalClient.Client(clientId, clientSecret, env);

        Stripe.StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
    }

    [HttpPost]
    public IActionResult StartStripePayment(Guid carId, DateTime pickUpDateTime, DateTime returnDateTime, decimal totalCost)
    {
        var domain = $"{Request.Scheme}://{Request.Host}";

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = totalCost * 100,
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Car Rental Reservation"
                        }
                    },
                    Quantity = 1,
                }
            },
            Mode = "payment",
            SuccessUrl = $"{domain}/Payment/PaymentSuccess?carId={carId}&pickup={pickUpDateTime:s}&returndate={returnDateTime:s}&method=Stripe",
            CancelUrl = $"{domain}/Payment/PaymentFailed?carId={carId}&pickup={pickUpDateTime:s}&returndate={returnDateTime:s}"
        };

        var service = new SessionService();
        var session = service.Create(options);

        return Redirect(session.Url);
    }

    [HttpPost]
    public async Task<IActionResult> StartPayPalPayment(Guid carId, DateTime pickUpDateTime, DateTime returnDateTime, decimal totalCost)
    {
        var domain = $"{Request.Scheme}://{Request.Host}";

        var orderRequest = new OrderRequest
        {
            CheckoutPaymentIntent = "CAPTURE",
            PurchaseUnits = new List<PurchaseUnitRequest>
        {
            new PurchaseUnitRequest
            {
                ReferenceId = carId.ToString(),
                Description = "Car Rental Reservation",
                AmountWithBreakdown = new AmountWithBreakdown
                {
                    CurrencyCode = "EUR",
                    Value = totalCost.ToString("F2", CultureInfo.InvariantCulture)
                }
            }
        },
            ApplicationContext = new ApplicationContext
            {
                ReturnUrl = $"{domain}/Payment/PayPalSuccess?carId={carId}&pickup={pickUpDateTime:s}&returndate={returnDateTime:s}",
                CancelUrl = $"{domain}/Payment/PaymentFailed?carId={carId}&pickup={pickUpDateTime:s}&returndate={returnDateTime:s}"
            }
        };

        var request = new OrdersCreateRequest();
        request.Prefer("return=representation");
        request.RequestBody(orderRequest);

        try
        {
            var response = await _payPalClient.Execute(request);
            var result = response.Result<Order>();

            var approvalLink = result.Links.FirstOrDefault(x => x.Rel == "approve")?.Href;

            if (!string.IsNullOrEmpty(approvalLink))
            {
                return Redirect(approvalLink);
            }

            TempData["Error"] = "Could not get PayPal approval link.";
            return RedirectToAction("PaymentFailed", new { carId, pickup = pickUpDateTime, returndate = returnDateTime });
        }
        catch (PayPalHttp.HttpException ex)
        {
            var statusCode = ex.StatusCode;
            var errorMessage = ex.Message; 

            Console.WriteLine($"PayPal HTTP Error {statusCode}:");
            Console.WriteLine(errorMessage);

            TempData["Error"] = $"PayPal API Error ({statusCode}): {errorMessage}";
            return RedirectToAction("PaymentFailed", new { carId, pickup = pickUpDateTime, returndate = returnDateTime });
        }

        catch (Exception ex)
        {
            TempData["Error"] = $"Unexpected PayPal error: {ex.Message}";
            return RedirectToAction("PaymentFailed", new { carId, pickup = pickUpDateTime, returndate = returnDateTime });
        }
    }


    public async Task<IActionResult> PayPalSuccess(Guid carId, DateTime pickup, DateTime returndate, string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            TempData["Error"] = "Invalid PayPal payment token.";
            return RedirectToAction("PaymentFailed", new { carId, pickup, returndate });
        }

        var request = new OrdersCaptureRequest(token);
        request.RequestBody(new OrderActionRequest());

        try
        {
            Console.WriteLine($"[PayPal Capture] Token = {token}");

            var response = await _payPalClient.Execute(request);
            var result = response.Result<Order>();

            if (result.Status == "COMPLETED")
            {
                var car = await _context.Car.FindAsync(carId);
                var user = await _userManager.GetUserAsync(User);

                if (car == null || !car.IsAvailable || user == null)
                {
                    TempData["Error"] = "Unable to complete reservation.";
                    return RedirectToAction("ReviewReservation", "Reservation", new { carId, pickUpDateTime = pickup, returnDateTime = returndate });
                }

                var reservation = new Reservation
                {
                    CarId = carId,
                    UserId = user.Id,
                    StartDate = pickup,
                    EndDate = returndate,
                    TotalCost = (returndate - pickup).Days * car.PricePerDay,
                    StatusId = _context.Status.First(s => s.Name == "Confirmed").Id
                };

                car.IsAvailable = false;
                _context.Reservation.Add(reservation);
                await _context.SaveChangesAsync();

                var payment = new Payment
                {
                    ReservationId = reservation.Id,
                    Amount = (double)reservation.TotalCost,
                    PaymentDate = DateTime.UtcNow,
                };
                _context.Payment.Add(payment);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Reservation completed via PayPal!";
                return RedirectToAction("MyBookings", "User");
            }

            TempData["Error"] = $"PayPal returned unexpected status: {result.Status}";
            return RedirectToAction("PaymentFailed", new { carId, pickup, returndate });
        }
        catch (PayPalHttp.HttpException ex)
        {
            var message = ex.Message;
            TempData["Error"] = $"PayPal capture API error: {message}";
            Console.WriteLine("💥 PayPal Capture Error:");
            Console.WriteLine(message);
            return RedirectToAction("PaymentFailed", new { carId, pickup, returndate });
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Unexpected capture error: {ex.Message}";
            return RedirectToAction("PaymentFailed", new { carId, pickup, returndate });
        }
    }

    public IActionResult PaymentFailed(Guid carId, DateTime pickup, DateTime returndate)
    {
        TempData["Error"] = "Payment was cancelled or failed. Please try again.";
        return RedirectToAction("ReviewReservation", "Reservation", new { carId, pickUpDateTime = pickup, returnDateTime = returndate });
    }

    public async Task<IActionResult> PaymentSuccess(Guid carId, DateTime pickup, DateTime returndate, string method)
    {
        var car = await _context.Car.FindAsync(carId);
        var user = await _userManager.GetUserAsync(User);

        if (car == null || !car.IsAvailable || user == null)
        {
            TempData["Error"] = "Unable to complete reservation.";
            return RedirectToAction("ReviewReservation", "Reservation", new { carId, pickUpDateTime = pickup, returnDateTime = returndate });
        }

        var reservation = new Reservation
        {
            CarId = carId,
            UserId = user.Id,
            StartDate = pickup,
            EndDate = returndate,
            TotalCost = (returndate - pickup).Days * car.PricePerDay,
            StatusId = _context.Status.First(s => s.Name == "Confirmed").Id
        };

        car.IsAvailable = false;
        _context.Reservation.Add(reservation);
        await _context.SaveChangesAsync();

        var payment = new Payment
        {
            ReservationId = reservation.Id,
            Amount = (double)reservation.TotalCost,
            PaymentDate = DateTime.UtcNow,
        };
        _context.Payment.Add(payment);
        await _context.SaveChangesAsync();

        TempData["Success"] = $"Reservation completed via {method}!";
        return RedirectToAction("MyBookings", "User");
    }
}

