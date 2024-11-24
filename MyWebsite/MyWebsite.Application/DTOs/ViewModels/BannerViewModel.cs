using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Application.DTOs.ViewModels
{
    public class BannerViewModel

    {
        public int Id { get; set; }

        [Display(Name = "Tiêu đề (*)")]
        public string Title { get; set; }

        [Display(Name = "Mô tả (*)")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        public byte[]? ImageData { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int InOrder { get; set; } = 0;

        [Display(Name = "Hiển thị (*)")]
        public bool IsActive { get; set; } = true;
    }
}
