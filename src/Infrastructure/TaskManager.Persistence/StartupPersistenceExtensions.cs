using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common;

namespace TaskManager.Persistence
{
    public static class StartupPersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(Consts.ConnectionStringNames.TaskManagerDatabase)));

            services.AddScoped<ITaskManagerDbContext>(provider => provider.GetService<TaskManagerDbContext>());

            return services;
        }
    }
}
