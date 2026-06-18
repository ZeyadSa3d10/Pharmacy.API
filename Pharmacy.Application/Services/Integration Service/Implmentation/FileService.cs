using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Pharmacy.Application.Services.Integration_Service.Implmentation
{
    public class FileService : IFileService
    {
        public IConfiguration Configuration { get; }

        public FileService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName = "uploads")
        {
            if(file == null||file.Length == 0) 
                throw new ArgumentNullException("File Is Invalid");
            string FolderPath = Path.Combine(Configuration["ImageDirectory"], folderName);
            if(!Directory.Exists(FolderPath)) 
                Directory.CreateDirectory(FolderPath);
            var FileName = $"{Guid.NewGuid()}_{file.FileName}";
            var FilePath = Path.Combine(FolderPath, FileName);
            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }
            return $"/{folderName}/{FileName}";

        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string folderName = "uploads")
        {
            if(fileStream == null||fileStream.Length == 0) 
                throw new ArgumentNullException("File Is Invalid");
            string FolderPath = Path.Combine(Configuration["ImageDirectory"], folderName);
            if(!Directory.Exists(FolderPath)) 
                Directory.CreateDirectory(FolderPath);
            var FilePath = Path.Combine(FolderPath, fileName);
            using (var stream = new FileStream(FilePath, FileMode.Create))
            {
               await fileStream.CopyToAsync(stream);
            }
            return $"/{folderName}/{fileName}";
        }
        public async Task<bool> DeleteFile(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl))
                return false;
            string filePath = Path.Combine(Configuration["ImageDirectory"], fileUrl.TrimStart('/'));
            if (!File.Exists(filePath))
                return false;
            File.Delete(filePath);
            return true;
        }
    }
}
