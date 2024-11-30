using MyWebsite.Domain.Entities;
using MyWebsite.Domain.Enums;

namespace MyWebsite.Application.DTOs
{
    public class QuotationDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public int? ProductId { get; set; }
        public string? Content { get; set; }
        public QuotationStatus? Status { get; set; }
        public DateTime? ResponseDate { get; set; }
        public virtual Product? Product { get; set; }
    }
}
