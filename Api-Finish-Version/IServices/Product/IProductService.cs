using Api_Finish_Version.DTO.Product;
using API_REST_PROYECT.DTOs.Product;

namespace Api_Finish_Version.IServices.Product
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductDto dto);
        Task<bool> DeleteProductAsync(string codeProduct);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(ProductUpdateDto dto);
    }
}
