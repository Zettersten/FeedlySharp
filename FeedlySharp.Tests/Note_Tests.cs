using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Note_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_create_note()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var note = new Note
        {
            EntryId = "entry-id",
            Comment = "Test comment",
            Tags = ["tag1", "tag2"]
        };

        var created = await feedlySharp.CreateNoteAsync(note);

        created.Should().NotBeNull();
        created.EntryId.Should().Be(note.EntryId);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_note()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var entryId = "entry-id";

        var note = await feedlySharp.GetNoteAsync(entryId);

        note.Should().NotBeNull();
        note.EntryId.Should().Be(entryId);
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_delete_note()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var entryId = "entry-id";

        await feedlySharp.DeleteNoteAsync(entryId);
    }

    [Fact]
    public async Task Should_throw_when_note_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => feedlySharp.CreateNoteAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_entry_id_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => feedlySharp.GetNoteAsync(null!));
    }
}
