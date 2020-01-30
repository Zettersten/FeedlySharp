namespace FeedlySharp.Models
{
    public class StreamOptions
    {
        public string StreamId { get; set; }

        public int Count { get; set; } = 20;

        public RankType Ranked { get; set; } = RankType.Newest;

        public bool UnreadOnly { get; set; } = false;

        public string Continuation { get; set; }

        public override string ToString()
        {
            var query =
                   $"?{nameof(StreamId).ToLower()}={StreamId}" +
                   $"&{nameof(Count).ToLower()}={Count}" +
                   $"&{nameof(Ranked).ToLower()}={Ranked.ToString().ToLower()}" +
                   $"&{nameof(UnreadOnly).ToLower()}={UnreadOnly.ToString().ToLower()}";

            if (string.IsNullOrEmpty(Continuation))
            {
                return query;
            }

            return query + $"&{nameof(Continuation).ToLower()}={Continuation}";
        }
    }

    public enum RankType
    {
        Newest,
        Oldest,
        Engagement
    }
}
