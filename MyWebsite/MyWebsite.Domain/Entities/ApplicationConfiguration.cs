using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities
{
    [Table("ApplicationConfigurations", Schema = "dbo")]
    public class ApplicationConfiguration : BaseEntity
    {
        public bool EnableQuotationNotification { get; set; } = false;
    }
}
