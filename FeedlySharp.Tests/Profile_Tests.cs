using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Profile_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_current_profile()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        var profile = await feedlySharp.GetProfileAsync();

        profile.Should().NotBeNull();
        profile.Id.Should().NotBeNullOrWhiteSpace();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_update_profile()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var currentProfile = await feedlySharp.GetProfileAsync();

        var updatedProfile = await feedlySharp.UpdateProfileAsync(currentProfile);

        updatedProfile.Should().NotBeNull();
    }
}
