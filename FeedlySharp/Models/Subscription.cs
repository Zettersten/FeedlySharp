namespace FeedlySharp.Models;

/// <summary>
/// Subscription model
/// </summary>
public record Subscription
{
    public required string Id { get; init; }
    public string? Title { get; init; }
    public string? Website { get; init; }
    public List<string> Categories { get; init; } = [];
    public DateTimeOffset? Updated { get; init; }
    public float Velocity { get; init; }
    public int Subscribers { get; init; }
    public string? IconUrl { get; init; }
    public string? VisualUrl { get; init; }
    public string? CoverUrl { get; init; }
    public string? Description { get; init; }
    public string? CoverColor { get; init; }
    public string? Language { get; init; }
    public string? ContentType { get; init; }
    public int EstimatedEngagement { get; init; }
}
