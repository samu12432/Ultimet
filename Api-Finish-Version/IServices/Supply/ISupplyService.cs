using API_REST_PROYECT.DTOs.Supply;

namespace Api_Finish_Version.IServices.Supply
{
    public interface ISupplyService<T>
    {
        Task<bool> AddSupplyAsync(T newSupplie);

        Task<bool> DeleteSupplyAsync(string codeSupply);

        Task<bool> updateSupply(EditSupplyDto codeSupply);
    }
}
