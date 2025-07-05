using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RapidKanban.Application.Repositories;
using RapidKanban.Infrastructure.Context;

namespace RapidKanban.Infrastructure.Repositories.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection service, IConfiguration config)
    {
        service.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("defaultConnection")));
        service.AddScoped<IUserstoryRepository,UserstoryRepository>();
        service.AddScoped<IUnitOfWork,UnitOfWork>();
    }
}