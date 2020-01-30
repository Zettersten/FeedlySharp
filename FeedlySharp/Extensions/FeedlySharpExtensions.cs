using System.Threading.Tasks;
using FeedlySharp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace FeedlySharp.Extensions
{
    public static class FeedlySharpExtensions
    {
        public static void ThrowIfNotSuccess(this IRestResponse restResponse, ILogger logger, params object[] data)
        {
            if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = JsonConvert.DeserializeObject<Error>(restResponse.Content);

                if (data == null)
                {
                    logger.LogError(error.ToString());
                }
                else
                {
                    logger.LogError(error.ToString(), data);
                }

                throw new FeedlyException(error);
            }
        }

        public static async Task<T> GetAsync<T>(this FeedlySharpHttpClient client, string url, ILogger logger)
        {
            logger.LogDebug($"[{nameof(FeedlySharpHttpClient)} - {nameof(GetAsync)}]: Calling {url}");

            var request = new RestRequest(url, DataFormat.Json);
            var response = await client.ExecuteGetAsync<T>(request);

            response.ThrowIfNotSuccess(logger, url);

            return response.Data;
        }

        public static async Task<T> PostAsync<T>(this FeedlySharpHttpClient client, string url, ILogger logger, object data)
        {
            logger.LogDebug($"[{nameof(FeedlySharpHttpClient)} - {nameof(PostAsync)}]: Calling {url}");

            var request = new RestRequest(url, DataFormat.Json);
            
            request.AddJsonBody(data);

            var response = await client.ExecutePostAsync<T>(request);

            response.ThrowIfNotSuccess(logger, url);

            return response.Data;
        }

        public static ServiceCollection AddFeedlySharp(this ServiceCollection services)
        {
            services.AddSingleton<IFeedlySharpHttpClient, FeedlySharpHttpClient>();

            return services;
        }

        public static ServiceCollection AddFeedlySharp(this ServiceCollection services, FeedlyOptions feedlyOptions)
        {
            services.AddSingleton<IFeedlySharpHttpClient>(new FeedlySharpHttpClient(feedlyOptions));

            return services;
        }
    }
}
