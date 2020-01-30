using System;

namespace FeedlySharp.Models
{
    [Serializable]
    public class FeedlyException : Exception
    {
        public FeedlyException(Error error) : base(error.ToString())
        {
        }
    }
}
