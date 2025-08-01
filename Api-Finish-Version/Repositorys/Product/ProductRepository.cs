using Api_Finish_Version.Data;
using Api_Finish_Version.IRepository.Product;
using Api_Finish_Version.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Api_Finish_Version.Repositorys.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly ContextDb _context;
        
        public ProductRepository(ContextDb context)
        {
            _context = context;
        }
        
        public async Task<bool> AddAsync(Models.Product.Product product)
        {
            _context.Products.Add(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Models.Product.Product product)
        {
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistProductAsync(string codeProduct)
        {
            var exist = await _context.Products.FirstOrDefaultAsync(p => p.codeProduct == codeProduct);
            return exist != null;
        }

        public async Task<IEnumerable<Models.Product.Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Models.Product.Product?> GetProductByCodeAsync(string codeProduct)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.codeProduct == codeProduct);
        }

        public async Task<bool> UpdateAsync(Models.Product.Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
