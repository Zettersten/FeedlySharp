using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class FeedlyOptions_Tests
{
    [Fact]
    public void Should_create_feedly_options()
    {
        var options = new FeedlyOptions
        {
            AccessToken = "test-token",
            RefreshToken = "refresh-token",
            UserID = "user-id",
            Domain = "https://cloud.feedly.com/"
        };

        options.AccessToken.Should().Be("test-token");
        options.RefreshToken.Should().Be("refresh-token");
        options.UserID.Should().Be("user-id");
        options.Domain.Should().Be("https://cloud.feedly.com/");
    }

    [Fact]
    public void Should_have_default_domain()
    {
        var options = new FeedlyOptions
        {
            AccessToken = "test-token"
        };

        options.Domain.Should().Be("https://cloud.feedly.com/");
    }

    [Fact]
    public void Should_create_from_static_method()
    {
        var options = FeedlyOptions.Create(
            "access-token",
            "refresh-token",
            "user-id",
            "https://custom.feedly.com/");

        options.Value.AccessToken.Should().Be("access-token");
        options.Value.RefreshToken.Should().Be("refresh-token");
        options.Value.UserID.Should().Be("user-id");
        options.Value.Domain.Should().Be("https://custom.feedly.com/");
    }

    [Fact]
    public void Should_create_with_default_domain()
    {
        var options = FeedlyOptions.Create("access-token");

        options.Value.AccessToken.Should().Be("access-token");
        options.Value.Domain.Should().Be("https://cloud.feedly.com/");
    }
}
