namespace FeedlySharp.Models;

/// <summary>
/// Read entry marker model
/// </summary>
public record ReadEntry
{
    public required string EntryId { get; init; }
    public DateTimeOffset? ActionTimestamp { get; init; }
}
