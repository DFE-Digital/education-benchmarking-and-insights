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
            .AddPolicyHandler((serviceProvider, _) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<T>>();
                return GetCircuitBreakerPolicy(logger);
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
                3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (_, timespan, retryAttempt, _) =>
                {
                    logger.LogWarning("Retry attempt {RetryAttempt}. Waiting {Timespan} before next retry.", retryAttempt, timespan);
                });
    }

    private static AsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ILogger logger)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                5,
                TimeSpan.FromSeconds(30),
                onBreak: (_, timespan) =>
                {
                    logger.LogWarning("Circuit broken. Reset in {Timespan}", timespan);
                },
                onReset: () =>
                {
                    logger.LogInformation("Circuit reset");
                });
    }
}


[ExcludeFromCodeCoverage]
public class ApiSettings
{
    public string? Key { get; set; }
    public string? Url { get; set; }
}