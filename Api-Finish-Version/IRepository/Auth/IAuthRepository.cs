using Api_Finish_Version.Models.Auth;

namespace Api_Finish_Version.IRepository.Auth
{
    public interface IAuthRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetUserByInfo(string nameUser);
        Task AddAsync(User user);
        Task SaveChangesAsync();
        Task UpdateAsync(User user);
    }
}
