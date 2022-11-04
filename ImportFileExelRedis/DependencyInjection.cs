
using Application.Publisher;
using ImportFileExcelRedis.Infrastructure;

namespace ImportFileExcelRedis
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
