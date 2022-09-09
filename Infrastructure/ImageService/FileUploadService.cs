namespace BankApplication.Infrastructure.ImageService
{
    public class FileUploadService : IFileUploadService
    {
        private readonly AppDbContext _dbContext;
        public FileUploadService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<AvatarModel> LoadFile(IFormFile uploadFile, IWebHostEnvironment webHost)
        {            
            if (uploadFile != null)
            {
                var avatar = await _dbContext.Files.FirstOrDefaultAsync(f => f.Name == uploadFile.FileName);
                if(avatar != null) return avatar;
                
                // путь к папке Files
                string path = "/Files/" + uploadFile.FileName;                
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(webHost.WebRootPath + path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fileStream);
                }
                
                var avatarFile = new AvatarModel { Name = uploadFile.FileName, Path = path };
                               
                await _dbContext.Files.AddAsync(avatarFile);
                await _dbContext.SaveChangesAsync();
                return avatarFile;
            }
            return new AvatarModel() { Name = "placeholder", Path = "~/img/placeholder.png" };
        }             
    }
}