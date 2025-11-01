using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RCPS.Infrastructure.Data;
using RCPS.Infrastructure.Repositories;

namespace RCPS.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RcpsDatabase") ?? throw new InvalidOperationException("Connection string 'RcpsDatabase' not configured.");

        services.AddDbContext<RcpsDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(RcpsDbContext).Assembly.FullName);
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
