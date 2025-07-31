using Api_Finish_Version.DTO.Supply;
using Api_Finish_Version.Exceptions.Supply;
using Api_Finish_Version.IRepository.Supply;
using Api_Finish_Version.Models.Supply;
using AutoMapper;

namespace Api_Finish_Version.Services.Supply
{
    public class AccessoryService : SupplyServiceBase<AccessoryDto, Accessory>
    {
        public AccessoryService(ISupplyRepository<Accessory> repository, IMapper mapper)
            : base(repository, mapper) { }

        protected override SupplyException CreateAlreadyExistsException()
            => new AccessoryException("Ya existe un accesorio con este código.");
    }

}
