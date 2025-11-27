using FeedlySharp.Models;
using FluentAssertions;
using Xunit;

namespace FeedlySharp.Tests;

public class ErrorHandling_Tests
{
    [Fact]
    public void Should_create_error()
    {
        var error = new Error
        {
            ErrorCode = 401,
            ErrorId = "unauthorized",
            ErrorMessage = "Invalid access token"
        };

        error.ErrorCode.Should().Be(401);
        error.ErrorId.Should().Be("unauthorized");
        error.ErrorMessage.Should().Be("Invalid access token");
    }

    [Fact]
    public void Should_to_string_error()
    {
        var error = new Error
        {
            ErrorCode = 404,
            ErrorId = "not_found",
            ErrorMessage = "Resource not found"
        };

        var result = error.ToString();

        result.Should().Contain("404");
        result.Should().Contain("not_found");
        result.Should().Contain("Resource not found");
    }

    [Fact]
    public void Should_create_feedly_exception()
    {
        var error = new Error
        {
            ErrorCode = 500,
            ErrorId = "server_error",
            ErrorMessage = "Internal server error"
        };

        var exception = new FeedlyException(error);

        exception.Error.Should().Be(error);
        exception.Message.Should().Contain("server_error");
    }

    [Fact]
    public void Should_create_feedly_exception_with_inner_exception()
    {
        var error = new Error
        {
            ErrorCode = 500,
            ErrorId = "server_error",
            ErrorMessage = "Internal server error"
        };

        var innerException = new Exception("Inner exception");
        var exception = new FeedlyException(error, innerException);

        exception.Error.Should().Be(error);
        exception.InnerException.Should().Be(innerException);
    }
}
