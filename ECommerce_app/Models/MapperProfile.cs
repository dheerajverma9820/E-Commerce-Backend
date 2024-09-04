using AutoMapper;
using ECommerce_app.Entities;
using ECommerce_app.Models.ResponseModel;

namespace ECommerce_app.Models
{
    public class MapperProfile: Profile

    {
        public MapperProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.productCategoryName, opt => opt.MapFrom(src => src.ProductCategory.CategoryName));
            CreateMap<ProductCategory, ProductCategoryResponse>();
            CreateMap<Image, ImageReponse>()
                   .ForMember(dest =>dest.imgURL,
            opt => opt.MapFrom(src => src.ImagePath));  
           
        }
    }
}
