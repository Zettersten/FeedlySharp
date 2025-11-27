using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Entry_Tests
{
    private const string TestEntryId = "U0B9hzMPYzqby9veraV2nWqKr9ZiyWt5hu6xQaFsoPA=_16fe63b32f3:460aa57:fd9c96c2";

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_single_entry()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var entry = await feedlySharp.GetEntryContentsAsync(TestEntryId);

        entry.Should().NotBeNull();
        entry!.Id.Should().NotBeNullOrWhiteSpace();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_multiple_entries()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var entries = await feedlySharp.GetEntryContentsAsync([TestEntryId, "+mZBOJxcVsi38VOzTzRj0Ozru3ydXPuFDUHMMpHPbPc=_16ff335c76c:594070c:31d4c877"]);

        entries.Should().NotBeNull();
        entries.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task Should_throw_when_entry_id_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => feedlySharp.GetEntryContentsAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_entry_ids_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => feedlySharp.GetEntryContentsAsync(null!));
    }
}
