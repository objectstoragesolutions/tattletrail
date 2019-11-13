using MimeKit;
using System;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace TattleTrail.Infrastructure.EmailService {
    public class EmailService : IEmailService {
        public async Task SendEmailAsync(String subscribers, string subject, string message) {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Warning!", "dmbo@opinov8.ru"));
            emailMessage.To.Add(new MailboxAddress($"{subscribers}"));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                Text = message
            };

            using (var client = new SmtpClient()) {
                await client.ConnectAsync("", 25, false);
                await client.AuthenticateAsync("", "");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
