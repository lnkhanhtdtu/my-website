using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("Images", Schema = "dbo")]
    public class Image : BaseEntity
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }
    }
}