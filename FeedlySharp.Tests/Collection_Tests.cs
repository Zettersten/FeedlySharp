using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Collection_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_collections()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collections = await feedlySharp.GetCollectionsAsync();

        collections.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_collection_by_id()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collections = await feedlySharp.GetCollectionsAsync();

        if (collections.Count > 0)
        {
            var collection = await feedlySharp.GetCollectionAsync(collections[0].Id);

            collection.Should().NotBeNull();
            collection.Id.Should().Be(collections[0].Id);
        }
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_create_collection()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collection = new Collection
        {
            Id = $"user/{Mocks.MockFeedlyOptions.UserID}/category/test-{Guid.NewGuid()}",
            Label = "Test Collection"
        };

        var created = await feedlySharp.CreateCollectionAsync(collection);

        created.Should().NotBeNull();
        created.Id.Should().Be(collection.Id);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_add_feed_to_collection()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collections = await feedlySharp.GetCollectionsAsync();

        if (collections.Count > 0)
        {
            var feedId = "feed/http://feeds.feedburner.com/ScottHanselman";
            var collection = await feedlySharp.AddPersonalFeedToCollectionAsync(collections[0].Id, feedId);

            collection.Should().NotBeNull();
        }
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_remove_feed_from_collection()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collections = await feedlySharp.GetCollectionsAsync();

        if (collections.Count > 0)
        {
            var feedId = "feed/http://feeds.feedburner.com/ScottHanselman";
            var collection = await feedlySharp.RemovePersonalFeedFromCollectionAsync(collections[0].Id, feedId);

            collection.Should().NotBeNull();
        }
    }
}
