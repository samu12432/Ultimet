
using Api_Finish_Version.Models.Supply;

namespace Api_Finish_Version.IRepository.Supply
{
    public interface IStockRepository
    {
        Task<bool> AddAsync(Stock newStock);
        Task<IEnumerable<Stock>> GetAllStock();
        Task<Stock?> GetStockByCode(string codeSupply);
        Task<bool> GetStockBySku(string codeSupply);
        Task<bool> UpdateStockAsync(Stock exist);

    }
}
