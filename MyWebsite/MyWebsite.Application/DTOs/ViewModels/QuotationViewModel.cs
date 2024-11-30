using MyWebsite.Domain.Entities;
using MyWebsite.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Application.DTOs.ViewModels
{
    public class QuotationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tên khách hàng (*)")]
        public string CustomerName { get; set; }

        [Display(Name = "Email")]
        public string? CustomerEmail { get; set; }

        [Display(Name = "SĐT (*)")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public int? ProductId { get; set; }

        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Trạng thái")]
        public QuotationStatus? Status { get; set; } = QuotationStatus.Pending;

        [Display(Name = "Ngày phản hồi")]
        public DateTime? ResponseDate { get; set; }

        public virtual Product? Product { get; set; }
    }
}
