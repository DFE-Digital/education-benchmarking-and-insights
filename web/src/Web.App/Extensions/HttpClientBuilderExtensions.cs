using System.Diagnostics.CodeAnalysis;
using CorrelationId.HttpClient;
using Microsoft.Extensions.Options;
using Web.App.Infrastructure.Apis;

namespace Web.App.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder ConfigureHttpClientForApi(this IHttpClientBuilder builder, string apiName)
        {
            builder
                .AddCorrelationIdForwarding()
                .ConfigureHttpClient((provider, client) =>
                {
                    var settings = provider
                        .GetRequiredService<IOptionsMonitor<ApiSettings>>()
                        .Get(apiName);

                    client.BaseAddress = new Uri(settings.Url!);
                    client.DefaultRequestHeaders.Add("x-functions-key", settings.Key);
                });

            return builder;
        }
    }
}