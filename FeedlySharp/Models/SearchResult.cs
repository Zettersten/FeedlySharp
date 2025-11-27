namespace FeedlySharp.Models;

/// <summary>
/// Search result model
/// </summary>
public record SearchResult
{
    public string? Query { get; init; }
    public List<Feed> Results { get; init; } = [];
    public string? Continuation { get; init; }
}

/// <summary>
/// Search options
/// </summary>
public record SearchOptions
{
    public required string Query { get; init; }
    public int Count { get; init; } = 20;
    public string? Locale { get; init; }
    public string? Continuation { get; init; }
}
