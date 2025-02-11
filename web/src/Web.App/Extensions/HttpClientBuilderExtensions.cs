using System.Diagnostics.CodeAnalysis;
using System.Net;
using CorrelationId.HttpClient;
using Microsoft.Net.Http.Headers;
using Polly;
using Polly.Extensions.Http;

namespace Web.App.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder Configure<T>(this IHttpClientBuilder builder, string sectionName)
    {
        builder
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
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

    //Http retries with linear backoff
    private static AsyncPolicy<HttpResponseMessage> GetRetryPolicy(ILogger logger)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            // AB#248424 - Transient 429 errors from Platform API should be allowed to be 
            //             retried based on RetryAfter header, if present in the response
            .OrResult(response => response.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(
                5,
                (retryAttempt, response, _) =>
                {
                    var retryAfter = response.Result?.Headers.RetryAfter?.Delta;
                    if (response.Result == null || retryAfter == null)
                    {
                        return TimeSpan.FromSeconds(0.5 * retryAttempt);
                    }

                    logger.LogInformation(
                        "Response with status code {StatusCode} contains {Header} header with value {RetryAfter}",
                        (int)response.Result.StatusCode,
                        HeaderNames.RetryAfter,
                        response.Result.Headers.RetryAfter);
                    return retryAfter.Value;
                },
                (_, timespan, retryAttempt, _) =>
                {
                    logger.LogWarning("Retry attempt {RetryAttempt}. Waiting {Timespan} before next retry.", retryAttempt, timespan);
                    return Task.CompletedTask;
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