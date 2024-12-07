using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyWebsite.Domain.Settings;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using Task = System.Threading.Tasks.Task;

namespace MyWebsite.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly MyWebsiteContext _context;
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly string _key;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration, MyWebsiteContext context)
        {
            // SMTP
            _configuration = configuration;

            // Bravo
            _context = context;
            var config = configuration.GetSection("EmailBrevo");
            _senderEmail = config["Sender:Email"];
            _senderName = config["Sender:Name"];
            _key = config["Key"] ?? string.Empty;

            if (!sib_api_v3_sdk.Client.Configuration.Default.ApiKey.ContainsKey("api-key"))
            {
                sib_api_v3_sdk.Client.Configuration.Default.ApiKey["api-key"] = _key;
            }
        }

        /// <summary>
        /// Gửi mail với Bravo
        /// </summary>
        /// <param name="emailSetting"></param>
        /// <returns></returns>
        public async Task<bool> Send(EmailSetting emailSetting)
        {
            try
            {
                var appConfig = await _context.ApplicationConfigurations.FirstOrDefaultAsync();
                if (appConfig != null && appConfig.EnableQuotationNotification != true)
                {
                    return false;
                }

                var transaction = new TransactionalEmailsApi();

                var sender = new SendSmtpEmailSender(_senderName, _senderEmail);
                var to = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(emailSetting.To, emailSetting.Name) };

                string body = emailSetting.Content;

                List<SendSmtpEmailCc> lsCc = null;
                if (emailSetting.CC.Any())
                {
                    lsCc = new List<SendSmtpEmailCc>();

                    foreach (var cc in emailSetting.CC)
                    {
                        lsCc.Add(new SendSmtpEmailCc { Email = cc });
                    }
                }

                List<SendSmtpEmailAttachment> lsAttachments = null;

                if (emailSetting.AttachmentFiles.Any())
                {
                    lsAttachments = new List<SendSmtpEmailAttachment>();

                    foreach (var file in emailSetting.AttachmentFiles)
                    {
                        lsAttachments.Add(new SendSmtpEmailAttachment { Url = file });
                    }
                }

                var sendEmail = new SendSmtpEmail(sender, to, null, lsCc, body, null, emailSetting.Subject, null, lsAttachments);

                CreateSmtpEmail result = await transaction.SendTransacEmailAsync(sendEmail);

                return result is not null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Gửi email với SMTP
        /// </summary>
        public async Task SendEmailSmtpAsync(EmailSetting emailSetting)
        {
            var appConfig = await _context.ApplicationConfigurations.FirstOrDefaultAsync();
            if (appConfig != null && appConfig.EnableQuotationNotification != true)
            {
                return;
            }

            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"] ?? string.Empty);
            var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
            var smtpPassword = _configuration["EmailSettings:SmtpPassword"];
            var senderEmail = _configuration["EmailSettings:SenderEmail"] ?? "";
            var receiverEmail = _configuration["EmailSettings:ReceiverEmail"] ?? "";
            var senderName = _configuration["EmailSettings:SenderName"];

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, senderName);
                mail.To.Add(receiverEmail);
                mail.Subject = emailSetting.Subject;
                mail.Body = emailSetting.Content;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                NetworkCredential credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtp.Credentials = credentials;
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
