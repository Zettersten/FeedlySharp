namespace FeedlySharp.Models;

/// <summary>
/// Tag model
/// </summary>
public record Tag : Resource
{
    public int Count { get; init; }
}
