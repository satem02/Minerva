using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minerva.Shared.Data;

namespace Minerva.Shared.Extensions
{
    public static class CapExtensions
    {
        public static IServiceCollection AddCap(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddCap(options =>
            {
                options.UseEntityFramework<MinervaDbContext>();
                options.UsePostgreSql(configuration.GetConnectionString("DbConnection"));
                options.UseRabbitMQ(configuration["RabbitMQ:HostName"]);
                options.UseDashboard();
            });
            return services;
        }
    }
}