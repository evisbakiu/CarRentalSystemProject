using CarRentalSystem.Models;
using CarRentalSystem.Services.Interfaces;
using CarRentalSystem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Encodings.Web;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IEmailSender _emailSender;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                DateOfBirth = model.DateOfBirth
            };

            // Attempt to create the user
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Generate email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { userId = user.Id, token = token }, Request.Scheme);

                // Send confirmation email
                await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.");

                TempData["Message"] = "Registration successful. Please confirm your email.";
                return RedirectToAction("EmailConfirmation");
            }

            // Add errors to ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        // If validation fails, redisplay the form
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (userId == null || token == null)
            return BadRequest("Invalid email confirmation request.");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found.");

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
            return View("ConfirmEmail");

        return BadRequest("Email confirmation failed.");
    }

    [HttpGet]
    public async Task<IActionResult> ResendConfirmationEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                new { userId = user.Id, token = token }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

            TempData["Message"] = "A new confirmation email has been sent.";
        }

        return RedirectToAction("Login");
    }


    [HttpGet]
    public IActionResult EmailConfirmation()
    {
        return View();
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "Your email has not been confirmed. Please check your inbox.");
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

                if (result.Succeeded)
                {
                    // Ruaj emrin në session për përdorim në View ose Controller
                    HttpContext.Session.SetString("FullName", user.FullName);

                    // Shtojmë claim FullName nëse nuk ekziston (opsionale, por e rekomanduar)
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (!claims.Any(c => c.Type == ClaimTypes.Name))
                    {
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.FullName));
                    }

                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }


                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }

        return View();
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        HttpContext.Session.Clear();

        Response.Cookies.Delete("WelcomeName");

        return RedirectToAction("Index", "Home");
    }


    [HttpPost]
    public IActionResult ExternalLogin(string provider, string? returnUrl = null)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
    {
        returnUrl ??= Url.Content("~/");

        if (remoteError != null)
        {
            ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
            return RedirectToAction("Login");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            Console.WriteLine("info is null");
            return RedirectToAction("Login");
        }

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (result.Succeeded)
        {
            Console.WriteLine("Logged in successfully");

            var existingUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (existingUser != null)
            {
                // Ruaj FullName në session
                HttpContext.Session.SetString("FullName", existingUser.FullName ?? "");

                // Shto Claim nëse nuk ekziston
                var claims = await _userManager.GetClaimsAsync(existingUser);
                if (!claims.Any(c => c.Type == ClaimTypes.Name))
                {
                    await _userManager.AddClaimAsync(existingUser, new Claim(ClaimTypes.Name, existingUser.FullName ?? ""));
                }
            }

            return RedirectToAction("Index", "Home");
        }

        // MERR TË DHËNAT NGA GOOGLE
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var name = info.Principal.FindFirstValue(ClaimTypes.Name);
        Console.WriteLine("Email nga Google: " + email);

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new User
            {
                UserName = email,
                Email = email,
                FullName = name,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                Console.WriteLine("Krijimi deshtoi:");
                foreach (var error in createResult.Errors)
                    Console.WriteLine(error.Description);

                return RedirectToAction("Login");
            }

            Console.WriteLine("User u krijua me sukses");
        }

        // LIDH GOOGLE
        var loginResult = await _userManager.AddLoginAsync(user, info);
        if (!loginResult.Succeeded)
        {
            Console.WriteLine("AddLoginAsync deshtoi");
            foreach (var error in loginResult.Errors)
                Console.WriteLine(error.Description);

            return RedirectToAction("Login");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        Console.WriteLine("User u logua me sukses");

        // Ruaj FullName në session
        HttpContext.Session.SetString("FullName", user.FullName ?? "");

        // Shto Claim për emrin nëse nuk ekziston
        var userClaims = await _userManager.GetClaimsAsync(user);
        if (!userClaims.Any(c => c.Type == ClaimTypes.Name))
        {
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.FullName ?? ""));
        }

        return RedirectToAction("Index", "Home");
    }




}
