using Microsoft.AspNetCore.Http;

namespace Pharmacy.Application.Services.Integration_Service
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName = "uploads");
        Task<string> SaveFileAsync(Stream fileStream, string fileName, string folderName = "uploads");
        Task<bool> DeleteFile(string fileUrl);
    }
}
