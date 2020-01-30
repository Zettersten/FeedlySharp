using Xunit;

namespace FeedlySharp.Tests
{
    public class Profile_Tests
    {
        [Fact]
        public void Should_get_current_profile()
        {
            var feedlySharp = Mocks.MockFeedlyHttpClient;

            var profile = feedlySharp.GetProfile().GetAwaiter().GetResult();

            Assert.NotNull(profile);
        }
    }
}