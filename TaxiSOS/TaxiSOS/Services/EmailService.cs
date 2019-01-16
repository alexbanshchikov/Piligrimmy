using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace TaxiSOS.Services
{
    public class EmailService
    {
        public void SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Такси TaxiSOS", "banshchikov.alex@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("banshchikov.alex@yandex.ru", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                 client.ConnectAsync("smtp.yandex.ru", 25, false);
                 client.AuthenticateAsync("login@yandex.ru", "password");
                 client.SendAsync(emailMessage);

                 client.DisconnectAsync(true);
            }
        }
    }
}
