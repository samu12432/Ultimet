using Api_Finish_Version.DTO.Supply;
using Api_Finish_Version.Exceptions.Supply;
using Api_Finish_Version.IRepository.Supply;
using Api_Finish_Version.Models.Supply;
using AutoMapper;

namespace Api_Finish_Version.Services.Supply
{
    public class GlassService : SupplyServiceBase<GlassDto, Glass>
    {
        public GlassService(ISupplyRepository<Glass> repository, IMapper mapper)
            : base(repository, mapper) { }

        protected override SupplyException CreateAlreadyExistsException()
            => new GlassException("Ya existe un vidrio con este código.");
    }
}
