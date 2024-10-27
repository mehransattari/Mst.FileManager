using Microsoft.AspNetCore.Http;
using Mst.FileManager.Services.Interfaces;

namespace Mst.FileManager.Services;

public class PhysicalFileStorageService : IPhysicalFileStorageService
{

    public async Task<string> SavePhysicalFileAsync(IFormFile file, string storagePath)
    {
        var fileName = Guid.NewGuid().ToString()+ "_" +file.FileName ;
        var filePath = Path.Combine(storagePath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public async Task<byte[]> ReadPhysicalFileAsync(string fileName, string storagePath)
    {
        var filePath = Path.Combine(storagePath, fileName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found");
        }

        return await File.ReadAllBytesAsync(filePath);
    }
}
