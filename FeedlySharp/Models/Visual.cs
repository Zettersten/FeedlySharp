namespace FeedlySharp.Models;

/// <summary>
/// Optional visual object an image URL for this entry.
/// If present, "url" will contain the image URL, "width" and "height" its dimension, and "contentType" its MIME type.
/// </summary>
public record Visual
{
    public required string Url { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public string? ContentType { get; init; }
}
