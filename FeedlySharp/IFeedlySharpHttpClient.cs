using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FeedlySharp.Models;

namespace FeedlySharp
{
    public interface IFeedlySharpHttpClient
    {
        Task<Entry> GetEntry(string id);

        Task<List<Entry>> GetEntry(params string[] ids);

        Task<Profile> GetProfile();

        Task<List<Collection>> GetCollection();

        Task<Collection> GetCollection(string id);

        Task<Stream> GetStream(StreamOptions streamOptions = null);

        IAsyncEnumerable<Stream> GetStreamAsContiuation(StreamOptions streamOptions = null);

        FeedlyOptions Options { get; }
    }
}
