using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Mix_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_mix()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var mixId = "mix/test-mix";

        var mix = await feedlySharp.GetMixAsync(mixId);

        mix.Should().NotBeNull();
        mix.Id.Should().Be(mixId);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_all_mixes()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        var mixes = await feedlySharp.GetMixesAsync();

        mixes.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_throw_when_mix_id_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => feedlySharp.GetMixAsync(null!));
    }
}
