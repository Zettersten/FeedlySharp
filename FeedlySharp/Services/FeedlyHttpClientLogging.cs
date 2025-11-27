using Microsoft.Extensions.Logging;

namespace FeedlySharp.Services;

/// <summary>
/// Logging factory for FeedlySharp
/// </summary>
public static class FeedlyHttpClientLogging
{
    private static ILoggerFactory? _factory;

    public static ILoggerFactory LoggerFactory
    {
        get => _factory ??= new LoggerFactory();
        set => _factory = value;
    }

    public static ILogger CreateLogger() => LoggerFactory.CreateLogger<FeedlySharpHttpClient>();
}
