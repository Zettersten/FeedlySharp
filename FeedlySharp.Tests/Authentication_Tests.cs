using FeedlySharp.Models;
using Xunit;

namespace FeedlySharp.Tests
{
    public class Authentication_Tests
    {
        [Fact]
        public void Should_throw_feedly_exception_unauthorized()
        {
            var feedlySharp = Mocks.MockFeedlyHttpClient;

            feedlySharp.Options.AccessToken = "__invalid__";

            Assert.Throws<FeedlyException>(() =>
            {
                feedlySharp.GetProfile().GetAwaiter().GetResult();
            });
        }
    }
}