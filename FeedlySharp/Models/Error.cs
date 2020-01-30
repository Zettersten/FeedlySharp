namespace FeedlySharp.Models
{
    public class Error
    {
        public int ErrorCode { get; set; }

        public string ErrorId { get; set; }

        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return $"[{nameof(FeedlySharpHttpClient)} - Error ({ErrorCode})]: {ErrorId} - {ErrorMessage}";
        }
    }
}
