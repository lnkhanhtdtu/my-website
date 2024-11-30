using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities;

[Table("ProductAttributes", Schema = "dbo")]
public class ProductAttribute : BaseEntity
{
    public string Name { get; set; }
}