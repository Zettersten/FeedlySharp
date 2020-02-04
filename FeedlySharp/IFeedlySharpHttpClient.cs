using System.Collections.Generic;
using System.Threading.Tasks;
using FeedlySharp.Models;

namespace FeedlySharp
{
    public interface IFeedlySharpHttpClient
    {
        #region Entry API (https://developer.feedly.com/v3/entries/)

        Task<Entry> GetEntryContents(string id);

        Task<List<Entry>> GetEntryContents(params string[] ids);

        Task<List<string>> CreateAndTagEntry(Entry entry);

        #endregion Entry API (https://developer.feedly.com/v3/entries/)

        #region Profile API (https://developer.feedly.com/v3/profile/)

        Task<Profile> GetProfile();

        Task<Profile> UpdateProfile(Profile profile);

        #endregion Profile API (https://developer.feedly.com/v3/profile/)

        #region Collections API (https://developer.feedly.com/v3/collections/)

        Task<List<Collection>> GetCollection();

        Task<Collection> GetCollection(string id);

        Task<Collection> CreateCollection(Collection collection);

        Task<Collection> UpdateCollectionCover(string id, byte[] cover);

        Task<Collection> AddPersonalFeedToCollection(string id, string feedId);

        Task<Collection> AddPersonalFeedToCollection(string id, List<string> feedIds);

        Task<Collection> RemovePersonalFeedFromCollection(string id, string feedId);

        Task<Collection> RemovePersonalFeedFromCollection(string id, List<string> feedIds);

        #endregion Collections API (https://developer.feedly.com/v3/collections/)

        #region Stream API (https://developer.feedly.com/v3/streams/)

        Task<Stream> GetStream(StreamOptions streamOptions = null);

        IAsyncEnumerable<Stream> GetStreamAsContiuation(StreamOptions streamOptions = null);

        Task<StreamId> GetStreamIds(StreamOptions streamOptions = null);

        IAsyncEnumerable<StreamId> GetStreamIdsAsContiuation(StreamOptions streamOptions = null);

        #endregion Stream API (https://developer.feedly.com/v3/streams/)

        FeedlyOptions Options { get; }
    }
}
