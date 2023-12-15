namespace EngineBay.RateLimiting
{
    using System.Threading.RateLimiting;
    using Microsoft.AspNetCore.RateLimiting;

    public class FixedWindowPolicy : IRateLimiterPolicy<string>
    {
        private readonly Func<OnRejectedContext, CancellationToken, ValueTask>? onRejected;

        public FixedWindowPolicy(ILogger<FixedWindowPolicy> logger)
        {
            ArgumentNullException.ThrowIfNull(logger);

            this.onRejected = OnRequestRejected.OnRejected(logger, nameof(SlidingWindowPolicy));
        }

        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get => this.onRejected; }

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            return RateLimitPartition.GetFixedWindowLimiter<string>(string.Empty, key => new FixedWindowRateLimiterOptions
            {
                PermitLimit = RateLimitingConfiguration.PermitLimit(),
                Window = TimeSpan.FromSeconds(RateLimitingConfiguration.Window()),
                QueueProcessingOrder = RateLimitingConfiguration.QueueProcessingOrder(),
                QueueLimit = RateLimitingConfiguration.QueueLimit(),
            });
        }
    }
}