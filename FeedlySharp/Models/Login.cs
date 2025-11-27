namespace FeedlySharp.Models;

/// <summary>
/// Login provider information
/// </summary>
public record Login : Resource
{
    public bool Verified { get; init; }
    public string? Picture { get; init; }
    public string? Provider { get; init; }
    public string? ProviderId { get; init; }
    public string? FullName { get; init; }
}
