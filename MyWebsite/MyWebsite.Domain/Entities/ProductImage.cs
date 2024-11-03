using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities;

[Table("ProductImages", Schema = "dbo")]
public class ProductImage : BaseEntity
{
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [ForeignKey("Image")]
    public int ImageId { get; set; }

    public virtual Product? Product { get; set; }
    public virtual Image? Image { get; set; }
}