namespace FeedlySharp.Models;

/// <summary>
/// Collection model representing a user's feed collection
/// </summary>
public record Collection : Resource
{
    public bool Customizable { get; init; }
    public bool Enterprise { get; init; }
    public int NumFeeds { get; init; }
    public List<Feed> Feeds { get; init; } = [];
}
