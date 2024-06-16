using System.Diagnostics.CodeAnalysis;
using CorrelationId.HttpClient;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using Web.App.Infrastructure.Apis;

namespace Web.App.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder Configure(this IHttpClientBuilder builder, string apiName)
    {
        builder
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy())
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

    //Http retries with exponential backoff
    private static AsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}