namespace FeedlySharp.Models;

/// <summary>
/// Search term model for priority filters
/// </summary>
public record SearchTerm
{
    public List<Resource> Parts { get; init; } = [];
}
