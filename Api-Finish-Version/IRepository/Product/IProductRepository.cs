



namespace Api_Finish_Version.IRepository.Product
{
    public interface IProductRepository
    {
        Task<bool> AddAsync(Models.Product.Product product);
        Task<bool> DeleteAsync(Models.Product.Product product);
        Task<bool> ExistProductAsync(string codeProduct);
        Task<IEnumerable<Models.Product.Product>> GetAllAsync();
        Task<Models.Product.Product?> GetProductByCodeAsync(string codeProduct);
        Task<bool> UpdateAsync(Models.Product.Product exist);
    }
}
