using System.Text;

namespace FeedlySharp.Models;

/// <summary>
/// Options for stream queries
/// </summary>
public record StreamOptions
{
    public string? StreamId { get; init; }
    public int Count { get; init; } = 20;
    public RankType Ranked { get; init; } = RankType.Newest;
    public bool UnreadOnly { get; init; } = false;
    public string? Continuation { get; init; }

    public override string ToString()
    {
        var query = new StringBuilder("?");
        var hasParams = false;
        
        if (!string.IsNullOrEmpty(StreamId))
        {
            query.Append($"{nameof(StreamId).ToLowerInvariant()}={Uri.EscapeDataString(StreamId)}");
            hasParams = true;
        }
        
        if (hasParams)
            query.Append('&');
        query.Append($"{nameof(Count).ToLowerInvariant()}={Count}");
        query.Append($"&{nameof(Ranked).ToLowerInvariant()}={Ranked.ToString().ToLowerInvariant()}");
        query.Append($"&{nameof(UnreadOnly).ToLowerInvariant()}={UnreadOnly.ToString().ToLowerInvariant()}");

        if (!string.IsNullOrEmpty(Continuation))
        {
            query.Append($"&{nameof(Continuation).ToLowerInvariant()}={Uri.EscapeDataString(Continuation)}");
        }

        return query.ToString();
    }
}

/// <summary>
/// Ranking type for stream ordering
/// </summary>
public enum RankType
{
    Newest,
    Oldest,
    Engagement
}
