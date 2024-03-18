using UniMagContributions.Models;

namespace UniMagContributions.Services.Interface
{
    public interface IEmailService
    {
        string SendEmail(Message message);
        Task<string> SendEmailAsync(Message message);
    }
}
