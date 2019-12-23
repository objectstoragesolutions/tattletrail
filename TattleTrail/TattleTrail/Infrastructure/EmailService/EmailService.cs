using MimeKit;
using System;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TattleTrail.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using TattleTrail.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TattleTrail.Infrastructure.EmailService {
    public class EmailService : IEmailService {
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;
        private readonly SmtpDataModel _smtpCredentials;
        private IWebHostEnvironment _currentEnvironment;

        public EmailService(ILogger<EmailService> logger, IConfiguration configuration, IWebHostEnvironment env) {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<EmailService>));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
            _currentEnvironment = env ?? throw new ArgumentNullException(nameof(IWebHostEnvironment));

            _smtpCredentials = new SmtpDataModel {
                EmailName = _configuration["SMTP_EMAIL_NAME"],
                EmailAddress = _configuration["SMTP_EMAIL_ADDRESS"],
                Client = _configuration["SMTP_CLIENT"],
                Port = Int32.Parse(_configuration["SMTP_PORT"]),
                Login = _configuration["SMTP_LOGIN"],
                Password = _configuration["SMTP_PASS"]
            } ;
        }

        public async Task SendEmailAsync(String[] subscribers, String subject, String message) {
            try {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_smtpCredentials.EmailName, _smtpCredentials.EmailAddress));
                emailMessage.To.AddRange(subscribers.ToMailBoxArray());
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                    Text = message
                };

                using (var client = new SmtpClient()) {
                    await client.ConnectAsync(_smtpCredentials.Client, _smtpCredentials.Port, false);
                    await client.AuthenticateAsync(_smtpCredentials.Login, _smtpCredentials.Password);
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
