namespace MyWebsite.Application.DTOs.Categories
{
    public class CategorySiteDTO
    {
        public int Id { get; set; }

        public byte[]? ImageData { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int TotalBooks { get; set; }
    }
}
