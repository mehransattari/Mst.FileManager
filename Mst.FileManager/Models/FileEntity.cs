using System.ComponentModel.DataAnnotations;
namespace Mst.FileManager.Models;

public class FileEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string FileName { get; set; }

    [Required]
    public required byte[] Data { get; set; }

    public string? ContentType { get; set; }

    [Range(0, long.MaxValue)]
    public long Size { get; set; } // اندازه فایل به بایت

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(500)] 
    public string? Description { get; set; }
}

