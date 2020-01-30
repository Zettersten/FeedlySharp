using Xunit;

namespace FeedlySharp.Tests
{
    public class Client_Tests
    {
        [Fact]
        public void Should_create_instance_of_client_using_FeedlyOptions()
        {
            var feedlySharp = Mocks.MockFeedlyHttpClient;

            Assert.NotNull(feedlySharp);
        }

        [Fact]
        public void Should_create_instance_of_client_using_IOptions()
        {
            var feedlySharp = new FeedlySharpHttpClient(Mocks.MockFeedlyIOptions);

            Assert.NotNull(feedlySharp);
        }

        [Fact]
        public void Should_create_instance_of_client_using_IConfiguration()
        {
            var feedlySharp = new FeedlySharpHttpClient(Mocks.MockFeedlyIConfiguration);

            Assert.NotNull(feedlySharp);
        }
    }
}