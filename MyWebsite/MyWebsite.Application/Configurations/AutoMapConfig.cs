using AutoMapper;
using MyWebsite.Application.DTOs.Categories;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Entities;

namespace MyWebsite.Application.Configurations
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
