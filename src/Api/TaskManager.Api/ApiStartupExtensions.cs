using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using TaskManager.Application.Common;

namespace TaskManager.Api
{
    public static class ApiStartupExtensions
    {
        public static void AddSerilogFileLogging(this ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    configuration.GetValue<string>(Consts.ConfigurationNames.LoggerConfigFilePath),
                    fileSizeLimitBytes: 1_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1)
                )
                .CreateLogger();

            loggerFactory.AddSerilog(log);
            Log.Logger = log;
        }
    }
}
