using Api_Finish_Version.DTO.Supply;
using Api_Finish_Version.IServices.Supply;
using Api_Finish_Version.Models.Supply;
using PROFILE = Api_Finish_Version.Models.Supply.Profile;
using AutoMapper;
using Api_Finish_Version.IRepository.Supply;
using Api_Finish_Version.Exceptions.Supply;

namespace Api_Finish_Version.Services.Supply
{
    public class ProfileService : SupplyServiceBase<ProfileDto, PROFILE>
    {
        public ProfileService(ISupplyRepository<PROFILE> repository, IMapper mapper)
            : base(repository, mapper) { }

        protected override SupplyException CreateAlreadyExistsException()
            => new ProfileException("Ya existe un perfil con este código.");
    }
}
