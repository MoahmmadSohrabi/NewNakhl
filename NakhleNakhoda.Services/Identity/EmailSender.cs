﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace NakhleNakhoda.Services.Identity
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
