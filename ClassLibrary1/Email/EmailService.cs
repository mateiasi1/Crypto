using System.Net;
using System.Net.Mail;
using WebApplication17.Models;

namespace WebApplication17.Email
{
    public class EmailService
    {
        public void SendEmail(EmailModel emailModel)
        {
            var credentials = new NetworkCredential("dragosmateiasi@gmail.com", "Pghelper1.");
            var mail = new MailMessage()
            {
                From = new MailAddress("noreplay@test.com"),
                Subject = emailModel.Subject,
                Body = emailModel.Message

            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(emailModel.EmailTo));
            mail.From = new MailAddress("noreplay@cryptoapp.ro");
            var client = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };
            client.Send(mail);
        }
    }
}
