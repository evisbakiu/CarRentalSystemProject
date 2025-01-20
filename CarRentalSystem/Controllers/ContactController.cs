using CarRentalSystem.Services.Interfaces;
using CarRentalSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailSender _emailSender;

        public ContactController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model); 
            }

            var emailBody = $@"
        <h2>Contact Form Submission</h2>
        <p><strong>Name:</strong> {model.Name}</p>
        <p><strong>Email:</strong> {model.Email}</p>
        <p><strong>Subject:</strong> {model.Subject}</p>
        <p><strong>Message:</strong></p>
        <p>{model.Message}</p>
    ";
            await _emailSender.SendEmailContactAsync(model.Email, model.Subject, emailBody);

            TempData["SuccessMessage"] = "Your message has been sent successfully!";

            return RedirectToAction("Index", "Home");
        }

    }
}
