using System.Collections.Generic;

namespace FeedlySharp.Models
{
    public class SearchTerm
    {
        public List<Resource> Parts { get; private set; } = new List<Resource>();
    }
}
