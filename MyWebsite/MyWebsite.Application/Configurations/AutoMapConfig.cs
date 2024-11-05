using AutoMapper;
using MyWebsite.Application.DTOs.Categories;
using MyWebsite.Application.DTOs.Products;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Entities;

namespace MyWebsite.Application.Configurations
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null))
                .ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.CategoryImage, opt => opt.MapFrom(src => src.Category != null ? src.Category.ImageData : null))
                .ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
