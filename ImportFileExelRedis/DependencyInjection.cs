
using Application.Publisher;
using ImportFileExelRedis.Infrastructure;

namespace ImportFileExelRedis
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IImportManager, ImportManager>();
            services.AddSingleton<IEventPublisher, EventPublisher>();
            return services;
        }
    }
}
