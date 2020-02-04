using System.Collections.Generic;

namespace FeedlySharp.Models
{
    public class Stream : Resource
    {
        public string Continuation { get; set; }

        public List<Entry> Items { get; private set; } = new List<Entry>();
    }

    public class StreamId : Resource
    {
        public string Continuation { get; set; }

        public List<string> Ids { get; private set; } = new List<string>();
    }
}
