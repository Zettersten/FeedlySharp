using FeedlySharp.Models;

namespace FeedlySharp;

/// <summary>
/// Feedly API client interface
/// </summary>
public interface IFeedlySharpHttpClient
{
    FeedlyOptions Options { get; }

    #region Entry API (https://developer.feedly.com/v3/entries/)

    Task<Entry?> GetEntryContentsAsync(string id, CancellationToken cancellationToken = default);
    Task<List<Entry>> GetEntryContentsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default);
    Task<List<string>> CreateAndTagEntryAsync(Entry entry, CancellationToken cancellationToken = default);

    #endregion Entry API

    #region Profile API (https://developer.feedly.com/v3/profile/)

    Task<Profile> GetProfileAsync(CancellationToken cancellationToken = default);
    Task<Profile> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken = default);

    #endregion Profile API

    #region Collections API (https://developer.feedly.com/v3/collections/)

    Task<List<Collection>> GetCollectionsAsync(CancellationToken cancellationToken = default);
    Task<Collection> GetCollectionAsync(string id, CancellationToken cancellationToken = default);
    Task<Collection> CreateCollectionAsync(Collection collection, CancellationToken cancellationToken = default);
    Task<Collection> UpdateCollectionCoverAsync(string id, byte[] cover, CancellationToken cancellationToken = default);
    Task<Collection> AddPersonalFeedToCollectionAsync(string id, string feedId, CancellationToken cancellationToken = default);
    Task<Collection> AddPersonalFeedToCollectionAsync(string id, IEnumerable<string> feedIds, CancellationToken cancellationToken = default);
    Task<Collection> RemovePersonalFeedFromCollectionAsync(string id, string feedId, CancellationToken cancellationToken = default);
    Task<Collection> RemovePersonalFeedFromCollectionAsync(string id, IEnumerable<string> feedIds, CancellationToken cancellationToken = default);
    Task DeleteCollectionAsync(string id, CancellationToken cancellationToken = default);

    #endregion Collections API

    #region Stream API (https://developer.feedly.com/v3/streams/)

    Task<Stream> GetStreamAsync(StreamOptions? streamOptions = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<Stream> GetStreamAsContinuationAsync(StreamOptions? streamOptions = null, CancellationToken cancellationToken = default);
    Task<StreamId> GetStreamIdsAsync(StreamOptions? streamOptions = null, CancellationToken cancellationToken = default);
    IAsyncEnumerable<StreamId> GetStreamIdsAsContinuationAsync(StreamOptions? streamOptions = null, CancellationToken cancellationToken = default);

    #endregion Stream API

    #region Feed API (https://developer.feedly.com/v3/feeds/)

    Task<Feed> GetFeedAsync(string feedId, CancellationToken cancellationToken = default);
    Task<List<Feed>> GetFeedsAsync(IEnumerable<string> feedIds, CancellationToken cancellationToken = default);
    Task<FeedMetadata> GetFeedMetadataAsync(string feedId, CancellationToken cancellationToken = default);

    #endregion Feed API

    #region Marker API (https://developer.feedly.com/v3/markers/)

    Task MarkEntriesAsync(Marker marker, CancellationToken cancellationToken = default);
    Task<List<UnreadCount>> GetUnreadCountsAsync(CancellationToken cancellationToken = default);
    Task<List<ReadEntry>> GetReadsAsync(string? streamId = null, int? count = null, string? newerThan = null, CancellationToken cancellationToken = default);

    #endregion Marker API

    #region Mix API (https://developer.feedly.com/v3/mixes/)

    Task<Mix> GetMixAsync(string mixId, CancellationToken cancellationToken = default);
    Task<List<Mix>> GetMixesAsync(CancellationToken cancellationToken = default);

    #endregion Mix API

    #region Preference API (https://developer.feedly.com/v3/preferences/)

    Task<List<Preference>> GetPreferencesAsync(CancellationToken cancellationToken = default);
    Task<Preference> GetPreferenceAsync(string key, CancellationToken cancellationToken = default);
    Task UpdatePreferencesAsync(List<Preference> preferences, CancellationToken cancellationToken = default);

    #endregion Preference API

    #region Priority API (https://developer.feedly.com/v3/priorities/)

    Task<List<Priority>> GetPrioritiesAsync(CancellationToken cancellationToken = default);
    Task<Priority> CreatePriorityAsync(Priority priority, CancellationToken cancellationToken = default);
    Task DeletePriorityAsync(string priorityId, CancellationToken cancellationToken = default);

    #endregion Priority API

    #region Notes & Highlights API (https://developer.feedly.com/v3/notes/)

    Task<Note> CreateNoteAsync(Note note, CancellationToken cancellationToken = default);
    Task<Note> GetNoteAsync(string entryId, CancellationToken cancellationToken = default);
    Task DeleteNoteAsync(string entryId, CancellationToken cancellationToken = default);

    #endregion Notes & Highlights API

    #region Tags API (https://developer.feedly.com/v3/tags/)

    Task<List<Tag>> GetTagsAsync(CancellationToken cancellationToken = default);
    Task<Tag> TagEntryAsync(string entryId, string tagId, CancellationToken cancellationToken = default);
    Task UntagEntryAsync(string entryId, string tagId, CancellationToken cancellationToken = default);
    Task ChangeTagLabelAsync(string tagId, string newLabel, CancellationToken cancellationToken = default);
    Task DeleteTagAsync(string tagId, CancellationToken cancellationToken = default);

    #endregion Tags API

    #region Subscriptions API (https://developer.feedly.com/v3/subscriptions/)

    Task<List<Subscription>> GetSubscriptionsAsync(CancellationToken cancellationToken = default);
    Task<Subscription> SubscribeToFeedAsync(string feedId, List<string>? categoryIds = null, CancellationToken cancellationToken = default);
    Task UpdateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default);
    Task UnsubscribeFromFeedAsync(string feedId, CancellationToken cancellationToken = default);

    #endregion Subscriptions API

    #region Search API (https://developer.feedly.com/v3/search/)

    Task<SearchResult> SearchFeedsAsync(SearchOptions options, CancellationToken cancellationToken = default);
    Task<SearchResult> SearchFeedsAsync(string query, int count = 20, string? locale = null, CancellationToken cancellationToken = default);

    #endregion Search API
}
