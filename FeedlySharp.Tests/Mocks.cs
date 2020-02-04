using FeedlySharp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace FeedlySharp.Tests
{
    public static class Mocks
    {
        private static string[] EnviromentVariables => Environment.GetEnvironmentVariable("FEEDLY_VARS").Split(';', StringSplitOptions.RemoveEmptyEntries);

        public static FeedlyOptions MockFeedlyOptions => new FeedlyOptions
        {
            AccessToken = EnviromentVariables[0],
            RefreshToken = EnviromentVariables[1],
            UserID = EnviromentVariables[2],
            Domain = "https://cloud.feedly.com"
        };

        public static IOptions<FeedlyOptions> MockFeedlyIOptions => Options.Create(MockFeedlyOptions);
            
        public static IConfiguration MockFeedlyIConfiguration => new ConfigurationBuilder().AddInMemoryCollection(new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>($"Feedly:{nameof(FeedlyOptions.AccessToken)}", MockFeedlyOptions.AccessToken),
            new KeyValuePair<string, string>($"Feedly:{nameof(FeedlyOptions.RefreshToken)}", MockFeedlyOptions.RefreshToken),
            new KeyValuePair<string, string>($"Feedly:{nameof(FeedlyOptions.UserID)}", MockFeedlyOptions.UserID),
            new KeyValuePair<string, string>($"Feedly:{nameof(FeedlyOptions.Domain)}", MockFeedlyOptions.Domain),
        }).Build();

        public static IFeedlySharpHttpClient MockFeedlyHttpClient => new FeedlySharpHttpClient(MockFeedlyOptions);
    }
}
