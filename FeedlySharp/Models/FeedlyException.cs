namespace FeedlySharp.Models;

/// <summary>
/// Exception thrown when Feedly API returns an error
/// </summary>
public class FeedlyException : Exception
{
    public Error Error { get; }

    public FeedlyException(Error error) : base(error.ToString())
    {
        Error = error;
    }

    public FeedlyException(Error error, Exception innerException) : base(error.ToString(), innerException)
    {
        Error = error;
    }
}
