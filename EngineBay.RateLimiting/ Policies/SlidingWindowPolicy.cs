namespace EngineBay.RateLimiting
{
    using System.Threading.RateLimiting;
    using Microsoft.AspNetCore.RateLimiting;

    public class SlidingWindowPolicy : IRateLimiterPolicy<string>
    {
        private readonly Func<OnRejectedContext, CancellationToken, ValueTask>? onRejected;

        public SlidingWindowPolicy(ILogger<SlidingWindowPolicy> logger)
        {
            ArgumentNullException.ThrowIfNull(logger);

            this.onRejected = OnRequestRejected.OnRejected(logger, nameof(SlidingWindowPolicy));
        }

        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get => this.onRejected; }

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            return RateLimitPartition.GetSlidingWindowLimiter<string>(string.Empty, key => new SlidingWindowRateLimiterOptions
            {
                PermitLimit = RateLimitingConfiguration.PermitLimit(),
                Window = TimeSpan.FromSeconds(RateLimitingConfiguration.Window()),
                SegmentsPerWindow = RateLimitingConfiguration.SegmentsPerWindow(),
                QueueProcessingOrder = RateLimitingConfiguration.QueueProcessingOrder(),
                QueueLimit = RateLimitingConfiguration.QueueLimit(),
            });
        }
    }
}