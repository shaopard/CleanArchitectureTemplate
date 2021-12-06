using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Infrastructure.FileExport;
using CleanArchitecture.Infrastructure.Guid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGuidGenerator, GuidGenerator>();
            services.AddTransient<ICsvExporter, CsvExporter>();

            return services;
        }
    }
}