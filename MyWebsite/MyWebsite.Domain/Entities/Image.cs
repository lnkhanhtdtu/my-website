using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("Images", Schema = "dbo")]
    public class Image : BaseEntity
    {
        public string? ImageName { get; set; }

        public byte[]? ImageData { get; set; }

        [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}