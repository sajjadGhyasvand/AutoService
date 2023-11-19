using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace AutoService.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
           MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp-mail.outlook.com");
            mail.From = new MailAddress("sajjadgh1995@hotmail.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            mail.IsBodyHtml = true;

            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("sajjadgh1995@hotmail.com", "sajjad8910");
            smtpServer.EnableSsl = true;

            smtpServer.Send(mail);
            return Task.CompletedTask;
        }
    }
}
