using Api_Finish_Version.Data;
using Api_Finish_Version.IRepository.Supply;
using Api_Finish_Version.Models.Supply;
using Microsoft.EntityFrameworkCore;
using SUPPLY = Api_Finish_Version.Models.Supply.Supply;

namespace Api_Finish_Version.Repositorys.Supply
{
    public class SupplyRepository<TEntity> : ISupplyRepository<TEntity> where TEntity : SUPPLY
    {
        private readonly ContextDb _context;
        private readonly DbSet<TEntity> _dbSet;

        public SupplyRepository(ContextDb context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<bool> ExistSupplyByCode(string codeSupply)
        {
            return await _dbSet.AnyAsync(x => x.codeSupply == codeSupply);
        }

        public async Task<bool> AddAsync(TEntity newSupply)
        {
            _context.Supplies.Add(newSupply); // Usa la tabla base SUPPLY
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }


        public async Task<bool> DeleteAsync(string codeSupply)
        {
            var exist = await GetSupplyByCode(codeSupply);
            _context.Supplies.Remove(exist);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public Task<TEntity?> GetSupplyByCode(string codeSupply) => 
            _dbSet.FirstOrDefaultAsync(x => x.codeSupply == codeSupply);

        public async Task<bool> UpdateSupply<TEntity1>(TEntity1 existing) where TEntity1 : SUPPLY
        {
            var existingEntity = _dbSet.FirstOrDefault(x => x.codeSupply == existing.codeSupply);
            _context.Entry(existingEntity).CurrentValues.SetValues(existing);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
