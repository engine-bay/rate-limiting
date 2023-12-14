namespace EngineBay.RateLimiting
{
    using System.Threading.RateLimiting;

    public abstract class RateLimitingConfiguration
    {
        public static int PermitLimit()
        {
            var permitLimitEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.PERMITLIMIT);
            int permitLimit;

            if (!string.IsNullOrEmpty(permitLimitEnvironmentVariable)
            && int.TryParse(permitLimitEnvironmentVariable, out permitLimit)
            && permitLimit > 0)
            {
                Console.WriteLine($"{EnvironmentVariableConstants.PERMITLIMIT} set to {permitLimit}.");
                return permitLimit;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.PERMITLIMIT} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultPermitLimit}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultPermitLimit;
        }

        public static int Window()
        {
            var windowEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.WINDOW);
            int window;

            if (!string.IsNullOrEmpty(windowEnvironmentVariable)
            && int.TryParse(windowEnvironmentVariable, out window)
            && window > 0)
            {
                Console.WriteLine($"{EnvironmentVariableConstants.WINDOW} set to {window}.");
                return window;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.WINDOW} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultWindow}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultWindow;
        }

        public static QueueProcessingOrder QueueProcessingOrder()
        {
            var queueProcessingOrderEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.QUEUEPROCESSINGORDER);
            QueueProcessingOrder queueProcessingOrder;

            if (!string.IsNullOrEmpty(queueProcessingOrderEnvironmentVariable)
            && Enum.TryParse(queueProcessingOrderEnvironmentVariable, out queueProcessingOrder))
            {
                Console.WriteLine($"{EnvironmentVariableConstants.QUEUEPROCESSINGORDER} set to {queueProcessingOrder}.");
                return queueProcessingOrder;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.QUEUEPROCESSINGORDER} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultQueueProcessingOrder}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultQueueProcessingOrder;
        }

        public static int QueueLimit()
        {
            var queueLimitEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.QUEUELIMIT);
            int queueLimit;

            if (!string.IsNullOrEmpty(queueLimitEnvironmentVariable)
            && int.TryParse(queueLimitEnvironmentVariable, out queueLimit)
            && queueLimit >= 0)
            {
                Console.WriteLine($"{EnvironmentVariableConstants.QUEUELIMIT} set to {queueLimit}.");
                return queueLimit;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.QUEUELIMIT} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultQueueLimit}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultQueueLimit;
        }

        public static int SegmentsPerWindow()
        {
            var segmentsPerWindowEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.SEGMENTSPERWINDOW);
            int segmentsPerWindow;

            if (!string.IsNullOrEmpty(segmentsPerWindowEnvironmentVariable)
            && int.TryParse(segmentsPerWindowEnvironmentVariable, out segmentsPerWindow)
            && segmentsPerWindow >= 0)
            {
                Console.WriteLine($"{EnvironmentVariableConstants.SEGMENTSPERWINDOW} set to {segmentsPerWindow}.");
                return segmentsPerWindow;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.SEGMENTSPERWINDOW} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultSegmentsPerWindow}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultSegmentsPerWindow;
        }

        public static int TokenLimit()
        {
            var tokenLimitEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.TOKENLIMIT);
            int tokenLimit;

            if (!string.IsNullOrEmpty(tokenLimitEnvironmentVariable)
            && int.TryParse(tokenLimitEnvironmentVariable, out tokenLimit)
            && tokenLimit >= 0)
            {
                Console.WriteLine($"{EnvironmentVariableConstants.TOKENLIMIT} set to {tokenLimit}.");
                return tokenLimit;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.TOKENLIMIT} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultTokenLimit}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultTokenLimit;
        }

        public static int ReplenishmentPeriod()
        {
            var replenishmentPeriodEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.REPLENISHMENTPERIOD);
            int replenishmentPeriod;

            if (!string.IsNullOrEmpty(replenishmentPeriodEnvironmentVariable)
            && int.TryParse(replenishmentPeriodEnvironmentVariable, out replenishmentPeriod)
            && replenishmentPeriod >= 0)
            {
                Console.WriteLine($"{EnvironmentVariableConstants.REPLENISHMENTPERIOD} set to {replenishmentPeriod}.");
                return replenishmentPeriod;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.REPLENISHMENTPERIOD} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultReplenishmentPeriod}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultReplenishmentPeriod;
        }

        public static int TokensPerPeriod()
        {
            var tokensPerPeriodEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.TOKENSPERPERIOD);
            int tokensPerPeriod;

            if (!string.IsNullOrEmpty(tokensPerPeriodEnvironmentVariable)
            && int.TryParse(tokensPerPeriodEnvironmentVariable, out tokensPerPeriod)
            && tokensPerPeriod >= 0)
            {
                Console.WriteLine($"{EnvironmentVariableConstants.TOKENSPERPERIOD} set to {tokensPerPeriod}.");
                return tokensPerPeriod;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.TOKENSPERPERIOD} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultSegmentsPerWindow}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultTokensPerPeriod;
        }

        public static bool AutoReplenishment()
        {
            var autoReplenishmentEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.AUTOREPLENISHMENT);
            bool autoReplenishment;

            if (!string.IsNullOrEmpty(autoReplenishmentEnvironmentVariable)
            && bool.TryParse(autoReplenishmentEnvironmentVariable, out autoReplenishment))
            {
                Console.WriteLine($"{EnvironmentVariableConstants.AUTOREPLENISHMENT} set to {autoReplenishment}.");
                return autoReplenishment;
            }

            Console.WriteLine($"Warning: {EnvironmentVariableConstants.AUTOREPLENISHMENT} not configured or invalid, using default '{DefaultRateLimitingConfigurationConstants.DefaultAutoReplenishment}'.");
            return DefaultRateLimitingConfigurationConstants.DefaultAutoReplenishment;
        }
    }
}