namespace EngineBay.RateLimiting
{
    internal static class LoggerExtensions
    {
        private static readonly Action<ILogger, string, string, Exception?> RequestRejectedValue = LoggerMessage.Define<string, string>(
            logLevel: LogLevel.Warning,
            eventId: 1,
            formatString: "Request rejected by '{Policy}' for '{Endpoint}'");

        public static void RequestRejected(this ILogger logger, string policy, string endpoint)
        {
            RequestRejectedValue(logger, policy, endpoint, null);
        }
    }
}