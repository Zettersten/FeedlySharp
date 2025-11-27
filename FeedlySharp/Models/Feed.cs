namespace FeedlySharp.Models;

/// <summary>
/// Feed information model
/// </summary>
public record Feed : Resource
{
    public string? FeedId { get; init; }
    public string? Title { get; init; }
    public DateTimeOffset? Updated { get; init; }
    public float Velocity { get; init; }
    public List<string> Topics { get; init; } = [];
    public int Subscribers { get; init; }
    public string? Website { get; init; }
    public bool Partial { get; init; }
    public int EstimatedEngagement { get; init; }
    public string? IconUrl { get; init; }
    public string? CoverUrl { get; init; }
    public string? VisualUrl { get; init; }
    public int NumReadEntriesPastMonth { get; init; }
    public int NumLongReadEntriesPastMonth { get; init; }
    public int TotalReadingTimePastMonth { get; init; }
    public int NumTaggedEntriesPastMonth { get; init; }
    public string? Language { get; init; }
    public string? ContentType { get; init; }
    public string? Description { get; init; }
    public string? CoverColor { get; init; }
    public string? TwitterScreenName { get; init; }
    public int TwitterFollowers { get; init; }
}
