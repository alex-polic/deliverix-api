using Deliverix.Common.Configurations;
using Deliverix.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Deliverix.Common.Helpers;

public static class UploadHelper
{
    private static readonly string UploadsPath = AppConfiguration.GetConfiguration("UploadsPath");
    private static readonly int MaxContentLength = int.Parse(AppConfiguration.GetConfiguration("MaxContentLength"));
    public static string UploadImage(IFormFile file)
    {
        if (file == null)
            throw new BusinessException("No file was uploaded!", 400);

        if (file.Length > MaxContentLength)
            throw new BusinessException("Uploaded files must be smaller than 20MB!", 413);

        string fileName = $"{DateTime.UtcNow.Ticks}-{file.Name}";
        string fileExtension = Path.GetExtension(file.FileName).ToLower();
        string newFileName = fileName + fileExtension;
        
        var stream = new FileStream(
            Path.Combine(Directory.GetCurrentDirectory(), UploadsPath, newFileName), 
            FileMode.Create
        );
        file.CopyToAsync(stream);
        
        return newFileName;
    }
}