using System.Collections.Generic;

namespace FeedlySharp.Models
{
    public class Collection : Resource
    {
        public bool Customizable { get; set; }

        public bool Enterprise { get; set; }

        public int NumFeeds { get; set; }

        public List<Feed> Feeds { get; private set; } = new List<Feed>();
    }
}
