using Api_Finish_Version.DTO.Supply;
using Api_Finish_Version.Models.Supply;

namespace Api_Finish_Version.IServices.Supply
{
    public interface IStockService
    {
        Task<bool> AddStock(StockDto dto);
        Task<IEnumerable<StockDto>> GetAllStock();
        Task<Stock> GetStockBySku(string sku);
        Task<bool> UpdateStock(StockDto dto);
    }
}
