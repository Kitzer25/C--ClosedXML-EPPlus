using Domain.Ports.Repositories;
using Domain.Ports.Services;
using Domain.Ports.Services.Security;
using Infraestructure.Adapters.Repositories;
using Infraestructure.Adapters.Services;
using Infraestructure.Adapters.Services.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGRepositories<>), typeof(GRepositories<>));

        services.AddScoped<IExcelExportService, ExcelExportService>();
        services.AddScoped<IPasswordHash, PasswordHash>();
        
        return services;
    }
}