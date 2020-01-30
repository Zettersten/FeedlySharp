using Xunit;

namespace FeedlySharp.Tests
{
    public class Collection_Tests
    {
        [Fact]
        public void Should_get_your_feedly_collection()
        {
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var collections = feedlySharp.GetCollection().GetAwaiter().GetResult();

            Assert.NotNull(collections);
            Assert.True(collections.Count > 0);
        }
    }
}