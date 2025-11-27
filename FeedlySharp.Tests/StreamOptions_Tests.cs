using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class StreamOptions_Tests
{
    [Fact]
    public void Should_create_default_stream_options()
    {
        var options = new StreamOptions();

        options.Count.Should().Be(20);
        options.Ranked.Should().Be(RankType.Newest);
        options.UnreadOnly.Should().BeFalse();
        options.StreamId.Should().BeNull();
        options.Continuation.Should().BeNull();
    }

    [Fact]
    public void Should_to_string_with_stream_id()
    {
        var options = new StreamOptions
        {
            StreamId = "feed/test",
            Count = 50,
            Ranked = RankType.Oldest,
            UnreadOnly = true
        };

        var result = options.ToString();

        result.Should().Contain("streamid=feed%2Ftest");
        result.Should().Contain("count=50");
        result.Should().Contain("ranked=oldest");
        result.Should().Contain("unreadonly=true");
    }

    [Fact]
    public void Should_to_string_with_continuation()
    {
        var options = new StreamOptions
        {
            StreamId = "feed/test",
            Continuation = "test-continuation"
        };

        var result = options.ToString();

        result.Should().Contain("continuation=test-continuation");
    }

    [Fact]
    public void Should_to_string_without_stream_id()
    {
        var options = new StreamOptions
        {
            Count = 30,
            Ranked = RankType.Engagement
        };

        var result = options.ToString();

        result.Should().Contain("count=30");
        result.Should().Contain("ranked=engagement");
        result.Should().NotContain("streamid=");
    }

    [Fact]
    public void Should_handle_special_characters_in_stream_id()
    {
        var options = new StreamOptions
        {
            StreamId = "feed/http://example.com/feed?param=value"
        };

        var result = options.ToString();

        result.Should().Contain("streamid=");
        result.Should().NotContain("http://example.com/feed?param=value");
    }
}
