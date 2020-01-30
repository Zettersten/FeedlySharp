using System;
using System.Collections.Generic;

namespace FeedlySharp.Models
{
    public class Feed : Resource
    {
        public string FeedId { get; set; }

        public string Title { get; set; }

        public DateTimeOffset? Updated { get; set; }

        public float Velocity { get; set; }

        public List<string> Topics { get; private set; } = new List<string>();

        public int Subscribers { get; set; }

        public string Website { get; set; }

        public bool Partial { get; set; }

        public int EstimatedEngagement { get; set; }

        public string IconUrl { get; set; }

        public string CoverUrl { get; set; }

        public string VisualUrl { get; set; }

        public int NumReadEntriesPastMonth { get; set; }

        public int NumLongReadEntriesPastMonth { get; set; }

        public int TotalReadingTimePastMonth { get; set; }

        public int NumTaggedEntriesPastMonth { get; set; }

        public string Language { get; set; }

        public string ContentType { get; set; }

        public string Description { get; set; }

        public string CoverColor { get; set; }

        public string TwitterScreenName { get; set; }

        public int TwitterFollowers { get; set; }
    }
}
