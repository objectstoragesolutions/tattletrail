using System;
using System.Threading.Tasks;

namespace TattleTrail.Infrastructure.EmailService {
    public interface IEmailService {
        Task SendEmailAsync(String[] subscribers, String subject, String message);
    }
}