using System;
using System.Runtime.Serialization;

namespace FeedlySharp.Models
{
    [Serializable]
    public class FeedlyException : Exception
    {
        public FeedlyException(Error error) : base(error.ToString())
        {
        }

        protected FeedlyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
