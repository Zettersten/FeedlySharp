using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Validation_Tests
{
    [Fact]
    public async Task Should_throw_when_feed_id_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => client.GetFeedAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_feed_id_is_empty()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => client.GetFeedAsync(string.Empty));
    }

    [Fact]
    public async Task Should_throw_when_feed_ids_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetFeedsAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_collection_id_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => client.GetCollectionAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_collection_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateCollectionAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_profile_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateProfileAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_marker_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.MarkEntriesAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_priority_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreatePriorityAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_note_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateNoteAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_subscription_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateSubscriptionAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_preferences_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdatePreferencesAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_search_options_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.SearchFeedsAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_entry_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateAndTagEntryAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_feed_ids_is_empty()
    {
        var client = Mocks.MockFeedlyHttpClient;

        var result = await client.GetFeedsAsync([]);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_throw_when_entry_ids_is_empty()
    {
        var client = Mocks.MockFeedlyHttpClient;

        var result = await client.GetEntryContentsAsync([]);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_throw_when_add_feed_ids_is_empty()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.AddPersonalFeedToCollectionAsync("collection-id", []));
    }

    [Fact]
    public async Task Should_throw_when_remove_feed_ids_is_empty()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.RemovePersonalFeedFromCollectionAsync("collection-id", []));
    }

    [Fact]
    public async Task Should_throw_when_tag_id_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.TagEntryAsync("entry-id", null!));
    }

    [Fact]
    public async Task Should_throw_when_tag_label_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.ChangeTagLabelAsync("tag-id", null!));
    }

    [Fact]
    public async Task Should_throw_when_preference_key_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.GetPreferenceAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_mix_id_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.GetMixAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_priority_id_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.DeletePriorityAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_note_entry_id_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.GetNoteAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_subscribe_feed_id_is_null()
    {
        var client = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => 
            client.SubscribeToFeedAsync(null!));
    }
}
