using Microsoft.Extensions.Options;

namespace FeedlySharp.Models
{
    public class FeedlyOptions
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string UserID { get; set; }

        public string Domain { get; set; } = "https://cloud.feedly.com/";

        public static IOptions<FeedlyOptions> Create(string accessToken, string refreshToken, string userId, string domain = "https://cloud.feedly.com/")
        {
            return Options.Create(new FeedlyOptions
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserID = userId,
                Domain = domain
            });
        }
    }
}
