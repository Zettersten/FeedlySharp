using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Subscription_Tests
{
    private const string TestFeedId = "feed/http://feeds.feedburner.com/ScottHanselman";

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_subscriptions()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var subscriptions = await feedlySharp.GetSubscriptionsAsync();

        subscriptions.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_subscribe_to_feed()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var subscription = await feedlySharp.SubscribeToFeedAsync(TestFeedId);

        subscription.Should().NotBeNull();
        subscription.Id.Should().Be(TestFeedId);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_unsubscribe_from_feed()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        await feedlySharp.UnsubscribeFromFeedAsync(TestFeedId);
    }
}
