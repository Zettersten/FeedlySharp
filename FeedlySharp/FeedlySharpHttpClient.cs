using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using FeedlySharp.Extensions;
using FeedlySharp.Models;
using FeedlySharp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace FeedlySharp
{
    public class FeedlySharpHttpClient : RestClient, IFeedlySharpHttpClient
    {
        private readonly ILogger logger;

        /// <summary>
        /// Uses: configuration["Feedly:AccessToken"]
        /// </summary>
        /// <param name="configuration"></param>
        public FeedlySharpHttpClient(IConfiguration configuration) : this(FeedlyOptions.Create(configuration[$"Feedly:{nameof(FeedlyOptions.AccessToken)}"], configuration[$"Feedly:{nameof(FeedlyOptions.RefreshToken)}"], configuration[$"Feedly:{nameof(FeedlyOptions.UserID)}"], configuration[$"Feedly:{nameof(FeedlyOptions.Domain)}"])) { }

        public FeedlySharpHttpClient(IOptions<FeedlyOptions> options) : this(options.Value)
        {
        }

        public FeedlySharpHttpClient(FeedlyOptions options) : this(new FeedlyAuthenticator(options))
        {
        }

        private FeedlySharpHttpClient(FeedlyAuthenticator feedlyAuthenticator) : base(feedlyAuthenticator.Options.Domain)
        {
            this.UseNewtonsoftJson(FeedlyContentSerialization.SerializerSettings);
            this.Authenticator = feedlyAuthenticator;
            this.UserAgent = $"{nameof(FeedlySharpHttpClient)}/{Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";
            this.logger = FeedlyHttpClientLogging.CreateLogger();
        }

        public FeedlyOptions Options => ((FeedlyAuthenticator)this.Authenticator).Options;

        public Task<Entry> CreateEntry(string title, string author, DateTimeOffset published, Summary content, Origin origin, List<Reference> alternate, List<Resource> tags = null, List<string> keywords = null)
        {
            if (title is null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (author is null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (origin is null)
            {
                throw new ArgumentNullException(nameof(origin));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            logger.LogDebug($"Calling {nameof(CreateEntry)}");

            throw new NotImplementedException();
        }

        public Task<List<Collection>> GetCollection()
        {
            return this.GetAsync<List<Collection>>($"v3/collections?withStatus=true", logger);
        }

        public Task<Collection> GetCollection(string id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.GetAsync<Collection>($"v3/collections/{id}?withStatus=true", logger);
        }

        public Task<Entry> GetEntry(string id)
        {
            return this.GetAsync<Entry>($"v3/entry/{id}", logger);
        }

        public Task<List<Entry>> GetEntry(params string[] ids)
        {
            logger.LogDebug($"Calling {nameof(GetEntry)}");

            throw new NotImplementedException();
        }

        public Task<Profile> GetProfile()
        {
            return this.GetAsync<Profile>("v3/profile", logger);
        }

        public Task<Stream> GetStream(StreamOptions streamOptions = null)
        {
            if (streamOptions == null)
            {
                streamOptions = new StreamOptions();
            }

            if (string.IsNullOrEmpty(streamOptions.Continuation))
            {
                streamOptions.Count = 500;
            }

            return this.GetAsync<Stream>("v3/streams/contents" + streamOptions.ToString(), logger);
        }

        public async Task<Stream> GetSaveForLaterStream(StreamOptions streamOptions = null)
        {
            if (streamOptions == null)
            {
                streamOptions = new StreamOptions();
            }

            if (string.IsNullOrEmpty(streamOptions.Continuation))
            {
                streamOptions.Count = 500;
            }

            var profile = await GetProfile();

            streamOptions.StreamId = $"user/{profile.Id}/tag/global.saved";

            return await this.GetAsync<Stream>("v3/streams/contents" + streamOptions.ToString(), logger);
        }

        public IAsyncEnumerable<Stream> GetStreamAsContiuation(StreamOptions streamOptions = null)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<Stream> GetSaveForLaterStreamAsContinuation(StreamOptions streamOptions = null)
        {
            Stream result = null;

            do
            {
                if (result != null && !string.IsNullOrEmpty(result.Continuation))
                {
                    streamOptions.Continuation = result.Continuation;
                }

                result = await GetSaveForLaterStream(streamOptions);

                yield return result;
            } while (!string.IsNullOrEmpty(result.Continuation));
        }
    }
}
