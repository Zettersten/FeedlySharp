![FeedlySharp](https://raw.githubusercontent.com/Zettersten/FeedlySharp/master/FeedlySharp/feedly.png "FeedlySharp")
# FeedlySharp

![Build history](https://buildstats.info/azurepipelines/chart/nenvy/FeedlySharp/8)

A high-performance, thread-safe HTTP Client for the Feedly Cloud API, built with .NET 10, source generators, and AOT support. Millions of users depend on their feedly for inspiration, information, and to feed their passions. But one size does not fit all. Individuals have different workflows, different habits, and different devices. In our efforts to evolve feedly from a product to a platform, we have therefore decided to open up the feedly API. Developers are welcome to deliver new applications, experiences, and innovations via the feedly cloud. We feel strongly that this will help to accelerate innovation and better serve our users.

[![Build Status](https://dev.azure.com/nenvy/FeedlySharp/_apis/build/status/Zettersten.FeedlySharp?branchName=master)](https://dev.azure.com/nenvy/FeedlySharp/_build/latest?definitionId=8&branchName=master) [![NuGet Badge](https://buildstats.info/nuget/FeedlySharp)](https://www.nuget.org/packages/FeedlySharp/)

## Features

- 🚀 **High Performance** - Built with source generators and optimized JSON serialization
- 🔒 **Thread-Safe** - Fully thread-safe implementation using modern HttpClient patterns
- ⚡ **AOT Compatible** - Native AOT support for maximum performance and minimal footprint
- 📦 **Complete API Coverage** - All Feedly API v3 endpoints implemented
- 🎯 **Modern C#** - Uses latest C# features including records, init-only properties, and pattern matching
- 🔧 **Dependency Injection** - First-class support for ASP.NET Core DI

## Installation

```bash
dotnet add package FeedlySharp
```

or from the VS Package Manager

```powershell
Install-Package FeedlySharp
```

## Quick Start

### Basic Usage

```csharp
using FeedlySharp;
using FeedlySharp.Models;

var options = new FeedlyOptions
{
    AccessToken = "your-access-token",
    Domain = "https://cloud.feedly.com/"
};

var client = new FeedlySharpHttpClient(options);

// Get your profile
var profile = await client.GetProfileAsync();

// Get collections
var collections = await client.GetCollectionsAsync();

// Get stream contents
var stream = await client.GetStreamAsync(new StreamOptions 
{ 
    StreamId = "feed/http://feeds.feedburner.com/ScottHanselman",
    Count = 20 
});
```

### Dependency Injection (ASP.NET Core)

```csharp
using FeedlySharp.Extensions;

// In your Program.cs or Startup.cs
services.AddFeedlySharp(options =>
{
    options.AccessToken = configuration["Feedly:AccessToken"];
    options.RefreshToken = configuration["Feedly:RefreshToken"];
    options.UserID = configuration["Feedly:UserID"];
    options.Domain = configuration["Feedly:Domain"] ?? "https://cloud.feedly.com/";
});

// Or use configuration directly
services.AddFeedlySharp(); // Reads from IConfiguration automatically
```

Configuration keys:
- `Feedly:AccessToken`
- `Feedly:RefreshToken`
- `Feedly:UserID`
- `Feedly:Domain`

### Using IOptions

```csharp
services.Configure<FeedlyOptions>(configuration.GetSection("Feedly"));
services.AddFeedlySharp(); // Uses IOptions<FeedlyOptions>
```

## API Coverage

### ✅ Entry API
- `GetEntryContentsAsync(string id)` - Get a single entry
- `GetEntryContentsAsync(IEnumerable<string> ids)` - Get multiple entries
- `CreateAndTagEntryAsync(Entry entry)` - Create and tag an entry

### ✅ Profile API
- `GetProfileAsync()` - Get current user profile
- `UpdateProfileAsync(Profile profile)` - Update user profile

### ✅ Collections API
- `GetCollectionsAsync()` - Get all collections
- `GetCollectionAsync(string id)` - Get a specific collection
- `CreateCollectionAsync(Collection collection)` - Create a new collection
- `UpdateCollectionCoverAsync(string id, byte[] cover)` - Update collection cover image
- `AddPersonalFeedToCollectionAsync(string id, string feedId)` - Add feed to collection
- `AddPersonalFeedToCollectionAsync(string id, IEnumerable<string> feedIds)` - Add multiple feeds
- `RemovePersonalFeedFromCollectionAsync(string id, string feedId)` - Remove feed from collection
- `RemovePersonalFeedFromCollectionAsync(string id, IEnumerable<string> feedIds)` - Remove multiple feeds
- `DeleteCollectionAsync(string id)` - Delete a collection

### ✅ Stream API
- `GetStreamAsync(StreamOptions? options)` - Get stream contents
- `GetStreamAsContinuationAsync(StreamOptions? options)` - Get stream with automatic continuation
- `GetStreamIdsAsync(StreamOptions? options)` - Get stream entry IDs
- `GetStreamIdsAsContinuationAsync(StreamOptions? options)` - Get stream IDs with automatic continuation

### ✅ Feed API
- `GetFeedAsync(string feedId)` - Get feed information
- `GetFeedsAsync(IEnumerable<string> feedIds)` - Get multiple feeds
- `GetFeedMetadataAsync(string feedId)` - Get feed metadata

### ✅ Marker API
- `MarkEntriesAsync(Marker marker)` - Mark entries as read/unread/saved
- `GetUnreadCountsAsync()` - Get unread counts for all streams
- `GetReadsAsync(string? streamId, int? count, string? newerThan)` - Get read markers

### ✅ Mix API
- `GetMixAsync(string mixId)` - Get a specific mix
- `GetMixesAsync()` - Get all mixes

### ✅ Preferences API
- `GetPreferencesAsync()` - Get all preferences
- `GetPreferenceAsync(string key)` - Get a specific preference
- `UpdatePreferencesAsync(List<Preference> preferences)` - Update preferences

### ✅ Priority API
- `GetPrioritiesAsync()` - Get all priority filters
- `CreatePriorityAsync(Priority priority)` - Create a priority filter
- `DeletePriorityAsync(string priorityId)` - Delete a priority filter

### ✅ Notes & Highlights API
- `CreateNoteAsync(Note note)` - Create a note for an entry
- `GetNoteAsync(string entryId)` - Get note for an entry
- `DeleteNoteAsync(string entryId)` - Delete note for an entry

### ✅ Tags API
- `GetTagsAsync()` - Get all tags
- `TagEntryAsync(string entryId, string tagId)` - Tag an entry
- `UntagEntryAsync(string entryId, string tagId)` - Remove tag from entry
- `ChangeTagLabelAsync(string tagId, string newLabel)` - Change tag label
- `DeleteTagAsync(string tagId)` - Delete a tag

### ✅ Subscriptions API
- `GetSubscriptionsAsync()` - Get all subscriptions
- `SubscribeToFeedAsync(string feedId, List<string>? categoryIds)` - Subscribe to a feed
- `UpdateSubscriptionAsync(Subscription subscription)` - Update subscription
- `UnsubscribeFromFeedAsync(string feedId)` - Unsubscribe from a feed

### ✅ Search API
- `SearchFeedsAsync(SearchOptions options)` - Search feeds with options
- `SearchFeedsAsync(string query, int count, string? locale)` - Quick search feeds

## Advanced Usage

### Stream Continuation

```csharp
var streamOptions = new StreamOptions 
{ 
    StreamId = "feed/http://feeds.feedburner.com/ScottHanselman",
    Count = 100 
};

await foreach (var stream in client.GetStreamAsContinuationAsync(streamOptions))
{
    foreach (var entry in stream.Items)
    {
        Console.WriteLine(entry.Title);
    }
    
    if (string.IsNullOrEmpty(stream.Continuation))
        break;
}
```

### Marking Entries

```csharp
var marker = new Marker
{
    Action = MarkerAction.MarkAsRead,
    Type = MarkerType.Entries,
    EntryIds = new List<string> { "entry-id-1", "entry-id-2" }
};

await client.MarkEntriesAsync(marker);
```

### Error Handling

```csharp
try
{
    var profile = await client.GetProfileAsync();
}
catch (FeedlyException ex)
{
    Console.WriteLine($"Error Code: {ex.Error.ErrorCode}");
    Console.WriteLine($"Error Message: {ex.Error.ErrorMessage}");
}
```

## Performance Optimizations

- **Source Generators**: JSON serialization metadata generated at compile-time
- **Connection Pooling**: Up to 20 concurrent connections per server
- **HTTP/2 Support**: Multiple HTTP/2 connections enabled
- **AOT Compatible**: Can be compiled to native code for maximum performance
- **Thread-Safe**: All operations are thread-safe and can be called concurrently

## AOT Support

FeedlySharp is fully compatible with Native AOT compilation:

```xml
<PropertyGroup>
  <PublishAot>true</PublishAot>
  <InvariantGlobalization>true</InvariantGlobalization>
</PropertyGroup>
```

## Thread Safety

All methods are thread-safe and can be called concurrently from multiple threads. The HttpClient instance is designed to be reused across requests.

## Requirements

- .NET 10.0 or later
- Valid Feedly API access token

## License

See LICENSE file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Links

- [Feedly API Documentation](https://developer.feedly.com/)
- [NuGet Package](https://www.nuget.org/packages/FeedlySharp/)
- [GitHub Repository](https://github.com/Zettersten/FeedlySharp)
