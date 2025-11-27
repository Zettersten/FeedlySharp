namespace FeedlySharp.Models;

/// <summary>
/// Credit card details model
/// </summary>
public record CardDetails
{
    public string? Brand { get; init; }
    public string? Last4 { get; init; }
    public string? NameOnCard { get; init; }
    public int ExpirationMonth { get; init; }
    public int ExpirationYear { get; init; }
    public string? Country { get; init; }
}
