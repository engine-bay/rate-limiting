namespace EngineBay.RateLimiting
{
    using System.Threading.RateLimiting;
    using EngineBay.Core;
    using Microsoft.AspNetCore.RateLimiting;

    public class RateLimitingModule : BaseModule
    {
        public override IServiceCollection RegisterModule(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(policyName: RateLimitingPolicies.FIXED, options =>
                {
                    options.PermitLimit = RateLimitingConfiguration.PermitLimit();
                    options.Window = TimeSpan.FromSeconds(RateLimitingConfiguration.Window());
                    options.QueueProcessingOrder = RateLimitingConfiguration.QueueProcessingOrder();
                    options.QueueLimit = RateLimitingConfiguration.QueueLimit();
                }));

            services.AddRateLimiter(_ => _
                .AddSlidingWindowLimiter(policyName: RateLimitingPolicies.SLIDING, options =>
                {
                    options.PermitLimit = RateLimitingConfiguration.PermitLimit();
                    options.Window = TimeSpan.FromSeconds(RateLimitingConfiguration.Window());
                    options.SegmentsPerWindow = RateLimitingConfiguration.SegmentsPerWindow();
                    options.QueueProcessingOrder = RateLimitingConfiguration.QueueProcessingOrder();
                    options.QueueLimit = RateLimitingConfiguration.QueueLimit();
                }));

            services.AddRateLimiter(_ => _
                .AddTokenBucketLimiter(policyName: RateLimitingPolicies.TOKEN, options =>
                {
                    options.TokenLimit = RateLimitingConfiguration.TokenLimit();
                    options.QueueProcessingOrder = RateLimitingConfiguration.QueueProcessingOrder();
                    options.QueueLimit = RateLimitingConfiguration.QueueLimit();
                    options.ReplenishmentPeriod = TimeSpan.FromSeconds(RateLimitingConfiguration.ReplenishmentPeriod());
                    options.TokensPerPeriod = RateLimitingConfiguration.TokensPerPeriod();
                    options.AutoReplenishment = RateLimitingConfiguration.AutoReplenishment();
                }));

            services.AddRateLimiter(_ => _
                .AddConcurrencyLimiter(policyName: RateLimitingPolicies.CONCURRENCY, options =>
                {
                    options.PermitLimit = RateLimitingConfiguration.PermitLimit();
                    options.QueueProcessingOrder = RateLimitingConfiguration.QueueProcessingOrder();
                    options.QueueLimit = RateLimitingConfiguration.QueueLimit();
                }));

            return services;
        }

        public override RouteGroupBuilder MapEndpoints(RouteGroupBuilder endpoints)
        {
            // override the base method to register API endpoints
            // otherwise, delete this method
            return endpoints;
        }

        public override WebApplication AddMiddleware(WebApplication app)
        {
            app.UseRateLimiter();
            return app;
        }

        public override IServiceCollection RegisterPolicies(IServiceCollection services)
        {
            // override the base method to register any role and claim policies
            // otherwise, delete this method
            return services;
        }

        public override void SeedDatabase(string seedDataPath, IServiceProvider serviceProvider)
        {
            // override the base method to load seed data for the module
            // otherwise, delete this method
            return;
        }
    }
}