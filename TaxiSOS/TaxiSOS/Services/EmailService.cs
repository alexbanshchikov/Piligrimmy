using MimeKit;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace TaxiSOS.Services
{
    public class EmailService
    {
        public void SendEmailAsync(string email, string subject, string message)
        {

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("Giruch233280@gmail.com", "TaxiSOS");
            // кому отправляем
            MailAddress to = new MailAddress("Giruch@inbox.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = subject;
            // текст письма
            m.Body = message;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("Giruch233280@gmail.com", "122170fyz");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
