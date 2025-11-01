using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RCPS.Services.Implementations;
using RCPS.Services.Interfaces;
using RCPS.Services.Profiles;

namespace RCPS.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DomainProfile).Assembly);

        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IChangeRequestService, ChangeRequestService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IWorkLogService, WorkLogService>();
        services.AddScoped<IDashboardService, DashboardService>();

        return services;
    }
}
