namespace FeedlySharp.Models;

/// <summary>
/// Unread count model
/// </summary>
public record UnreadCount
{
    public required string Id { get; init; }
    public int Count { get; init; }
    public DateTimeOffset? Updated { get; init; }
}
