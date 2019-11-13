using System.Threading.Tasks;

namespace TattleTrail.Infrastructure.EmailService {
    public interface IEmailService {
        Task SendEmailAsync(string subscribers, string subject, string message);
    }
}