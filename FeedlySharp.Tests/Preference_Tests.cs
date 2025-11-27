using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Preference_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_preferences()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        var preferences = await feedlySharp.GetPreferencesAsync();

        preferences.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_preference_by_key()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var key = "test-key";

        var preference = await feedlySharp.GetPreferenceAsync(key);

        preference.Should().NotBeNull();
        preference.Key.Should().Be(key);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_update_preferences()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var preferences = new List<Preference>
        {
            new() { Key = "key1", Value = "value1" },
            new() { Key = "key2", Value = "value2" }
        };

        await feedlySharp.UpdatePreferencesAsync(preferences);
    }

    [Fact]
    public async Task Should_throw_when_preference_key_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => feedlySharp.GetPreferenceAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_preferences_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => feedlySharp.UpdatePreferencesAsync(null!));
    }
}
