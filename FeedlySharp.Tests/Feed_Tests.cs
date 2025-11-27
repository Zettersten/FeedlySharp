using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Feed_Tests
{
    private const string TestFeedId = "feed/http://feeds.feedburner.com/ScottHanselman";

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_feed()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var feed = await feedlySharp.GetFeedAsync(TestFeedId);

        feed.Should().NotBeNull();
        feed.Id.Should().NotBeNullOrWhiteSpace();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_multiple_feeds()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var feeds = await feedlySharp.GetFeedsAsync([TestFeedId, "feed/http://feeds.feedburner.com/oreilly/radar"]);

        feeds.Should().NotBeNull();
        feeds.Count.Should().BeGreaterThan(0);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_feed_metadata()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var metadata = await feedlySharp.GetFeedMetadataAsync(TestFeedId);

        metadata.Should().NotBeNull();
        metadata.FeedId.Should().Be(TestFeedId);
    }
}
