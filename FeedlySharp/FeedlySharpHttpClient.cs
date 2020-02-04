using System;
using System.Collections.Generic;
using System.Linq;
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
            this.UserAgent = $"{nameof(FeedlySharpHttpClient)}/{Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";
            this.logger = FeedlyHttpClientLogging.CreateLogger();
        }

        public FeedlyOptions Options => ((FeedlyAuthenticator)this.Authenticator).Options;

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

        public async Task<Entry> GetEntry(string id)
        {
            var result = await this.GetAsync<List<Entry>>($"v3/entries/{id}", logger);

            return result.FirstOrDefault();
        }

        public Task<List<Entry>> GetEntry(params string[] ids)
        {
            return this.PostAsync<List<Entry>>($"v3/entries/.mget", logger, ids);
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

        public async IAsyncEnumerable<Stream> GetStreamAsContiuation(StreamOptions streamOptions = null)
        {
            Stream result = null;

            do
            {
                if (result != null && !string.IsNullOrEmpty(result.Continuation))
                {
                    streamOptions.Continuation = result.Continuation;
                }

                result = await GetStream(streamOptions);

                yield return result;
            } while (!string.IsNullOrEmpty(result.Continuation));
        }
    }
}
