using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class ThreadSafety_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_handle_concurrent_requests()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        const int concurrentRequests = 10;

        var tasks = Enumerable.Range(0, concurrentRequests)
            .Select(_ => feedlySharp.GetProfileAsync())
            .ToArray();

        var results = await Task.WhenAll(tasks);

        results.Should().HaveCount(concurrentRequests);
        results.Should().OnlyContain(r => r != null);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_handle_concurrent_stream_requests()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        const int concurrentRequests = 5;

        var tasks = Enumerable.Range(0, concurrentRequests)
            .Select(_ => feedlySharp.GetStreamAsync(new Models.StreamOptions { StreamId = "feed/http://feeds.feedburner.com/ScottHanselman", Count = 10 }))
            .ToArray();

        var results = await Task.WhenAll(tasks);

        results.Should().HaveCount(concurrentRequests);
        results.Should().OnlyContain(r => r != null);
    }
}
