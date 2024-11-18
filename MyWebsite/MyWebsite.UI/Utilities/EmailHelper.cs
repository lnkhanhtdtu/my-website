using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Settings;

namespace MyWebsite.UI.Utilities
{
    public static class EmailHelper
    {
        public static EmailSetting EmailTemplateForQuotationNotification(QuotationViewModel quotationViewModel)
        {
            var subject = $"Yêu cầu tư vấn báo giá mới từ {quotationViewModel.CustomerName}";
            var content = $"<h2>Có yêu cầu tư vấn báo giá mới</h2>" +
                          $"<p><strong>Tên khách hàng:</strong> {quotationViewModel.CustomerName}</p>" +
                          $"<p><strong>Email:</strong> {quotationViewModel.CustomerEmail}</p>" +
                          $"<p><strong>Số điện thoại:</strong> {quotationViewModel.CustomerPhone}</p>" +
                          $"<p><strong>Nội dung:</strong> {quotationViewModel.Content}</p>";

            var emailInfo = new EmailSetting()
            {
                Name = "Hỗ trợ",
                To = "lnkhanhtdtu@gmail.com",
                Subject = subject,
                Content = content
            };

            return emailInfo;
        }
    }
}