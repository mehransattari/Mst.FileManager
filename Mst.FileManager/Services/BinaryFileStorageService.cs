using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mst.FileManager.Data;
using Mst.FileManager.Models;
using Mst.FileManager.Services.Interfaces;
namespace Mst.FileManager.Services;

public class BinaryFileStorageService : IBinaryFileStorageService
{
    private readonly IFileManagerDbContext _dbContext;

    public BinaryFileStorageService(IFileManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> SaveBinaryFileAsync(IFormFile file, string? description)
    {
        var fileName = Guid.NewGuid().ToString() +"_"+ file.FileName;

        var contentType = file.ContentType;

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var fileData = memoryStream.ToArray();

            var fileEntity = new FileEntity
            {
                FileName = fileName,
                Data = fileData,
                ContentType = contentType,
                Size = fileData.Length,
                Description = description
            };

            _dbContext.Files.Add(fileEntity);
            await _dbContext.SaveChangesAsync();
        }

        return fileName;
    }


    public async Task<byte[]> ReadBinaryFileAsync(string fileName)
    {
        var fileEntity = await _dbContext.Files
            .FirstOrDefaultAsync(f => f.FileName == fileName);

        if (fileEntity == null)
        {
            throw new FileNotFoundException("File not found");
        }

        return fileEntity.Data;
    }
}

