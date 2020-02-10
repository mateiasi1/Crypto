using System.Net;
using System.Net.Mail;
using WebApplication17.Models;

namespace WebApplication17.Email
{
    public class EmailService
    {
        public void SendEmail(EmailModel emailModel)
        {
            string baseURL = "http://localhost:4200/setPassword";
            var credentials = new NetworkCredential("dragosmateiasi@gmail.com", "Pghelper1.");
            string body = emailModel.Message;
            body = body.Replace("{name}", emailModel.Username);
            body = body.Replace("{baseURL}", baseURL);
            body = body.Replace("{id}", emailModel.UserId.ToString());

            var mail = new MailMessage()
            {
                From = new MailAddress("noreplay@test.com"),
                Subject = emailModel.Subject,
                Body = body

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
