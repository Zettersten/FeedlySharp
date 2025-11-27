namespace FeedlySharp.Models;

/// <summary>
/// Feedly API error response model
/// </summary>
public record Error
{
    public required int ErrorCode { get; init; }
    public required string ErrorId { get; init; }
    public required string ErrorMessage { get; init; }

    public override string ToString() => $"[FeedlySharpHttpClient - Error ({ErrorCode})]: {ErrorId} - {ErrorMessage}";
}
