using Application.Interfaces.External;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Service.External
{
    public class UploadService : IUploadService
    {
        private readonly UploadFileSettings _uploadFile;
        public UploadService(IOptions<UploadFileSettings> uploadFile)
        {
            _uploadFile = uploadFile.Value;
        }
        public Task<string>? UploadFileAsync(IFormFile file)
        {
            if (file != null)
            {
                var rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + _uploadFile.FolderName);
                if (!Directory.Exists(rootFolder))
                {
                    Directory.CreateDirectory(rootFolder);
                }

                var contentType = file.ContentType;
                string subFolder = contentType switch
                {
                    var type when type.StartsWith("image/") => "Images",
                    var type when type.StartsWith("video/") => "Videos"
                };

                var folder = Path.Combine(rootFolder, subFolder);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var fileName = $"{Guid.NewGuid()} {Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Task.FromResult("/" + _uploadFile.FolderName + "/" + subFolder + "/" + fileName);
            }
            return null;
        }

        public Task<string> UploadProfileImageAsync(IFormFile file)
        {
            var profileFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + _uploadFile.ProfileFolderName);
            if (!Directory.Exists(profileFolder))
            {
                Directory.CreateDirectory(profileFolder);
            }

            var fileName = $"{Guid.NewGuid()} {Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(profileFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            return Task.FromResult("/" + _uploadFile.ProfileFolderName + "/" + fileName);
        }
    }
}