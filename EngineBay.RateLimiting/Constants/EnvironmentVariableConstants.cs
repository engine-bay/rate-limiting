namespace EngineBay.RateLimiting
{
    public static class EnvironmentVariableConstants
    {
        public const string AUTOREPLENISHMENT = "RATE_LIMITING_AUTO_REPLENISHMENT";
        public const string PERMITLIMIT = "RATE_LIMITING_PERMIT_LIMIT";
        public const string QUEUELIMIT = "RATE_LIMITING_QUEUE_LIMIT";
        public const string QUEUEPROCESSINGORDER = "RATE_LIMITING_QUEUE_PROCESSING_ORDER";
        public const string REPLENISHMENTPERIOD = "RATE_LIMITING_REPLENISHMENT_PERIOD";
        public const string SEGMENTSPERWINDOW = "RATE_LIMITING_SEGMENTS_PER_WINDOW";
        public const string TOKENLIMIT = "RATE_LIMITING_TOKEN_LIMIT";
        public const string TOKENSPERPERIOD = "RATE_LIMITING_TOKENS_PER_PERIOD";
        public const string WINDOW = "RATE_LIMITING_WINDOW";
    }
}