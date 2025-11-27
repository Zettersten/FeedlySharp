namespace FeedlySharp.Models;

/// <summary>
/// Reference link model
/// </summary>
public record Reference
{
    public required string Href { get; init; }
    public string? Type { get; init; }
}
