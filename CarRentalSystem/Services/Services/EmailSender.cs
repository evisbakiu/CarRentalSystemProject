namespace CarRentalSystem.Services.Services
{
    using System.Net;
    using System.Net.Mail;
    using CarRentalSystem.Services.Interfaces;

    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("evis.bakiu12@gmail.com", "lkbumsxvkjywjpvj"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("evis.bakiu12@gmail.com"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendEmailContactAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("evis.bakiu12@gmail.com", "lkbumsxvkjywjpvj"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };

            mailMessage.To.Add("evis.bakiu12@gmail.com");

            await smtpClient.SendMailAsync(mailMessage);
        }
    }


}
