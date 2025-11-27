namespace FeedlySharp.Models;

/// <summary>
/// Base resource model with id and label
/// </summary>
public record Resource
{
    public required string Id { get; init; }
    public string? Label { get; init; }
}
