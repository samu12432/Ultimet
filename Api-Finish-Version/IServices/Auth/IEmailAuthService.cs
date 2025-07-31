namespace Api_Finish_Version.IServices.Auth
{
    public interface IEmailAuthService
    {
        Task SendAsync(string to, string subject, string body);
    }
}
