namespace EngineBay.RateLimiting
{
    using System;

    public class RequestRejectedException : Exception
    {
        public RequestRejectedException()
        {
        }

        public RequestRejectedException(string message)
            : base(message)
        {
        }

        public RequestRejectedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
