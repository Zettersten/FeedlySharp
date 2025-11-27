using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FeedlySharp.Internal;
using FeedlySharp.Json;
using FeedlySharp.Models;
using FeedlySharp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FeedlySharp;

/// <summary>
/// Thread-safe HTTP client for Feedly Cloud API
/// </summary>
public sealed class FeedlySharpHttpClient : IFeedlySharpHttpClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FeedlySharpHttpClient> _logger;
    private readonly FeedlyOptions _options;
    private readonly JsonSerializerOptions _jsonOptions;
    private bool _disposed;

    public FeedlyOptions Options => _options;

    public FeedlySharpHttpClient(IConfiguration configuration) 
        : this(FeedlyOptions.Create(
            configuration[$"Feedly:{nameof(FeedlyOptions.AccessToken)}"] ?? throw new ArgumentNullException(nameof(configuration)),
            configuration[$"Feedly:{nameof(FeedlyOptions.RefreshToken)}"],
            configuration[$"Feedly:{nameof(FeedlyOptions.UserID)}"],
            configuration[$"Feedly:{nameof(FeedlyOptions.Domain)}"] ?? "https://cloud.feedly.com/"))
    {
    }

    public FeedlySharpHttpClient(IOptions<FeedlyOptions> options) : this(options.Value)
    {
    }

    public FeedlySharpHttpClient(FeedlyOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        ArgumentException.ThrowIfNullOrWhiteSpace(_options.AccessToken);

        _logger = FeedlyHttpClientLogging.CreateLogger();
        _jsonOptions = FeedlyJsonSerializer.Options;

        var handler = new SocketsHttpHandler
        {
            MaxConnectionsPerServer = 20,
            AllowAutoRedirect = true,
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
            EnableMultipleHttp2Connections = true
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(_options.Domain.TrimEnd('/') + '/'),
            Timeout = TimeSpan.FromSeconds(30)
        };

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.AccessToken);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"FeedlySharp/3.0.0");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    #region Entry API

    public async Task<Entry?> GetEntryContentsAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        var result = await GetAsync<List<Entry>>($"v3/entries/{Uri.EscapeDataString(id)}", cancellationToken).ConfigureAwait(false);
        return result.FirstOrDefault();
    }

    public async Task<List<Entry>> GetEntryContentsAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ids);
        var idsList = ids.ToList();
        if (idsList.Count == 0)
            return [];

        return await PostAsync<List<Entry>>("v3/entries/.mget", idsList, cancellationToken).ConfigureAwait(false);
    }

    public async Task<List<string>> CreateAndTagEntryAsync(Entry entry, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entry);
        return await PostAsync<List<string>>("v3/entries", entry, cancellationToken).ConfigureAwait(false);
    }

    #endregion Entry API

    #region Profile API

    public Task<Profile> GetProfileAsync(CancellationToken cancellationToken = default)
        => GetAsync<Profile>("v3/profile", cancellationToken);

    public Task<Profile> UpdateProfileAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(profile);
        return PostAsync<Profile>("v3/profile", profile, cancellationToken);
    }

    #endregion Profile API

    #region Collections API

    public Task<List<Collection>> GetCollectionsAsync(CancellationToken cancellationToken = default)
        => GetAsync<List<Collection>>("v3/collections?withStatus=true", cancellationToken);

    public Task<Collection> GetCollectionAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return GetAsync<Collection>($"v3/collections/{Uri.EscapeDataString(id)}?withStatus=true", cancellationToken);
    }

    public Task<Collection> CreateCollectionAsync(Collection collection, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(collection);
        return PostAsync<Collection>("v3/collections", collection, cancellationToken);
    }

    public async Task<Collection> UpdateCollectionCoverAsync(string id, byte[] cover, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(cover);

        var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(cover), "cover", "cover.jpg");
        
        return await PutAsync<Collection>($"v3/collections/{Uri.EscapeDataString(id)}/cover", content, cancellationToken).ConfigureAwait(false);
    }

    public Task<Collection> AddPersonalFeedToCollectionAsync(string id, string feedId, CancellationToken cancellationToken = default)
        => AddPersonalFeedToCollectionAsync(id, [feedId], cancellationToken);

    public async Task<Collection> AddPersonalFeedToCollectionAsync(string id, IEnumerable<string> feedIds, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(feedIds);

        var feedIdsList = feedIds.ToList();
        if (feedIdsList.Count == 0)
            throw new ArgumentException("At least one feed ID is required", nameof(feedIds));

        return await PutAsync<Collection>($"v3/collections/{Uri.EscapeDataString(id)}/feeds", feedIdsList, cancellationToken).ConfigureAwait(false);
    }

    public Task<Collection> RemovePersonalFeedFromCollectionAsync(string id, string feedId, CancellationToken cancellationToken = default)
        => RemovePersonalFeedFromCollectionAsync(id, [feedId], cancellationToken);

    public async Task<Collection> RemovePersonalFeedFromCollectionAsync(string id, IEnumerable<string> feedIds, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(feedIds);

        var feedIdsList = feedIds.ToList();
        if (feedIdsList.Count == 0)
            throw new ArgumentException("At least one feed ID is required", nameof(feedIds));

        return await DeleteAsync<Collection>($"v3/collections/{Uri.EscapeDataString(id)}/feeds", feedIdsList, cancellationToken).ConfigureAwait(false);
    }

    public Task DeleteCollectionAsync(string id, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return DeleteAsync($"v3/collections/{Uri.EscapeDataString(id)}", cancellationToken);
    }

    #endregion Collections API

    #region Stream API

    public Task<Stream> GetStreamAsync(StreamOptions? streamOptions = null, CancellationToken cancellationToken = default)
    {
        streamOptions ??= new StreamOptions { Count = 500 };
        if (string.IsNullOrEmpty(streamOptions.Continuation))
            streamOptions = streamOptions with { Count = 500 };

        return GetAsync<Stream>($"v3/streams/contents{streamOptions}", cancellationToken);
    }

    public async IAsyncEnumerable<Stream> GetStreamAsContinuationAsync(StreamOptions? streamOptions = null, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        streamOptions ??= new StreamOptions();
        Stream? result = null;

        do
        {
            if (result != null && !string.IsNullOrEmpty(result.Continuation))
            {
                streamOptions = streamOptions with { Continuation = result.Continuation };
            }

            result = await GetStreamAsync(streamOptions, cancellationToken).ConfigureAwait(false);
            yield return result;
        } while (!string.IsNullOrEmpty(result.Continuation));
    }

    public Task<StreamId> GetStreamIdsAsync(StreamOptions? streamOptions = null, CancellationToken cancellationToken = default)
    {
        streamOptions ??= new StreamOptions { Count = 500 };
        if (string.IsNullOrEmpty(streamOptions.Continuation))
            streamOptions = streamOptions with { Count = 500 };

        return GetAsync<StreamId>($"v3/streams/ids{streamOptions}", cancellationToken);
    }

    public async IAsyncEnumerable<StreamId> GetStreamIdsAsContinuationAsync(StreamOptions? streamOptions = null, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        streamOptions ??= new StreamOptions();
        StreamId? result = null;

        do
        {
            if (result != null && !string.IsNullOrEmpty(result.Continuation))
            {
                streamOptions = streamOptions with { Continuation = result.Continuation };
            }

            result = await GetStreamIdsAsync(streamOptions, cancellationToken).ConfigureAwait(false);
            yield return result;
        } while (!string.IsNullOrEmpty(result.Continuation));
    }

    #endregion Stream API

    #region Feed API

    public Task<Feed> GetFeedAsync(string feedId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(feedId);
        return GetAsync<Feed>($"v3/feeds/{Uri.EscapeDataString(feedId)}", cancellationToken);
    }

    public async Task<List<Feed>> GetFeedsAsync(IEnumerable<string> feedIds, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(feedIds);
        var feedIdsList = feedIds.ToList();
        if (feedIdsList.Count == 0)
            return [];

        return await PostAsync<List<Feed>>("v3/feeds/.mget", feedIdsList, cancellationToken).ConfigureAwait(false);
    }

    public Task<FeedMetadata> GetFeedMetadataAsync(string feedId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(feedId);
        return GetAsync<FeedMetadata>($"v3/feeds/{Uri.EscapeDataString(feedId)}/metadata", cancellationToken);
    }

    #endregion Feed API

    #region Marker API

    public Task MarkEntriesAsync(Marker marker, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(marker);
        return PostAsync<object>("v3/markers", marker, cancellationToken);
    }

    public Task<List<UnreadCount>> GetUnreadCountsAsync(CancellationToken cancellationToken = default)
        => GetAsync<List<UnreadCount>>("v3/markers/counts", cancellationToken);

    public Task<List<ReadEntry>> GetReadsAsync(string? streamId = null, int? count = null, string? newerThan = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder("v3/markers/reads?");
        if (!string.IsNullOrWhiteSpace(streamId))
            query.Append($"streamId={Uri.EscapeDataString(streamId)}&");
        if (count.HasValue)
            query.Append($"count={count}&");
        if (!string.IsNullOrWhiteSpace(newerThan))
            query.Append($"newerThan={Uri.EscapeDataString(newerThan)}&");

        var url = query.ToString().TrimEnd('&', '?');
        return GetAsync<List<ReadEntry>>(url, cancellationToken);
    }

    #endregion Marker API

    #region Mix API

    public Task<Mix> GetMixAsync(string mixId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(mixId);
        return GetAsync<Mix>($"v3/mixes/{Uri.EscapeDataString(mixId)}", cancellationToken);
    }

    public Task<List<Mix>> GetMixesAsync(CancellationToken cancellationToken = default)
        => GetAsync<List<Mix>>("v3/mixes", cancellationToken);

    #endregion Mix API

    #region Preference API

    public Task<List<Preference>> GetPreferencesAsync(CancellationToken cancellationToken = default)
        => GetAsync<List<Preference>>("v3/preferences", cancellationToken);

    public Task<Preference> GetPreferenceAsync(string key, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        return GetAsync<Preference>($"v3/preferences/{Uri.EscapeDataString(key)}", cancellationToken);
    }

    public Task UpdatePreferencesAsync(List<Preference> preferences, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(preferences);
        return PutAsync<object>("v3/preferences", preferences, cancellationToken);
    }

    #endregion Preference API

    #region Priority API

    public Task<List<Priority>> GetPrioritiesAsync(CancellationToken cancellationToken = default)
        => GetAsync<List<Priority>>("v3/priorities", cancellationToken);

    public Task<Priority> CreatePriorityAsync(Priority priority, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(priority);
        return PostAsync<Priority>("v3/priorities", priority, cancellationToken);
    }

    public Task DeletePriorityAsync(string priorityId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(priorityId);
        return DeleteAsync($"v3/priorities/{Uri.EscapeDataString(priorityId)}", cancellationToken);
    }

    #endregion Priority API

    #region Notes & Highlights API

    public Task<Note> CreateNoteAsync(Note note, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(note);
        return PostAsync<Note>("v3/notes", note, cancellationToken);
    }

    public Task<Note> GetNoteAsync(string entryId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entryId);
        return GetAsync<Note>($"v3/notes/{Uri.EscapeDataString(entryId)}", cancellationToken);
    }

    public Task DeleteNoteAsync(string entryId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entryId);
        return DeleteAsync($"v3/notes/{Uri.EscapeDataString(entryId)}", cancellationToken);
    }

    #endregion Notes & Highlights API

    #region Tags API

    public Task<List<Tag>> GetTagsAsync(CancellationToken cancellationToken = default)
        => GetAsync<List<Tag>>("v3/tags", cancellationToken);

    public Task<Tag> TagEntryAsync(string entryId, string tagId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entryId);
        ArgumentException.ThrowIfNullOrWhiteSpace(tagId);
        return PutAsync<Tag>($"v3/tags/{Uri.EscapeDataString(tagId)}/{Uri.EscapeDataString(entryId)}", null, cancellationToken);
    }

    public Task UntagEntryAsync(string entryId, string tagId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entryId);
        ArgumentException.ThrowIfNullOrWhiteSpace(tagId);
        return DeleteAsync($"v3/tags/{Uri.EscapeDataString(tagId)}/{Uri.EscapeDataString(entryId)}", cancellationToken);
    }

    public Task ChangeTagLabelAsync(string tagId, string newLabel, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tagId);
        ArgumentException.ThrowIfNullOrWhiteSpace(newLabel);
        return PostAsync<Tag>($"v3/tags/{Uri.EscapeDataString(tagId)}", new { label = newLabel }, cancellationToken);
    }

    public Task DeleteTagAsync(string tagId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tagId);
        return DeleteAsync($"v3/tags/{Uri.EscapeDataString(tagId)}", cancellationToken);
    }

    #endregion Tags API

    #region Subscriptions API

    public Task<List<Subscription>> GetSubscriptionsAsync(CancellationToken cancellationToken = default)
        => GetAsync<List<Subscription>>("v3/subscriptions", cancellationToken);

    public Task<Subscription> SubscribeToFeedAsync(string feedId, List<string>? categoryIds = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(feedId);
        var subscription = new { id = feedId, categories = categoryIds ?? [] };
        return PostAsync<Subscription>("v3/subscriptions", subscription, cancellationToken);
    }

    public Task<Subscription> UpdateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subscription);
        return PostAsync<Subscription>("v3/subscriptions", subscription, cancellationToken);
    }

    public Task UnsubscribeFromFeedAsync(string feedId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(feedId);
        return DeleteAsync($"v3/subscriptions/{Uri.EscapeDataString(feedId)}", cancellationToken);
    }

    #endregion Subscriptions API

    #region Search API

    public Task<SearchResult> SearchFeedsAsync(SearchOptions options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(options);
        var query = new StringBuilder("v3/search/feeds?");
        query.Append($"q={Uri.EscapeDataString(options.Query)}");
        query.Append($"&count={options.Count}");
        if (!string.IsNullOrWhiteSpace(options.Locale))
            query.Append($"&locale={Uri.EscapeDataString(options.Locale)}");
        if (!string.IsNullOrWhiteSpace(options.Continuation))
            query.Append($"&continuation={Uri.EscapeDataString(options.Continuation)}");

        return GetAsync<SearchResult>(query.ToString(), cancellationToken);
    }

    public Task<SearchResult> SearchFeedsAsync(string query, int count = 20, string? locale = null, CancellationToken cancellationToken = default)
        => SearchFeedsAsync(new SearchOptions { Query = query, Count = count, Locale = locale }, cancellationToken);

    #endregion Search API

    #region HTTP Methods

    private async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken)
    {
        _logger.LogDebug("[FeedlySharpHttpClient - GetAsync]: Calling {Endpoint}", endpoint);

        using var response = await _httpClient.GetAsync(endpoint, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> PostAsync<T>(string endpoint, object? content, CancellationToken cancellationToken)
    {
        _logger.LogDebug("[FeedlySharpHttpClient - PostAsync]: Calling {Endpoint}", endpoint);

        HttpContent? httpContent = null;
        if (content != null)
        {
            var json = JsonSerializerHelper.Serialize(content);
            httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        }

        using var response = await _httpClient.PostAsync(endpoint, httpContent, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> PutAsync<T>(string endpoint, object? content, CancellationToken cancellationToken)
    {
        _logger.LogDebug("[FeedlySharpHttpClient - PutAsync]: Calling {Endpoint}", endpoint);

        HttpContent? httpContent = null;
        if (content != null)
        {
            if (content is HttpContent httpContentDirect)
            {
                httpContent = httpContentDirect;
            }
            else
            {
                var json = JsonSerializerHelper.Serialize(content);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }
        }

        using var response = await _httpClient.PutAsync(endpoint, httpContent, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> DeleteAsync<T>(string endpoint, object? content, CancellationToken cancellationToken)
    {
        _logger.LogDebug("[FeedlySharpHttpClient - DeleteAsync]: Calling {Endpoint}", endpoint);

        HttpRequestMessage? request = null;
        if (content != null)
        {
            request = new HttpRequestMessage(HttpMethod.Delete, endpoint)
            {
                Content = new StringContent(JsonSerializerHelper.Serialize(content), Encoding.UTF8, "application/json")
            };
        }

        using var response = request != null
            ? await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false)
            : await _httpClient.DeleteAsync(endpoint, cancellationToken).ConfigureAwait(false);

        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task DeleteAsync(string endpoint, CancellationToken cancellationToken)
    {
        _logger.LogDebug("[FeedlySharpHttpClient - DeleteAsync]: Calling {Endpoint}", endpoint);

        using var response = await _httpClient.DeleteAsync(endpoint, cancellationToken).ConfigureAwait(false);
        await HandleResponseAsync<object>(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            Error? error = null;
            try
            {
                error = JsonSerializer.Deserialize(content, FeedlyJsonContext.Default.Error);
            }
            catch
            {
                // Ignore deserialization errors
            }

            _logger.LogError(
                "[FeedlySharpHttpClient - Error]: Status {StatusCode}, Endpoint: {RequestUri}, Error: {Error}",
                response.StatusCode,
                response.RequestMessage?.RequestUri,
                error?.ToString() ?? content);

            throw error != null
                ? new FeedlyException(error)
                : new FeedlyException(new Error
                {
                    ErrorCode = (int)response.StatusCode,
                    ErrorId = response.StatusCode.ToString(),
                    ErrorMessage = content
                });
        }

        if (typeof(T) == typeof(object) || string.IsNullOrWhiteSpace(content))
            return default!;

        try
        {
            // Use source-generated context when possible for better performance
            return JsonSerializer.Deserialize<T>(content, _jsonOptions) ?? throw new InvalidOperationException("Deserialization returned null");
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "[FeedlySharpHttpClient - Deserialization Error]: Content: {Content}", content);
            throw;
        }
    }

    #endregion HTTP Methods

    public void Dispose()
    {
        if (_disposed)
            return;

        _httpClient.Dispose();
        _disposed = true;
    }
}
