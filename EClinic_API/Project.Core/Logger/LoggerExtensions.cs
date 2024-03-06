using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Project.Core.Logger
{
    public static class LoggerExtensions
    {
        public static ILoggingBuilder AddLogger(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(logger);
            return loggingBuilder;
        }

    }
}
