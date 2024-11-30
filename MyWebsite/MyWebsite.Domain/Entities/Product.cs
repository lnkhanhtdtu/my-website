using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Domain.Entities;

// TODO: Lưu nhiều thuộc tính của sản phẩm: https://viblo.asia/c/EvbLbqKmJnk

[Table("Products", Schema = "dbo")]
public class Product : BaseEntity
{
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; }

    [Display(Name = "Mô tả tóm tắt")]
    public string Summary { get; set; }

    [Display(Name = "Mô tả")]
    public string Description { get; set; }

    [ForeignKey("Category")]
    [Display(Name = "Loại sản phẩm")]
    public int? CategoryId { get; set; }

    [Display(Name = "Hình ảnh")]
    public byte[]? ImageData { get; set; }

    [Display(Name = "Danh sách hình ảnh")]
    public List<Image>? Images { get; set; }

    [Display(Name = "Sản phẩm nổi bật")]
    public bool? IsFeatured { get; set; }

    [Display(Name = "Giá")]
    public double? Price { get; set; }

    [Display(Name = "Giá cũ")]
    public double? OldPrice { get; set; }

    public virtual Category? Category { get; set; }

    // TODO: Chuyển sang Attribute
    //[Display(Name = "Kích thước")]
    //public string? Size { get; set; }

    //[Display(Name = "Chất liệu")]
    //public string? Material { get; set; }

    //[Display(Name = "Công nghệ in")]
    //public string? PrintingTechnology { get; set; }

    //[Display(Name = "Màu sắc")]
    //public string? Color { get; set; }

    //[Display(Name = "Ứng dụng")]
    //public string? Application { get; set; }

    //[Display(Name = "Nguồn gốc")]
    //public string? Origin { get; set; }

    //[Display(Name = "Khối lượng")]
    //public string? Weight { get; set; }

    //[Display(Name = "Trạng thái")]
    //public string? Condition { get; set; }
}