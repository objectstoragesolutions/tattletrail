using MimeKit;
using System;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TattleTrail.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;

namespace TattleTrail.Infrastructure.EmailService {
    public class EmailService : IEmailService {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration) {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<EmailService>));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
        }

        public async Task SendEmailAsync(String[] subscribers, String subject, String message) {
            try {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_configuration["SMTP_EMAIL_NAME"], _configuration["SMTP_EMAIL_ADDRESS"]));
                emailMessage.To.AddRange(subscribers.ToMailBoxArray());
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                    Text = message
                };

                using (var client = new SmtpClient()) {
                    await client.ConnectAsync(_configuration["SMTP_CLIENT"], Int32.Parse(_configuration["SMTP_PORT"]), false);
                    await client.AuthenticateAsync(_configuration["SMTP_LOGIN"], _configuration["SMTP_PASS"]);
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
