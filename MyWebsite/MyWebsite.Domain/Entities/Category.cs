using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("Categories", Schema = "dbo")]
    public class Category : BaseEntity
    {
        [Display(Name = "Hình ảnh")]
        public byte[]? ImageData { get; set; }

        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [ForeignKey("ParentCategory")]
        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }
        
        public virtual Category? ParentCategory { get; set; }

        public virtual ICollection<Category>? ChildCategories { get; set; }
        // public ICollection<Product>? Products { get; set; }
    }
}
