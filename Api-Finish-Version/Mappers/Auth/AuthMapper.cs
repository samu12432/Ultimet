using Api_Finish_Version.DTO.Auth;
using Api_Finish_Version.Models.Auth;
using AutoMapper;

namespace Api_Finish_Version.Mappers.Auth
{
    public class AuthMapper : Profile
    {
        public AuthMapper() 
        {
            CreateMap<User, RegisterDto>().ReverseMap();
        }
    }
}
