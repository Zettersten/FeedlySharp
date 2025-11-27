using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Model_Tests
{
    [Fact]
    public void Should_create_resource()
    {
        var resource = new Resource
        {
            Id = "test-id",
            Label = "Test Label"
        };

        resource.Id.Should().Be("test-id");
        resource.Label.Should().Be("Test Label");
    }

    [Fact]
    public void Should_create_entry()
    {
        var entry = new Entry
        {
            Id = "entry-id",
            Title = "Test Entry",
            Published = DateTimeOffset.UtcNow,
            Crawled = DateTimeOffset.UtcNow
        };

        entry.Id.Should().Be("entry-id");
        entry.Title.Should().Be("Test Entry");
        entry.Published.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Should_create_stream_options()
    {
        var options = new StreamOptions
        {
            StreamId = "test-stream",
            Count = 50,
            Ranked = RankType.Oldest,
            UnreadOnly = true,
            Continuation = "test-continuation"
        };

        options.StreamId.Should().Be("test-stream");
        options.Count.Should().Be(50);
        options.Ranked.Should().Be(RankType.Oldest);
        options.UnreadOnly.Should().BeTrue();
        options.Continuation.Should().Be("test-continuation");
    }

    [Fact]
    public void Should_create_marker()
    {
        var marker = new Marker
        {
            Action = MarkerAction.MarkAsRead,
            Type = MarkerType.Entries,
            EntryIds = ["id1", "id2"]
        };

        marker.Action.Should().Be(MarkerAction.MarkAsRead);
        marker.Type.Should().Be(MarkerType.Entries);
        marker.EntryIds.Should().HaveCount(2);
    }

    [Fact]
    public void Should_create_search_options()
    {
        var options = new SearchOptions
        {
            Query = "test query",
            Count = 30,
            Locale = "en",
            Continuation = "test-cont"
        };

        options.Query.Should().Be("test query");
        options.Count.Should().Be(30);
        options.Locale.Should().Be("en");
        options.Continuation.Should().Be("test-cont");
    }

    [Fact]
    public void Should_create_collection()
    {
        var collection = new Collection
        {
            Id = "collection-id",
            Label = "Test Collection",
            Customizable = true,
            Enterprise = false,
            NumFeeds = 5
        };

        collection.Id.Should().Be("collection-id");
        collection.Label.Should().Be("Test Collection");
        collection.Customizable.Should().BeTrue();
        collection.Enterprise.Should().BeFalse();
        collection.NumFeeds.Should().Be(5);
    }

    [Fact]
    public void Should_create_feed()
    {
        var feed = new Feed
        {
            Id = "feed-id",
            FeedId = "feed-id",
            Title = "Test Feed",
            Velocity = 10.5f,
            Subscribers = 1000
        };

        feed.Id.Should().Be("feed-id");
        feed.FeedId.Should().Be("feed-id");
        feed.Title.Should().Be("Test Feed");
        feed.Velocity.Should().Be(10.5f);
        feed.Subscribers.Should().Be(1000);
    }

    [Fact]
    public void Should_create_preference()
    {
        var preference = new Preference
        {
            Key = "test-key",
            Value = "test-value"
        };

        preference.Key.Should().Be("test-key");
        preference.Value.Should().Be("test-value");
    }

    [Fact]
    public void Should_create_note()
    {
        var note = new Note
        {
            EntryId = "entry-id",
            Comment = "Test comment",
            Tags = ["tag1", "tag2"]
        };

        note.EntryId.Should().Be("entry-id");
        note.Comment.Should().Be("Test comment");
        note.Tags.Should().HaveCount(2);
    }

    [Fact]
    public void Should_create_subscription()
    {
        var subscription = new Subscription
        {
            Id = "sub-id",
            Title = "Test Subscription",
            Website = "https://example.com",
            Categories = ["cat1", "cat2"]
        };

        subscription.Id.Should().Be("sub-id");
        subscription.Title.Should().Be("Test Subscription");
        subscription.Website.Should().Be("https://example.com");
        subscription.Categories.Should().HaveCount(2);
    }
}
