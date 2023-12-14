namespace EngineBay.RateLimiting
{
    using System.Threading.RateLimiting;

    public static class DefaultRateLimitingConfigurationConstants
    {
        public const bool DefaultAutoReplenishment = true;
        public const int DefaultPermitLimit = 100;
        public const int DefaultQueueLimit = 10;
        public const QueueProcessingOrder DefaultQueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        public const int DefaultReplenishmentPeriod = 5;
        public const int DefaultSegmentsPerWindow = 5;
        public const int DefaultTokenLimit = 100;
        public const int DefaultTokensPerPeriod = 10;
        public const int DefaultWindow = 60;
    }
}