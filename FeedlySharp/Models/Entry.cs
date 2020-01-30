using System;
using System.Collections.Generic;

namespace FeedlySharp.Models
{
    public class Entry : Resource
    {
        /// <summary>
        /// string the unique id of this post in the RSS feed (not necessarily a URL!)
        /// </summary>
        public string OriginId { get; set; }

        /// <summary>
        /// string the article fingerprint. This value might change if the article is updated.
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// Optional string the article’s title. This string does not contain any HTML markup.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Optional timestamp the timestamp, in ms, when this article was re-processed and updated by the feedly Cloud servers.
        /// </summary>
        public DateTimeOffset? Recrawled { get; set; }

        /// <summary>
        /// Optional timestamp the timestamp, in ms, when this article was updated, as reported by the RSS feed
        /// </summary>
        public DateTimeOffset? Updated { get; set; }

        /// <summary>
        /// timestamp the timestamp, in ms, when this article was published, as reported by the RSS feed (often inaccurate).
        /// </summary>
        public DateTimeOffset Published { get; set; }

        /// <summary>
        /// timestamp the immutable timestamp, in ms, when this article was processed by the feedly Cloud servers.
        /// </summary>
        public DateTimeOffset Crawled { get; set; }

        /// <summary>
        /// Optional content object the article summary. See the content object above.
        /// </summary>
        public Summary Summary { get; set; }

        /// <summary>
        /// Optional string the author’s name
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Optional origin object the feed from which this article was crawled.
        /// If present, “streamId” will contain the feed id, “title” will contain the feed title, and “htmlUrl” will contain the feed’s website.
        /// </summary>
        public Origin Origin { get; set; }

        /// <summary>
        /// Optional visual object an image URL for this entry.
        /// If present, “url” will contain the image URL, “width” and “height” its dimension, and “contentType” its MIME type.
        /// </summary>
        public Visual Visual { get; set; }

        /// <summary>
        /// boolean was this entry read by the user? If an Authorization header is not provided, this will always return false.
        /// If an Authorization header is provided, it will reflect if the user has read this entry or not.
        /// </summary>
        public bool Unread { get; set; }

        /// <summary>
        /// Optional integer an indicator of how popular this entry is.
        /// The higher the number, the more readers have read, saved or shared this particular entry.
        /// </summary>
        public int? Engagement { get; set; }

        /// <summary>
        /// Optional integer an indicator of how popular this entry is.
        /// The higher the number, the more readers have read, saved or shared this particular entry.
        /// </summary>
        public float? EngagementRate { get; set; }

        /// <summary>
        /// Optional priority object array a list of priority filters that match this entry (pro+ and team only).
        /// </summary>
        public List<Priority> Priorities { get; private set; } = new List<Priority>();

        /// <summary>
        /// Optional string array a list of keyword strings extracted from the RSS entry.
        /// </summary>
        public List<string> Keywords { get; private set; } = new List<string>();

        /// <summary>
        /// category object array a list of category objects (“id” and “label”) that the user associated with the feed of this entry.
        /// This value is only returned if an Authorization header is provided.
        /// </summary>
        public List<Resource> Categories { get; private set; } = new List<Resource>();

        /// <summary>
        /// Optional tag object array a list of tag objects (“id” and “label”) that the user added to this entry.
        /// This value is only returned if an Authorization header is provided, and at least one tag has been added.
        /// If the entry has been explicitly marked as read (not the feed itself), the “global.read” tag will be present.
        /// </summary>
        public List<Resource> Tags { get; private set; } = new List<Resource>();

        public List<Thumbnail> Thumbnail { get; private set; } = new List<Thumbnail>();

        public List<Reference> Alternate { get; private set; } = new List<Reference>();

        public List<Reference> Canonical { get; private set; } = new List<Reference>();

        public List<Reference> Enclosure { get; private set; } = new List<Reference>();
    }
}
