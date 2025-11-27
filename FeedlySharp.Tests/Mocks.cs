using FeedlySharp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FeedlySharp.Tests;

public static class Mocks
{
    private static string[]? _environmentVariables;
    
    private static string[] EnvironmentVariables
    {
        get
        {
            if (_environmentVariables == null)
            {
                var vars = Environment.GetEnvironmentVariable("FEEDLY_VARS");
                if (string.IsNullOrWhiteSpace(vars))
                {
                    // Use test values if environment variable is not set
                    _environmentVariables = ["test_token", "test_refresh", "test_user"];
                }
                else
                {
                    _environmentVariables = vars.Split(';', StringSplitOptions.RemoveEmptyEntries);
                }
            }
            return _environmentVariables;
        }
    }

    public static FeedlyOptions MockFeedlyOptions => new()
    {
        AccessToken = EnvironmentVariables[0],
        RefreshToken = EnvironmentVariables.Length > 1 ? EnvironmentVariables[1] : null,
        UserID = EnvironmentVariables.Length > 2 ? EnvironmentVariables[2] : null,
        Domain = "https://cloud.feedly.com/"
    };

    public static IOptions<FeedlyOptions> MockFeedlyIOptions => Options.Create(MockFeedlyOptions);
            
    public static IConfiguration MockFeedlyIConfiguration => new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
    {
        [$"Feedly:{nameof(FeedlyOptions.AccessToken)}"] = MockFeedlyOptions.AccessToken,
        [$"Feedly:{nameof(FeedlyOptions.RefreshToken)}"] = MockFeedlyOptions.RefreshToken,
        [$"Feedly:{nameof(FeedlyOptions.UserID)}"] = MockFeedlyOptions.UserID,
        [$"Feedly:{nameof(FeedlyOptions.Domain)}"] = MockFeedlyOptions.Domain,
    }).Build();

    public static IFeedlySharpHttpClient MockFeedlyHttpClient => new FeedlySharpHttpClient(MockFeedlyOptions);
}
