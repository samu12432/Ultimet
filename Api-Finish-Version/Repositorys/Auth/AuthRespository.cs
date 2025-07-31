using Api_Finish_Version.Data;
using Api_Finish_Version.IRepository.Auth;
using Api_Finish_Version.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace Api_Finish_Version.Repositorys.Auth
{
    public class AuthRespository : IAuthRepository
    {
        private readonly ContextDb _context;

        public AuthRespository(ContextDb context)
        {
            _context = context;
        }

        public Task<User?> GetByEmailAsync(string email) => _context.Users.FirstOrDefaultAsync(u => u.userEmail == email);

        public Task AddAsync(User user) =>
            _context.Users.AddAsync(user).AsTask(); // Optional: .AsTask() to clarify

        public Task SaveChangesAsync() =>
            _context.SaveChangesAsync();

        public Task<User?> GetUserByInfo(string userName) =>
            _context.Users.FirstOrDefaultAsync(u => u.userName == userName);

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
