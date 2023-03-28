using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models
{
    public  class SendEmail2
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.info@nakhlenakhoda.ir");
            mail.From = new MailAddress("info@nakhlenakhoda.ir", "نخل ناخدا");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@nakhlenakhoda.ir", "M.sohrabi1378.ms");

            SmtpServer.Send(mail);

        }
    }
}
