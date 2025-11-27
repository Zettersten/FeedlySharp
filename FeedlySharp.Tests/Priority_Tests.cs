using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Priority_Tests
{
    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_get_priorities()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        var priorities = await feedlySharp.GetPrioritiesAsync();

        priorities.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_create_priority()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var priority = new Priority
        {
            Id = "priority-id",
            Label = "Test Priority",
            StreamId = "stream-id",
            ActionTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };

        var created = await feedlySharp.CreatePriorityAsync(priority);

        created.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid API credentials")]
    public async Task Should_delete_priority()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;
        var priorityId = "priority-id";

        await feedlySharp.DeletePriorityAsync(priorityId);
    }

    [Fact]
    public async Task Should_throw_when_priority_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentNullException>(() => feedlySharp.CreatePriorityAsync(null!));
    }

    [Fact]
    public async Task Should_throw_when_priority_id_is_null()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        await Assert.ThrowsAsync<ArgumentException>(() => feedlySharp.DeletePriorityAsync(null!));
    }
}
