using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Search_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_search_feeds()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var result = await feedlySharp.SearchFeedsAsync("technology");

        result.Should().NotBeNull();
        result.Results.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_search_feeds_with_options()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var options = new SearchOptions
        {
            Query = "technology",
            Count = 10,
            Locale = "en"
        };

        var result = await feedlySharp.SearchFeedsAsync(options);

        result.Should().NotBeNull();
        result.Results.Should().NotBeNull();
    }
}
