using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.External
{
    public interface IUploadService
    {
        Task<string>? UploadFileAsync(IFormFile file);
        Task<string> UploadProfileImageAsync(IFormFile file);
    }
}
