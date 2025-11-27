using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Authentication_Tests
{
    [Fact(Skip = "Requires invalid token to test")]
    public async Task Should_throw_feedly_exception_unauthorized()
    {
        var options = new FeedlyOptions { AccessToken = "__invalid__", Domain = "https://cloud.feedly.com/" };
        var feedlySharp = new FeedlySharpHttpClient(options);

        await Assert.ThrowsAsync<FeedlyException>(() => feedlySharp.GetProfileAsync());
    }

    [Fact]
    public void Should_throw_when_access_token_is_whitespace()
    {
        var options = new FeedlyOptions { AccessToken = "   ", Domain = "https://cloud.feedly.com/" };

        Action act = () => new FeedlySharpHttpClient(options);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Should_throw_when_access_token_is_empty()
    {
        var options = new FeedlyOptions { AccessToken = string.Empty, Domain = "https://cloud.feedly.com/" };

        Action act = () => new FeedlySharpHttpClient(options);

        act.Should().Throw<ArgumentException>();
    }
}
