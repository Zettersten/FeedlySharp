using FeedlySharp.Models;
using Xunit;

namespace FeedlySharp.Tests
{
    public class Stream_Tests
    {
        [Fact]
        public void Should_get_latest_stream()
        {
            var streamId = "feed/http://feeds.feedburner.com/ScottHanselman";
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var stream = feedlySharp.GetStream(new StreamOptions() { StreamId = streamId }).GetAwaiter().GetResult();

            Assert.NotNull(stream);
            Assert.True(stream.Items.Count > 0);
        }

        [Fact]
        public async void Should_get_latest_stream_as_continuation()
        {
            var streamId = "feed/http://feeds.feedburner.com/ScottHanselman";
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var stream = feedlySharp.GetStreamAsContiuation(new StreamOptions() { StreamId = streamId });

            await foreach (var s in stream)
            {
                Assert.NotNull(s);

                break;
            }
        }

        [Fact]
        public void Should_create_stream_otpions()
        {
            var actual = new StreamOptions();
            var toUri = actual.ToString();

            Assert.Equal(20, actual.Count);
            Assert.Equal(RankType.Newest, actual.Ranked);
            Assert.Null(actual.StreamId);
            Assert.Null(actual.Continuation);
            Assert.False(actual.UnreadOnly);
            Assert.Equal("?streamid=&count=20&ranked=newest&unreadonly=false", toUri);
        }
    }
}
