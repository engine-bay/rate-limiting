namespace EngineBay.RateLimiting
{
    using Microsoft.AspNetCore.RateLimiting;

    public static class OnRequestRejected
    {
        public static Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected(ILogger logger, string policy)
        {
            return (context, token) =>
            {
                string endpoint = context.HttpContext.GetEndpoint()?.DisplayName ?? string.Empty;
                logger.RequestRejected(policy, endpoint);
                throw new RequestRejectedException();
            };
        }
    }
}