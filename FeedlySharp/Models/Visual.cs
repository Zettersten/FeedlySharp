namespace FeedlySharp.Models
{
    /// <summary>
    /// Optional visual object an image URL for this entry.
    /// If present, “url” will contain the image URL, “width” and “height” its dimension, and “contentType” its MIME type.
    /// </summary>
    public class Visual
    {
        public string Url { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ContentType { get; set; }
    }
}
