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
    public static IHttpClientBuilder Configure<T>(this IHttpClientBuilder builder, string sectionName)
    {
        builder
            .SetHandlerLifetime(TimeSpan.FromMinutes(1))
            .AddPolicyHandler((serviceProvider, _) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<T>>();
                return GetRetryPolicy(logger);
            })
            .AddCorrelationIdForwarding()
            .ConfigureHttpClient((provider, client) =>
            {
                var settings = provider
                    .GetRequiredService<IConfiguration>()
                    .GetSection(sectionName)
                    .Get<ApiSettings>();

                ArgumentNullException.ThrowIfNull(settings?.Url);

                client.BaseAddress = new Uri(settings.Url);
                client.DefaultRequestHeaders.Add("x-functions-key", settings.Key);
            });

        return builder;
    }

    //Http retries with exponential backoff
    private static AsyncPolicy<HttpResponseMessage> GetRetryPolicy(ILogger logger)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                6,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    logger.LogWarning("Retry attempt {RetryAttempt} for {PolicyKey}. Waiting {Timespan} before next retry. Outcome: {StatusCode}", retryAttempt, context.PolicyKey, timespan, outcome.Result?.StatusCode);
                });
    }
}


[ExcludeFromCodeCoverage]
public class ApiSettings
{
    public string? Key { get; set; }
    public string? Url { get; set; }
}