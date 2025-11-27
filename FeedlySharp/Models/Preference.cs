namespace FeedlySharp.Models;

/// <summary>
/// User preference model
/// </summary>
public record Preference
{
    public required string Key { get; init; }
    public string? Value { get; init; }
}
