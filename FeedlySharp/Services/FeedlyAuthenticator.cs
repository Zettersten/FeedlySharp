using FeedlySharp.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace FeedlySharp.Services
{
    public class FeedlyAuthenticator : IAuthenticator
    {
        private readonly FeedlyOptions feedlyOptions;

        public FeedlyAuthenticator(FeedlyOptions feedlyOptions)
        {
            this.feedlyOptions = feedlyOptions;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("Authorization", $"Bearer {feedlyOptions.AccessToken}");
        }

        public FeedlyOptions Options { get => feedlyOptions; }
    }
}
