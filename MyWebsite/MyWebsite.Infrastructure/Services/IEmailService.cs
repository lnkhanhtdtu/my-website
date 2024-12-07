using MyWebsite.Domain.Settings;

namespace MyWebsite.Infrastructure.Services
{
    public interface IEmailService
    {
        Task<bool> Send(EmailSetting emailSetting);
        Task SendEmailSmtpAsync(EmailSetting emailSetting);
    }
}
