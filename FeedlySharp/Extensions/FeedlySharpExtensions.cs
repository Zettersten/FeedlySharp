using FeedlySharp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FeedlySharp.Extensions;

/// <summary>
/// Extension methods for dependency injection
/// </summary>
public static class FeedlySharpExtensions
{
    /// <summary>
    /// Adds FeedlySharp to the service collection using configuration
    /// </summary>
    public static IServiceCollection AddFeedlySharp(this IServiceCollection services)
    {
        services.AddHttpClient<IFeedlySharpHttpClient, FeedlySharpHttpClient>();
        services.AddSingleton<IFeedlySharpHttpClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<FeedlyOptions>>();
            return new FeedlySharpHttpClient(options);
        });

        return services;
    }

    /// <summary>
    /// Adds FeedlySharp to the service collection with explicit options
    /// </summary>
    public static IServiceCollection AddFeedlySharp(this IServiceCollection services, FeedlyOptions feedlyOptions)
    {
        ArgumentNullException.ThrowIfNull(feedlyOptions);
        
        services.AddSingleton<IFeedlySharpHttpClient>(new FeedlySharpHttpClient(feedlyOptions));
        return services;
    }

    /// <summary>
    /// Adds FeedlySharp to the service collection with options configuration
    /// </summary>
    public static IServiceCollection AddFeedlySharp(this IServiceCollection services, Action<FeedlyOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        
        services.Configure(configure);
        services.AddSingleton<IFeedlySharpHttpClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<FeedlyOptions>>();
            return new FeedlySharpHttpClient(options);
        });

        return services;
    }
}
