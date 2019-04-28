using Microsoft.Extensions.DependencyInjection;
using Minerva.Job.Consumers;

namespace Minerva.Job.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddTransient<IBookmarkConsumer, BookmarkConsumer>();
            services.AddTransient<IEmailConsumer, EmailConsumer>();
            return services;
        }
    }
}