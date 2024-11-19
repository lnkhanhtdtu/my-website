using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("CompanyInfo", Schema = "dbo")]
    public class CompanyInfo : BaseEntity
    {
        [Display(Name = "Tên công ty")]
        public string Name { get; set; }

        [Display(Name = "Tên viết tắt")]
        public string ShortName { get; set; }

        [Display(Name = "Logo")]
        public byte[]? Logo { get; set; }

        [Display(Name = "Lĩnh vực hoạt động")]
        public string? BusinessField { get; set; }

        [Display(Name = "Slogan")]
        public string? Slogan { get; set; }

        [Display(Name = "Mã số thuế")]
        public string? TaxCode { get; set; }

        [Display(Name = "Năm thành lập")]
        public int? FoundationYear { get; set; }

        [Display(Name = "Địa chỉ trụ sở chính")]
        public string? HeadquartersAddress { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Địa chỉ email")]
        public string? Email { get; set; }

        [Display(Name = "Website")]
        public string? Website { get; set; }
    }
}