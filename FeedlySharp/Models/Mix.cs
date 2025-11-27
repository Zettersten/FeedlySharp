namespace FeedlySharp.Models;

/// <summary>
/// Mix model for feed mixes
/// </summary>
public record Mix
{
    public required string Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public List<string> FeedIds { get; init; } = [];
}
