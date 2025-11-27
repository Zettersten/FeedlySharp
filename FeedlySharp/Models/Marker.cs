namespace FeedlySharp.Models;

/// <summary>
/// Marker model for marking entries as read/unread
/// </summary>
public record Marker
{
    public required string Action { get; init; }
    public required string Type { get; init; }
    public List<string> EntryIds { get; init; } = [];
    public DateTimeOffset? LastReadAsEntryId { get; init; }
}

/// <summary>
/// Marker action types
/// </summary>
public static class MarkerAction
{
    public const string MarkAsRead = "markAsRead";
    public const string MarkAsUnread = "markAsUnread";
    public const string KeepUnread = "keepUnread";
    public const string MarkAsSaved = "markAsSaved";
    public const string MarkAsUnsaved = "markAsUnsaved";
}

/// <summary>
/// Marker type
/// </summary>
public static class MarkerType
{
    public const string Entries = "entries";
    public const string Feeds = "feeds";
}
