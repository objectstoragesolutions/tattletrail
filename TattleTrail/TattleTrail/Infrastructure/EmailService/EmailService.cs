using MimeKit;
using System;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace TattleTrail.Infrastructure.EmailService {
    public class EmailService : IEmailService {
        public async Task SendEmailAsync(String[] subscribers, String subject, String message) {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin", "admin@tattletrail.com"));
            emailMessage.To.Add(new MailboxAddress(subscribers[0]));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                Text = message
            };

            using (var client = new SmtpClient()) {
                await client.ConnectAsync("smtp.gmail.com", 587);
                await client.AuthenticateAsync("email", "password");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
