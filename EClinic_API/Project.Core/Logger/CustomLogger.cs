using Microsoft.Extensions.Logging;

namespace Project.Core.Logger
{
    public static class CustomLogger
    {
        public static void WriteLogInformation(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Information, message);
        }
        public static void WriteLogDebug(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Debug, message);
        }
        public static void WriteLogWarning(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Warning, message);
        }
        public static void WriteLogError(this ILogger logger, string message)
        {
            logger.Log(LogLevel.Error, message);
        }
    }
}
