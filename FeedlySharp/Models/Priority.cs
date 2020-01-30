namespace FeedlySharp.Models
{
    public class Priority : Resource
    {
        public SearchTerm SearchTerms { get; set; }

        public long ActionTimestamp { get; set; }

        public string StreamId { get; set; }

        public string StreamLabel { get; set; }
    }
}
