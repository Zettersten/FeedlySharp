using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Stream_Tests
{
    private const string TestStreamId = "feed/http://feeds.feedburner.com/ScottHanselman";

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_latest_stream()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var stream = await feedlySharp.GetStreamAsync(new StreamOptions { StreamId = TestStreamId });

        stream.Should().NotBeNull();
        stream.Items.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_latest_stream_as_continuation()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var stream = feedlySharp.GetStreamAsContinuationAsync(new StreamOptions { StreamId = TestStreamId });

        await foreach (var s in stream)
        {
            s.Should().NotBeNull();
            break;
        }
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_latest_stream_ids()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var streamIds = await feedlySharp.GetStreamIdsAsync(new StreamOptions { StreamId = TestStreamId });

        streamIds.Should().NotBeNull();
        streamIds.Ids.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_latest_stream_ids_as_continuation()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var streamIds = feedlySharp.GetStreamIdsAsContinuationAsync(new StreamOptions { StreamId = TestStreamId });

        await foreach (var s in streamIds)
        {
            s.Should().NotBeNull();
            break;
        }
    }

    [Fact]
    public void Should_create_stream_options()
    {
        var actual = new StreamOptions();
        var toUri = actual.ToString();

        actual.Count.Should().Be(20);
        actual.Ranked.Should().Be(RankType.Newest);
        actual.StreamId.Should().BeNull();
        actual.Continuation.Should().BeNull();
        actual.UnreadOnly.Should().BeFalse();
        toUri.Should().Contain("count=20");
        toUri.Should().Contain("ranked=newest");
    }
}
