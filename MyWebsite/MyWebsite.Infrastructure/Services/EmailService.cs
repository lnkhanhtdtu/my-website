using Microsoft.Extensions.Configuration;
using MyWebsite.Domain.Settings;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;

namespace MyWebsite.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly string _key;

        public EmailService(IConfiguration configuration)
        {
            var config = configuration.GetSection("EmailBrevo");
            _senderEmail = config["Sender:Email"];
            _senderName = config["Sender:Name"];
            _key = config["Key"] ?? string.Empty;

            if (!sib_api_v3_sdk.Client.Configuration.Default.ApiKey.ContainsKey("api-key"))
            {
                sib_api_v3_sdk.Client.Configuration.Default.ApiKey["api-key"] = _key;
            }
        }

        public async Task<bool> Send(EmailSetting emailSetting)
        {
            try
            {
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
    }
}
