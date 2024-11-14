using MyWebsite.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("Quotations", Schema = "dbo")]
    public class Quotation : BaseEntity
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public string? Content { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public QuotationStatus? Status { get; set; } = QuotationStatus.Pending;

        public DateTime? ResponseDate { get; set; }

        public virtual Product? Product { get; set; }
    }
}
