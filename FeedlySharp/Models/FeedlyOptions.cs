using Microsoft.Extensions.Options;

namespace FeedlySharp.Models;

/// <summary>
/// Configuration options for Feedly API client
/// </summary>
public record FeedlyOptions
{
    public required string AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public string? UserID { get; init; }
    public string Domain { get; init; } = "https://cloud.feedly.com/";

    public static IOptions<FeedlyOptions> Create(string accessToken, string? refreshToken = null, string? userId = null, string domain = "https://cloud.feedly.com/")
    {
        return Options.Create(new FeedlyOptions
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            UserID = userId,
            Domain = domain
        });
    }
}
