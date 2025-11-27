namespace FeedlySharp.Models;

/// <summary>
/// Note model for entry annotations
/// </summary>
public record Note
{
    public required string EntryId { get; init; }
    public string? Comment { get; init; }
    public List<string> Tags { get; init; } = [];
}
