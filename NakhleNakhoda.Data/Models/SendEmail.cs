using NakhleNakhoda.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models
{
    public class SendEmail : IEmailSender
    {
        private ApplicationDbContext db;
        public SendEmail(ApplicationDbContext _db)
        {
            db = _db;
        }
        
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            
            var qservice = db.tanzimatEmails.FirstOrDefault();

            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(email, "خریدار محترم"));
                message.From = new MailAddress("info@nakhlenakhoda.ir", "نخل ناخدا");
                message.Subject = subject;
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("mail.nakhlenakhoda.ir"))
                {
                    client.Port = 587;
                    client.Credentials = new NetworkCredential("info@nakhlenakhoda.ir", "M.sohrabi1378.ms");
                    client.EnableSsl = false;
                    client.Send(message);
                }
            }


            return Task.FromResult(0);
        }

    }
}
