using Api_Finish_Version.Exceptions.Supply;
using Api_Finish_Version.IRepository.Supply;
using Api_Finish_Version.IServices.Supply;
using SUPPLY = Api_Finish_Version.Models.Supply.Supply;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using API_REST_PROYECT.DTOs.Supply;

namespace Api_Finish_Version.Services.Supply
{
    public abstract class SupplyServiceBase<TDto, TEntity> : ISupplyService<TDto>
    where TEntity : SUPPLY
    {
        protected readonly ISupplyRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        protected SupplyServiceBase(ISupplyRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AddSupplyAsync(TDto dto)
        {
            if (dto == null)
                throw new SupplyException("Los datos están vacíos.");

            var entity = _mapper.Map<TEntity>(dto);

            var exists = await _repository.ExistSupplyByCode(entity.codeSupply);
            if (exists)
                throw CreateAlreadyExistsException();

            return await _repository.AddAsync(entity);
        }

        public async Task<bool> DeleteSupplyAsync(string codeSupply)
        {
            if(codeSupply.IsNullOrEmpty()) throw new SupplyException("El código del suministro no puede estar vacío.");

            // Aquí se implementaría la lógica para eliminar el suministro por su código.
            var exists = await _repository.ExistSupplyByCode(codeSupply);
            if (exists) throw CreateAlreadyExistsException();

            var deleted = await _repository.DeleteAsync(codeSupply);

            return deleted; // Placeholder for actual delete logic
        }

        public async Task<bool> updateSupply(EditSupplyDto dto)
        {
            if (dto == null) throw new SupplyException("No puede estar vacío.");

            // Aquí se implementaría la lógica para editar el suministro por su código.
            var existing = await _repository.GetSupplyByCode(dto.codeSupply);
            if (existing == null) throw CreateAlreadyExistsException();

            existing.descriptionSupply = dto.description;

            return await _repository.UpdateSupply(existing); 
        }

        protected abstract SupplyException CreateAlreadyExistsException();
    }
}
