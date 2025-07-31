using SUPPLY = Api_Finish_Version.Models.Supply.Supply;
namespace Api_Finish_Version.IRepository.Supply
{
    public interface ISupplyRepository<T> where T : SUPPLY
    {
        Task<bool> ExistSupplyByCode(string codeSupply);

        Task<bool> AddAsync(T newSupply);

        Task<bool> DeleteAsync(string codeSupply);

        Task<T?> GetSupplyByCode(string codeSupply);

        Task<bool> UpdateSupply<TEntity>(TEntity existing) where TEntity : SUPPLY;
    }
}
