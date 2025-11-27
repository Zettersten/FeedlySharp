using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Tag_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_tags()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var tags = await feedlySharp.GetTagsAsync();

        tags.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_tag_entry()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var tag = await feedlySharp.TagEntryAsync("test-entry-id", "user/test-user/tag/test-tag");

        tag.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_untag_entry()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        await feedlySharp.UntagEntryAsync("test-entry-id", "user/test-user/tag/test-tag");
    }
}
