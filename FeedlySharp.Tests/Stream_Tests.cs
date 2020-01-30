using FeedlySharp.Models;
using Xunit;

namespace FeedlySharp.Tests
{
    public class Stream_Tests
    {
        [Fact]
        public void Should_get_latest_save_for_later_stream()
        {
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var stream = feedlySharp.GetSaveForLaterStream(new StreamOptions()).GetAwaiter().GetResult();

            Assert.NotNull(stream);
            Assert.True(stream.Items.Count > 0);
        }

        [Fact]
        public async void Should_get_latest_save_for_later_stream_as_continuation()
        {
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var stream = feedlySharp.GetSaveForLaterStreamAsContinuation(new StreamOptions());

            await foreach (var s in stream)
            {
                Assert.NotNull(s);

                break;
            }
        }
    }
}