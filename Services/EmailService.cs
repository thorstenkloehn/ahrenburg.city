using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace ahrenburg.city.Services
{
    public class EmailService
    {
        public async Task SendMailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ahrenburg.city", "noreply@deinedomain.de"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync("localhost", 25, false); // Postfix auf localhost
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
