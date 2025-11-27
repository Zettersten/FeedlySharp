using System.Text.Json;
using FeedlySharp.Json;
using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class JsonSerialization_Tests
{
    [Fact]
    public void Should_serialize_entry()
    {
        var entry = new Entry
        {
            Id = "entry-id",
            Title = "Test Entry",
            Published = DateTimeOffset.UtcNow,
            Crawled = DateTimeOffset.UtcNow
        };

        var json = JsonSerializer.Serialize(entry, FeedlyJsonContext.Default.Entry);

        json.Should().NotBeNullOrEmpty();
        json.Should().Contain("entry-id");
        json.Should().Contain("Test Entry");
    }

    [Fact]
    public void Should_deserialize_entry()
    {
        var json = """{"id":"entry-id","title":"Test Entry","published":1234567890,"crawled":1234567890}""";

        var entry = JsonSerializer.Deserialize(json, FeedlyJsonContext.Default.Entry);

        entry.Should().NotBeNull();
        entry!.Id.Should().Be("entry-id");
        entry.Title.Should().Be("Test Entry");
    }

    [Fact]
    public void Should_serialize_collection()
    {
        var collection = new Collection
        {
            Id = "collection-id",
            Label = "Test Collection"
        };

        var json = JsonSerializer.Serialize(collection, FeedlyJsonContext.Default.Collection);

        json.Should().NotBeNullOrEmpty();
        json.Should().Contain("collection-id");
    }

    [Fact]
    public void Should_serialize_stream_options()
    {
        var options = new StreamOptions
        {
            StreamId = "feed/test",
            Count = 50,
            Ranked = RankType.Oldest
        };

        // StreamOptions uses ToString() for query strings, not JSON serialization
        var queryString = options.ToString();
        queryString.Should().NotBeNullOrEmpty();
        queryString.Should().Contain("feed%2Ftest");
    }

    [Fact]
    public void Should_serialize_marker()
    {
        var marker = new Marker
        {
            Action = MarkerAction.MarkAsRead,
            Type = MarkerType.Entries,
            EntryIds = ["id1", "id2"]
        };

        var json = JsonSerializer.Serialize(marker, FeedlyJsonContext.Default.Marker);

        json.Should().NotBeNullOrEmpty();
        json.Should().Contain("markAsRead");
    }

    [Fact]
    public void Should_serialize_list_of_entries()
    {
        var entries = new List<Entry>
        {
            new() { Id = "id1", Title = "Entry 1", Published = DateTimeOffset.UtcNow, Crawled = DateTimeOffset.UtcNow },
            new() { Id = "id2", Title = "Entry 2", Published = DateTimeOffset.UtcNow, Crawled = DateTimeOffset.UtcNow }
        };

        var json = JsonSerializer.Serialize(entries, FeedlyJsonContext.Default.ListEntry);

        json.Should().NotBeNullOrEmpty();
        json.Should().Contain("id1");
        json.Should().Contain("id2");
    }
}
