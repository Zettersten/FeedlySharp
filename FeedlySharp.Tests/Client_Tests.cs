using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class Client_Tests
{
    [Fact]
    public void Should_create_instance_of_client_using_FeedlyOptions()
    {
        var feedlySharp = Mocks.MockFeedlyHttpClient;

        feedlySharp.Should().NotBeNull();
        feedlySharp.Options.Should().NotBeNull();
    }

    [Fact]
    public void Should_create_instance_of_client_using_IOptions()
    {
        var feedlySharp = new FeedlySharpHttpClient(Mocks.MockFeedlyIOptions);

        feedlySharp.Should().NotBeNull();
        feedlySharp.Options.Should().NotBeNull();
    }

    [Fact]
    public void Should_create_instance_of_client_using_IConfiguration()
    {
        var feedlySharp = new FeedlySharpHttpClient(Mocks.MockFeedlyIConfiguration);

        feedlySharp.Should().NotBeNull();
        feedlySharp.Options.Should().NotBeNull();
    }

    [Fact]
    public void Should_throw_when_options_is_null()
    {
        Action act = () => new FeedlySharpHttpClient((FeedlyOptions)null!);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Should_throw_when_access_token_is_null()
    {
        var options = new FeedlyOptions { AccessToken = null! };

        Action act = () => new FeedlySharpHttpClient(options);

        act.Should().Throw<ArgumentException>();
    }
}
