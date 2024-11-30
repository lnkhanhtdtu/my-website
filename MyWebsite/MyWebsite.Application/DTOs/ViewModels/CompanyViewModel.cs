using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Application.DTOs.ViewModels
{
    public class CompanyViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tên công ty (*)")]
        public string Name { get; set; }

        [Display(Name = "Tên viết tắt (*)")]
        public string ShortName { get; set; }

        [Display(Name = "Logo")]
        public byte[]? Logo { get; set; }

        [Display(Name = "Lĩnh vực hoạt động")]
        public string? BusinessField { get; set; }

        [Display(Name = "Mã số thuế")]
        public string? TaxCode { get; set; }

        [Display(Name = "Năm thành lập")]
        public string? FoundationYear { get; set; }

        [Display(Name = "Địa chỉ trụ sở chính")]
        public string? HeadquartersAddress { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ email")]
        public string? Email { get; set; }

        [Display(Name = "Website")]
        public string? Website { get; set; }

        [Display(Name = "Zalo OA Id")]
        public string? ZaloOaId { get; set; }

        [Display(Name = "Số Zalo")]
        public string? ZaloNumber { get; set; }

        [Display(Name = "Số WhatsApp")]
        public string? WhatsAppNumber { get; set; }

        [Display(Name = "Quản lý bởi")]
        public string? ManagedBy { get; set; }

        [Display(Name = "Loại hình doanh nghiệp")]
        public string? TypeOfBusiness { get; set; }
    }
}
