using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("Banners", Schema = "dbo")]
    public class Banner : BaseEntity
    {
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        public byte[] ImageData { get; set; }

        [Display(Name = "Thứ tự hiển thị")] 
        public int InOrder { get; set; } = 0;

        [Display(Name = "Hiển thị")] 
        public bool IsActive { get; set; } = true;
    }
}