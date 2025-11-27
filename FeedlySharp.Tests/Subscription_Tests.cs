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

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_subscribe_with_categories()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var categories = new List<string> { "category-id-1", "category-id-2" };
        var subscription = await feedlySharp.SubscribeToFeedAsync(TestFeedId, categories);

        subscription.Should().NotBeNull();
        subscription.Id.Should().Be(TestFeedId);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_update_subscription()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var subscription = new Subscription
        {
            Id = TestFeedId,
            Title = "Updated Title",
            Categories = ["category-id"]
        };

        var updated = await feedlySharp.UpdateSubscriptionAsync(subscription);

        updated.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_throw_when_feed_id_is_null_for_unsubscribe()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            feedlySharp.UnsubscribeFromFeedAsync(null!));
    }
}
