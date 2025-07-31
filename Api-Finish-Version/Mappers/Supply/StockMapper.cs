using Api_Finish_Version.DTO.Supply;
using Stock = Api_Finish_Version.Models.Supply.Stock;
using AutoMapper;

namespace Api_Finish_Version.Mappers.Supply
{
    public class StockMapper : Profile
    {
        public StockMapper()
        {
            CreateMap<StockDto, Stock>()
            .ForMember(dest => dest.stockCreate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.stockUpdate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Stock, StockDto>();

        }
    }
}
