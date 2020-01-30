using Microsoft.Extensions.Logging;

namespace FeedlySharp.Services
{
    public class FeedlyHttpClientLogging
    {
        private static ILoggerFactory _Factory = null;

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_Factory == null)
                {
                    _Factory = new LoggerFactory();
                }

                return _Factory;
            }
            set { _Factory = value; }
        }

        public static ILogger CreateLogger() => LoggerFactory.CreateLogger<FeedlySharpHttpClient>();
    }
}
