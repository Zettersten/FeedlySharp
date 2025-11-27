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
}
