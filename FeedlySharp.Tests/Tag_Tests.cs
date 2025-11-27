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

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_change_tag_label()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var tag = await feedlySharp.ChangeTagLabelAsync("user/test-user/tag/test-tag", "New Label");

        tag.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_delete_tag()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        await feedlySharp.DeleteTagAsync("user/test-user/tag/test-tag");
    }

    [Fact]
    public async Task Should_throw_when_entry_id_is_null_for_tag()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            feedlySharp.TagEntryAsync(null!, "tag-id"));
    }

    [Fact]
    public async Task Should_throw_when_tag_id_is_null_for_tag()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            feedlySharp.TagEntryAsync("entry-id", null!));
    }

    [Fact]
    public async Task Should_throw_when_tag_id_is_null_for_delete()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            feedlySharp.DeleteTagAsync(null!));
    }
}
