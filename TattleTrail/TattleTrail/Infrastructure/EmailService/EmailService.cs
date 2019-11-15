using MimeKit;
using System;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TattleTrail.Infrastructure.EmailService {
    public class EmailService : IEmailService {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<EmailService>));
        }

        public async Task SendEmailAsync(String[] subscribers, String subject, String message) {
            try {
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
            } catch(Exception ex) {
                _logger.LogError($"Something went wrong inside EmailService -> SendEmailAsync: {ex.Message}");
                throw;
            }
        }
    }
}
