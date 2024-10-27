### Install package

`Install-Package MST.FileManager -Version 1.0.0`

### Program.cs
```

builder.Services.AddScoped<IFileManagerDbContext>(provider =>
        provider.GetService<ApplicationDbContext>());

builder.Services.AddFileManager();

```
                             


### Add IActivityLogDbContext To ApplicationDbContext
```
public class ApplicationDbContext : DbContext, IFileManagerDbContext
{   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public ApplicationDbContext()
    {
    }
   public DbSet<FileEntity> Files { get; set; }
}

```


###How Use Controller

```
[Route("api/[controller]")]
[ApiController]
public class LogEntriesController : ControllerBase
{
  private readonly IBinaryFileStorageService _fileManager;
  private readonly IPhysicalFileStorageService _physicalFile;

    public LogEntriesController(IBinaryFileStorageService fileManager, IPhysicalFileStorageService physicalFile, ApplicationDbContext context)
    {
        _activityLogger = activityLogger;
        _context = context;
   _fileManager = fileManager;
   _physicalFile = physicalFile;
    }

     [HttpPost("UploadFileBinary")]
 public async Task<IActionResult> UploadFile(IFormFile file)
 {
     var fileName = await _fileManager.SaveBinaryFileAsync(file);
     return Ok(new { FileName = fileName });
 }

 [HttpGet("DownloadFileBinary/{fileName}")]
 public async Task<IActionResult> DownloadFile(string fileName)
 {
     var fileData = await _fileManager.ReadBinaryFileAsync(fileName);
     return File(fileData, "application/octet-stream", fileName);
 }

 [HttpPost("UploadFilePhysical")]
 public async Task<IActionResult> UploadFilePhysical(IFormFile file)
 {
     var fileName = await _physicalFile.SavePhysicalFileAsync(file,"wwwroot/images");
     return Ok(new { FileName = fileName });
 }

 [HttpGet("DownloadFilePhysical/{fileName}")]
 public async Task<IActionResult> DownloadFilePhysical(string fileName)
 {
     var fileData = await _physicalFile.ReadPhysicalFileAsync(fileName, "wwwroot/images");
     return File(fileData, "application/octet-stream", fileName);
 }
}


```
