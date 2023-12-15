namespace EngineBay.RateLimiting
{
    using System.Threading.RateLimiting;
    using Microsoft.AspNetCore.RateLimiting;

    public class ConcurrencyPolicy : IRateLimiterPolicy<string>
    {
        private readonly Func<OnRejectedContext, CancellationToken, ValueTask>? onRejected;

        public ConcurrencyPolicy(ILogger<TokenBucketPolicy> logger)
        {
            ArgumentNullException.ThrowIfNull(logger);

            this.onRejected = OnRequestRejected.OnRejected(logger, nameof(TokenBucketPolicy));
        }

        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get => this.onRejected; }

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            return RateLimitPartition.GetConcurrencyLimiter<string>(string.Empty, key => new ConcurrencyLimiterOptions
            {
                PermitLimit = RateLimitingConfiguration.PermitLimit(),
                QueueProcessingOrder = RateLimitingConfiguration.QueueProcessingOrder(),
                QueueLimit = RateLimitingConfiguration.QueueLimit(),
            });
        }
    }
}