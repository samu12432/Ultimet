using Api_Finish_Version.DTO.Supply;
using AutoMapper;

namespace Api_Finish_Version.Mappers.Supply
{
    public class SupplyMapper : Profile
    {
        public SupplyMapper()
        {
            CreateMap<Models.Supply.Profile, ProfileDto>().ReverseMap();
        }
    }
}
