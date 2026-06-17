using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infraestructure.Context;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Busca appsettings.json desde la raíz del proyecto Web/API
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../S13MauricioCV.ReportData");
        // ↑ Ajusta esta ruta para que apunte al proyecto donde está tu appsettings.json
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var provider = configuration["DatabaseProvider"];
        
        if (provider == "MySQL")
        {
            var conn = configuration.GetConnectionString("MySQLConnection");
            optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));
        }
        else
        {
            var conn = configuration.GetConnectionString("PostgresConnection");
            optionsBuilder.UseNpgsql(conn);
        }

        return new AppDbContext(optionsBuilder.Options);
    }
}