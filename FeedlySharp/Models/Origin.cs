namespace FeedlySharp.Models
{
    /// <summary>
    /// Optional origin object the feed from which this article was crawled.
    /// If present, “streamId” will contain the feed id, “title” will contain the feed title, and “htmlUrl” will contain the feed’s website.
    /// </summary>
    public class Origin
    {
        public string StreamId { get; set; }

        public string Title { get; set; }

        public string HtmlUrl { get; set; }
    }
}
