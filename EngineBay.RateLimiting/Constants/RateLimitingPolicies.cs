namespace EngineBay.RateLimiting
{
    public static class RateLimitingPolicies
    {
        public const string FIXED = "fixed";

        public const string SLIDING = "sliding";

        public const string TOKEN = "token";
        public const string CONCURRENCY = "concurrency";
    }
}