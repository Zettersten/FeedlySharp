namespace FeedlySharp.Models;

/// <summary>
/// Entry (article) model
/// </summary>
public record Entry : Resource
{
    /// <summary>
    /// string the unique id of this post in the RSS feed (not necessarily a URL!)
    /// </summary>
    public string? OriginId { get; init; }

    /// <summary>
    /// string the article fingerprint. This value might change if the article is updated.
    /// </summary>
    public string? Fingerprint { get; init; }

    /// <summary>
    /// Optional string the article's title. This string does not contain any HTML markup.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Optional timestamp the timestamp, in ms, when this article was re-processed and updated by the feedly Cloud servers.
    /// </summary>
    public DateTimeOffset? Recrawled { get; init; }

    /// <summary>
    /// Optional timestamp the timestamp, in ms, when this article was updated, as reported by the RSS feed
    /// </summary>
    public DateTimeOffset? Updated { get; init; }

    /// <summary>
    /// timestamp the timestamp, in ms, when this article was published, as reported by the RSS feed (often inaccurate).
    /// </summary>
    public DateTimeOffset Published { get; init; }

    /// <summary>
    /// timestamp the immutable timestamp, in ms, when this article was processed by the feedly Cloud servers.
    /// </summary>
    public DateTimeOffset Crawled { get; init; }

    /// <summary>
    /// Optional content object the article summary. See the content object above.
    /// </summary>
    public Summary? Summary { get; init; }

    /// <summary>
    /// Optional string the author's name
    /// </summary>
    public string? Author { get; init; }

    /// <summary>
    /// Optional origin object the feed from which this article was crawled.
    /// If present, "streamId" will contain the feed id, "title" will contain the feed title, and "htmlUrl" will contain the feed's website.
    /// </summary>
    public Origin? Origin { get; init; }

    /// <summary>
    /// Optional visual object an image URL for this entry.
    /// If present, "url" will contain the image URL, "width" and "height" its dimension, and "contentType" its MIME type.
    /// </summary>
    public Visual? Visual { get; init; }

    /// <summary>
    /// boolean was this entry read by the user? If an Authorization header is not provided, this will always return false.
    /// If an Authorization header is provided, it will reflect if the user has read this entry or not.
    /// </summary>
    public bool Unread { get; init; }

    /// <summary>
    /// Optional integer an indicator of how popular this entry is.
    /// The higher the number, the more readers have read, saved or shared this particular entry.
    /// </summary>
    public int? Engagement { get; init; }

    /// <summary>
    /// Optional integer an indicator of how popular this entry is.
    /// The higher the number, the more readers have read, saved or shared this particular entry.
    /// </summary>
    public float? EngagementRate { get; init; }

    /// <summary>
    /// Optional priority object array a list of priority filters that match this entry (pro+ and team only).
    /// </summary>
    public List<Priority> Priorities { get; init; } = [];

    /// <summary>
    /// Optional string array a list of keyword strings extracted from the RSS entry.
    /// </summary>
    public List<string> Keywords { get; init; } = [];

    /// <summary>
    /// category object array a list of category objects ("id" and "label") that the user associated with the feed of this entry.
    /// This value is only returned if an Authorization header is provided.
    /// </summary>
    public List<Resource> Categories { get; init; } = [];

    /// <summary>
    /// Optional tag object array a list of tag objects ("id" and "label") that the user added to this entry.
    /// This value is only returned if an Authorization header is provided, and at least one tag has been added.
    /// If the entry has been explicitly marked as read (not the feed itself), the "global.read" tag will be present.
    /// </summary>
    public List<Resource> Tags { get; init; } = [];

    public List<Thumbnail> Thumbnail { get; init; } = [];
    public List<Reference> Alternate { get; init; } = [];
    public List<Reference> Canonical { get; init; } = [];
    public List<Reference> Enclosure { get; init; } = [];
}
