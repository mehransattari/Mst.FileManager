using Microsoft.EntityFrameworkCore;
using Mst.FileManager.Models;
namespace Mst.FileManager.Data;

public interface IFileManagerDbContext
{
    DbSet<FileEntity> Files { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
