using Microsoft.AspNetCore.Http;

namespace Mst.FileManager.Services.Interfaces;

public interface IBinaryFileStorageService
{
    Task<string> SaveBinaryFileAsync(IFormFile file, string? description="");
    Task<byte[]> ReadBinaryFileAsync(string fileName);
}

