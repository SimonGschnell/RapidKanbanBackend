using Microsoft.Extensions.DependencyInjection;
using RapidKanban.Application.DTO;
using RapidKanban.Application.Services;
using RapidKanban.Application.Services.Tasks;
using RapidKanban.CQRS;

namespace RapidKanban.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserstoryService, UserstoryService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ICommandHandler<CreateUserstoryCommand, UserstoryDTO>, CreateUserstoryHandler>();
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
    }
 
}