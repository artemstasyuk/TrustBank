namespace BankApplication.Infrastructure.ImageService
{
    public interface IFileUploadService
    {       
        Task<AvatarModel> LoadFile(IFormFile file, IWebHostEnvironment webHost);               
    }
}
