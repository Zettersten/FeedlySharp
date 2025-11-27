using System.Text.Json.Serialization;
using FeedlySharp.Models;

namespace FeedlySharp.Json;

/// <summary>
/// Source-generated JSON serialization context for AOT and performance optimization.
/// This context provides compile-time JSON serialization metadata for maximum performance.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = false,
    PropertyNameCaseInsensitive = true,
    UseStringEnumConverter = true)]
[JsonSerializable(typeof(Entry))]
[JsonSerializable(typeof(List<Entry>))]
[JsonSerializable(typeof(Profile))]
[JsonSerializable(typeof(Collection))]
[JsonSerializable(typeof(List<Collection>))]
[JsonSerializable(typeof(Stream))]
[JsonSerializable(typeof(StreamId))]
[JsonSerializable(typeof(Feed))]
[JsonSerializable(typeof(List<Feed>))]
[JsonSerializable(typeof(FeedMetadata))]
[JsonSerializable(typeof(Marker))]
[JsonSerializable(typeof(List<Marker>))]
[JsonSerializable(typeof(UnreadCount))]
[JsonSerializable(typeof(List<UnreadCount>))]
[JsonSerializable(typeof(ReadEntry))]
[JsonSerializable(typeof(List<ReadEntry>))]
[JsonSerializable(typeof(Mix))]
[JsonSerializable(typeof(List<Mix>))]
[JsonSerializable(typeof(Preference))]
[JsonSerializable(typeof(List<Preference>))]
[JsonSerializable(typeof(Priority))]
[JsonSerializable(typeof(List<Priority>))]
[JsonSerializable(typeof(Note))]
[JsonSerializable(typeof(Tag))]
[JsonSerializable(typeof(List<Tag>))]
[JsonSerializable(typeof(Subscription))]
[JsonSerializable(typeof(List<Subscription>))]
[JsonSerializable(typeof(SearchResult))]
[JsonSerializable(typeof(SearchOptions))]
[JsonSerializable(typeof(Error))]
[JsonSerializable(typeof(Resource))]
[JsonSerializable(typeof(List<Resource>))]
[JsonSerializable(typeof(Origin))]
[JsonSerializable(typeof(Summary))]
[JsonSerializable(typeof(Visual))]
[JsonSerializable(typeof(Thumbnail))]
[JsonSerializable(typeof(List<Thumbnail>))]
[JsonSerializable(typeof(Reference))]
[JsonSerializable(typeof(List<Reference>))]
[JsonSerializable(typeof(SearchTerm))]
[JsonSerializable(typeof(CardDetails))]
[JsonSerializable(typeof(Login))]
[JsonSerializable(typeof(List<Login>))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(string[]))]
[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(Dictionary<string, object>))]
[JsonSerializable(typeof(Dictionary<string, string>))]
public partial class FeedlyJsonContext : JsonSerializerContext
{
}
