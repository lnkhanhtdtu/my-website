using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities;

[Table("ProductAttributeValues", Schema = "dbo")]
public class ProductAttributeValue : BaseEntity
{
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [ForeignKey("ProductAttribute")]
    public int AttributeId { get; set; }

    public string Value { get; set; }

    public virtual Product? Product { get; set; }
    public virtual ProductAttribute? ProductAttribute { get; set; }
}