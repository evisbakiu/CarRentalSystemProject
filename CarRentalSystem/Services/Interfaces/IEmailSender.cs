﻿namespace CarRentalSystem.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailContactAsync(string email, string subject, string message);
    }
}
