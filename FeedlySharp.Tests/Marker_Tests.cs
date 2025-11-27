using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Marker_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_mark_entries()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var marker = new Marker
        {
            Action = MarkerAction.MarkAsRead,
            Type = MarkerType.Entries,
            EntryIds = ["test-entry-id"]
        };

        await feedlySharp.MarkEntriesAsync(marker);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_unread_counts()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var counts = await feedlySharp.GetUnreadCountsAsync();

        counts.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_reads()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var reads = await feedlySharp.GetReadsAsync();

        reads.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_reads_with_parameters()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var reads = await feedlySharp.GetReadsAsync("feed/http://feeds.feedburner.com/ScottHanselman", 10);

        reads.Should().NotBeNull();
    }
}
