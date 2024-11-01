using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("Categories", Schema = "dbo")]
    public class Category : BaseEntity
    {
        public byte[]? ImageData { get; set; }

        public string CategoryName { get; set; }

        // public ICollection<Product>? Products { get; set; }
    }
}