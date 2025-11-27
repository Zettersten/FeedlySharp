using System.Text.Json;
using FeedlySharp.Json;
using FeedlySharp.Models;

namespace FeedlySharp.Internal;

/// <summary>
/// Internal helper for optimized JSON serialization using source generators
/// </summary>
internal static class JsonSerializerHelper
{
    /// <summary>
    /// Serialize an object using source-generated context when possible
    /// </summary>
    public static string Serialize(object? value)
    {
        if (value == null)
            return "null";

        return value switch
        {
            Entry entry => JsonSerializer.Serialize(entry, FeedlyJsonContext.Default.Entry),
            List<Entry> entries => JsonSerializer.Serialize(entries, FeedlyJsonContext.Default.ListEntry),
            Profile profile => JsonSerializer.Serialize(profile, FeedlyJsonContext.Default.Profile),
            Collection collection => JsonSerializer.Serialize(collection, FeedlyJsonContext.Default.Collection),
            List<Collection> collections => JsonSerializer.Serialize(collections, FeedlyJsonContext.Default.ListCollection),
            Stream stream => JsonSerializer.Serialize(stream, FeedlyJsonContext.Default.Stream),
            StreamId streamId => JsonSerializer.Serialize(streamId, FeedlyJsonContext.Default.StreamId),
            Feed feed => JsonSerializer.Serialize(feed, FeedlyJsonContext.Default.Feed),
            List<Feed> feeds => JsonSerializer.Serialize(feeds, FeedlyJsonContext.Default.ListFeed),
            FeedMetadata metadata => JsonSerializer.Serialize(metadata, FeedlyJsonContext.Default.FeedMetadata),
            Marker marker => JsonSerializer.Serialize(marker, FeedlyJsonContext.Default.Marker),
            List<Marker> markers => JsonSerializer.Serialize(markers, FeedlyJsonContext.Default.ListMarker),
            UnreadCount unreadCount => JsonSerializer.Serialize(unreadCount, FeedlyJsonContext.Default.UnreadCount),
            List<UnreadCount> unreadCounts => JsonSerializer.Serialize(unreadCounts, FeedlyJsonContext.Default.ListUnreadCount),
            ReadEntry readEntry => JsonSerializer.Serialize(readEntry, FeedlyJsonContext.Default.ReadEntry),
            List<ReadEntry> readEntries => JsonSerializer.Serialize(readEntries, FeedlyJsonContext.Default.ListReadEntry),
            Mix mix => JsonSerializer.Serialize(mix, FeedlyJsonContext.Default.Mix),
            List<Mix> mixes => JsonSerializer.Serialize(mixes, FeedlyJsonContext.Default.ListMix),
            Preference preference => JsonSerializer.Serialize(preference, FeedlyJsonContext.Default.Preference),
            List<Preference> preferences => JsonSerializer.Serialize(preferences, FeedlyJsonContext.Default.ListPreference),
            Priority priority => JsonSerializer.Serialize(priority, FeedlyJsonContext.Default.Priority),
            List<Priority> priorities => JsonSerializer.Serialize(priorities, FeedlyJsonContext.Default.ListPriority),
            Note note => JsonSerializer.Serialize(note, FeedlyJsonContext.Default.Note),
            Tag tag => JsonSerializer.Serialize(tag, FeedlyJsonContext.Default.Tag),
            List<Tag> tags => JsonSerializer.Serialize(tags, FeedlyJsonContext.Default.ListTag),
            Subscription subscription => JsonSerializer.Serialize(subscription, FeedlyJsonContext.Default.Subscription),
            List<Subscription> subscriptions => JsonSerializer.Serialize(subscriptions, FeedlyJsonContext.Default.ListSubscription),
            SearchResult searchResult => JsonSerializer.Serialize(searchResult, FeedlyJsonContext.Default.SearchResult),
            SearchOptions searchOptions => JsonSerializer.Serialize(searchOptions, FeedlyJsonContext.Default.SearchOptions),
            List<string> stringList => JsonSerializer.Serialize(stringList, FeedlyJsonContext.Default.ListString),
            string[] stringArray => JsonSerializer.Serialize(stringArray, FeedlyJsonContext.Default.StringArray),
            Dictionary<string, object> dict => JsonSerializer.Serialize(dict, FeedlyJsonContext.Default.DictionaryStringObject),
            Dictionary<string, string> stringDict => JsonSerializer.Serialize(stringDict, FeedlyJsonContext.Default.DictionaryStringString),
            _ => JsonSerializer.Serialize(value, FeedlyJsonSerializer.Options)
        };
    }
}
