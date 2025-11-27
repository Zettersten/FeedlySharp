namespace FeedlySharp.Models;

/// <summary>
/// Priority filter model (pro+ and team only)
/// </summary>
public record Priority : Resource
{
    public SearchTerm? SearchTerms { get; init; }
    public long ActionTimestamp { get; init; }
    public string? StreamId { get; init; }
    public string? StreamLabel { get; init; }
}
