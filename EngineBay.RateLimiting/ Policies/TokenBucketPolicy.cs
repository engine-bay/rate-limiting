namespace EngineBay.RateLimiting
{
    using System.Threading.RateLimiting;
    using Microsoft.AspNetCore.RateLimiting;

    public class TokenBucketPolicy : IRateLimiterPolicy<string>
    {
        private readonly Func<OnRejectedContext, CancellationToken, ValueTask>? onRejected;

        public TokenBucketPolicy(ILogger<TokenBucketPolicy> logger)
        {
            ArgumentNullException.ThrowIfNull(logger);

            this.onRejected = OnRequestRejected.OnRejected(logger, nameof(TokenBucketPolicy));
        }

        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get => this.onRejected; }

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            return RateLimitPartition.GetTokenBucketLimiter<string>(string.Empty, key => new TokenBucketRateLimiterOptions
            {
                TokenLimit = RateLimitingConfiguration.TokenLimit(),
                QueueProcessingOrder = RateLimitingConfiguration.QueueProcessingOrder(),
                QueueLimit = RateLimitingConfiguration.QueueLimit(),
                ReplenishmentPeriod = TimeSpan.FromSeconds(RateLimitingConfiguration.ReplenishmentPeriod()),
                TokensPerPeriod = RateLimitingConfiguration.TokensPerPeriod(),
                AutoReplenishment = RateLimitingConfiguration.AutoReplenishment(),
            });
        }
    }
}