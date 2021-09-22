using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using TaskManager.Application.Common;

namespace TaskManager.Persistence
{
    public class TaskManagerDbContextFactory : IDesignTimeDbContextFactory<TaskManagerDbContext>
    {
        public TaskManagerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString(Consts.ConnectionStringNames.TaskManagerDatabase);

            var optionsBuilder = new DbContextOptionsBuilder<TaskManagerDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new TaskManagerDbContext(optionsBuilder.Options);
        }
    }
}
