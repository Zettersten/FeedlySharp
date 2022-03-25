using System.Threading.Tasks;
using FeedlySharp.Models;
using Xunit;

namespace FeedlySharp.Tests
{
    public class Stream_Tests
    {
        [Fact]
        public async Task Should_get_latest_stream()
        {
            var streamId = "feed/http://feeds.feedburner.com/ScottHanselman";
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var stream = await feedlySharp.GetStream(new StreamOptions() { StreamId = streamId });

            Assert.NotNull(stream);
            Assert.True(stream.Items.Count > 0);
        }

        [Fact]
        public async Task Should_get_latest_stream_as_continuation()
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
        public async Task Should_get_latest_streamId()
        {
            var streamId = "feed/http://feeds.feedburner.com/ScottHanselman";
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var streamIdResponse = await feedlySharp.GetStreamIds(new StreamOptions() { StreamId = streamId });

            Assert.NotNull(streamIdResponse);
            Assert.True(streamIdResponse.Ids.Count > 0);
        }

        [Fact]
        public async Task Should_get_latest_streamIds_as_continuation()
        {
            var streamId = "feed/http://feeds.feedburner.com/ScottHanselman";
            var feedlySharp = Mocks.MockFeedlyHttpClient;
            var streamIdResponse = feedlySharp.GetStreamIdsAsContiuation(new StreamOptions() { StreamId = streamId });

            await foreach (var s in streamIdResponse)
            {
                Assert.NotNull(s);

                break;
            }
        }

        [Fact]
        public void Should_create_stream_options()
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
