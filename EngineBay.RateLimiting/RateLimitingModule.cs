namespace EngineBay.RateLimiting
{
    using System.Threading.RateLimiting;
    using EngineBay.Core;
    using Microsoft.AspNetCore.RateLimiting;

    public class RateLimitingModule : BaseModule
    {
        public override IServiceCollection RegisterModule(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                options
                .AddPolicy<string, FixedWindowPolicy>(RateLimitingPolicies.FIXED)
                .AddPolicy<string, SlidingWindowPolicy>(RateLimitingPolicies.SLIDING)
                .AddPolicy<string, TokenBucketPolicy>(RateLimitingPolicies.TOKEN)
                .AddPolicy<string, ConcurrencyPolicy>(RateLimitingPolicies.CONCURRENCY);
            });

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