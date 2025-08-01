namespace Api_Finish_Version.IServices.Image
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile image, string folder);

    }
}
