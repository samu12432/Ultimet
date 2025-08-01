using Api_Finish_Version.DTO.Product;
using AutoMapper;

namespace Api_Finish_Version.Mappers.Product
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Models.Product.Product, ProductDto>().ReverseMap();
        }
    }
}
