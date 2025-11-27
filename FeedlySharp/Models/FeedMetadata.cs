namespace FeedlySharp.Models;

/// <summary>
/// Feed metadata model
/// </summary>
public record FeedMetadata
{
    public required string FeedId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? Website { get; init; }
    public string? IconUrl { get; init; }
    public string? VisualUrl { get; init; }
    public string? CoverUrl { get; init; }
    public string? Language { get; init; }
    public string? ContentType { get; init; }
    public int Subscribers { get; init; }
    public float Velocity { get; init; }
    public DateTimeOffset? Updated { get; init; }
}
