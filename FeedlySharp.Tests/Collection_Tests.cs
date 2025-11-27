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

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_add_multiple_feeds_to_collection()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collections = await feedlySharp.GetCollectionsAsync();

        if (collections.Count > 0)
        {
            var feedIds = new[] { "feed/http://feeds.feedburner.com/ScottHanselman", "feed/http://feeds.feedburner.com/oreilly/radar" };
            var collection = await feedlySharp.AddPersonalFeedToCollectionAsync(collections[0].Id, feedIds);

            collection.Should().NotBeNull();
        }
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_remove_multiple_feeds_from_collection()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collections = await feedlySharp.GetCollectionsAsync();

        if (collections.Count > 0)
        {
            var feedIds = new[] { "feed/http://feeds.feedburner.com/ScottHanselman" };
            var collection = await feedlySharp.RemovePersonalFeedFromCollectionAsync(collections[0].Id, feedIds);

            collection.Should().NotBeNull();
        }
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_update_collection_cover()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collections = await feedlySharp.GetCollectionsAsync();

        if (collections.Count > 0)
        {
            var coverBytes = new byte[] { 0x89, 0x50, 0x4E, 0x47 }; // PNG header
            var collection = await feedlySharp.UpdateCollectionCoverAsync(collections[0].Id, coverBytes);

            collection.Should().NotBeNull();
        }
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_delete_collection()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var collection = new Collection
        {
            Id = $"user/{Mocks.MockFeedlyOptions.UserID}/category/test-{Guid.NewGuid()}",
            Label = "Test Collection To Delete"
        };

        var created = await feedlySharp.CreateCollectionAsync(collection);
        await feedlySharp.DeleteCollectionAsync(created.Id);
    }

    [Fact]
    public async Task Should_throw_when_collection_id_is_null_for_delete()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            feedlySharp.DeleteCollectionAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_collection_id_is_null_for_cover()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            feedlySharp.UpdateCollectionCoverAsync(null!, []));
    }

    [Fact]
    public async Task Should_throw_when_cover_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            feedlySharp.UpdateCollectionCoverAsync("collection-id", null!));
    }
}
