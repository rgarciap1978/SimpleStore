using Microsoft.Extensions.Logging;

namespace SimpleStore.Services
{
    public static class Utils
    {
        public static string LogMessage(this ILogger logger, Exception exception, string methodName, bool error = true)
        {
            var message = $"Error en el {methodName}";
            if (error) logger.LogCritical(exception, $"{message} {exception.Message}");
            else logger.LogWarning(exception, $"{message} {exception.Message}");

            return message;
        }
    }
}
