namespace FeedlySharp.Models;

/// <summary>
/// Stream of entries model
/// </summary>
public record Stream : Resource
{
    public string? Continuation { get; init; }
    public List<Entry> Items { get; init; } = [];
}

/// <summary>
/// Stream of entry IDs model
/// </summary>
public record StreamId : Resource
{
    public string? Continuation { get; init; }
    public List<string> Ids { get; init; } = [];
}
