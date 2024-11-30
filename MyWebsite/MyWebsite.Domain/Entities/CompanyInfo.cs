using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("CompanyInfo", Schema = "dbo")]
    public class CompanyInfo : BaseEntity
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public byte[]? Logo { get; set; }

        public string? BusinessField { get; set; }

        public string? TaxCode { get; set; }

        public string? FoundationYear { get; set; }

        public string? HeadquartersAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public string? ZaloOaId { get; set; }

        public string? ZaloNumber { get; set; }

        public string? WhatsAppNumber { get; set; }

        public string? ManagedBy { get; set; }

        public string? TypeOfBusiness { get; set; }
    }
}