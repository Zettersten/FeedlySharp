namespace FeedlySharp.Models;

/// <summary>
/// Optional content object the article summary
/// </summary>
public record Summary
{
    public string? Content { get; init; }
    public string? Direction { get; init; }
}
