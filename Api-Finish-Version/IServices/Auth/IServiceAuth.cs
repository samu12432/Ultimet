using Api_Finish_Version.DTO.Auth;

namespace Api_Finish_Version.IServices.Auth
{
    public interface IServiceAuth
    {
        Task<bool> RegisterUser(RegisterDto dto);

        Task<string?> LoginUser(LoginDto dto);

        Task<bool> ConfirmEmailAsync(string token);
    }
}
