using MyWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Application.DTOs.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Loại sản phẩm")]
        public int? CategoryId { get; set; }

        [Display(Name = "Hình ảnh")]
        public byte[]? ImageData { get; set; }

        [Display(Name = "Danh sách hình ảnh")]
        public List<Image>? Images { get; set; }

        [Display(Name = "Sản phẩm nổi bật")]
        public bool? IsFeatured { get; set; }

        [Display(Name = "Giá")]
        public double? Price { get; set; }

        [Display(Name = "Giá cũ")]
        public double? OldPrice { get; set; }
    }
}
