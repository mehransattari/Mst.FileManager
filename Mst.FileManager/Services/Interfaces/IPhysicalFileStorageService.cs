using Microsoft.AspNetCore.Http;

namespace Mst.FileManager.Services.Interfaces;

public interface IPhysicalFileStorageService
{
    Task<string> SavePhysicalFileAsync(IFormFile file, string storagePath);
    Task<byte[]> ReadPhysicalFileAsync(string fileName, string storagePath);
}

