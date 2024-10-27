using Microsoft.Extensions.DependencyInjection;
using Mst.FileManager.Services;
using Mst.FileManager.Services.Interfaces;


namespace Mst.FileManager.Config;


public static class DependencyInjection
{
    public static IServiceCollection AddFileManager(this IServiceCollection services)
    {
        services.AddScoped<IBinaryFileStorageService, BinaryFileStorageService>();
        services.AddScoped<IPhysicalFileStorageService, PhysicalFileStorageService>();  
        return services;
    }
}


