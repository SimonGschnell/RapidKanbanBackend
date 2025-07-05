using Microsoft.Extensions.DependencyInjection;
using RapidKanban.Application.Services;

namespace RapidKanban.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserstoryService, UserstoryService>();
        services.AddScoped<ITaskService, TaskService>();
    }
 
}