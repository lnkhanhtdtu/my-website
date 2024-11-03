using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Application.DTOs.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Hình ảnh")]
        public byte[]? ImageData { get; set; }

        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }
    }
}
