using System.Diagnostics.CodeAnalysis;
using System.Net;
using CorrelationId.HttpClient;
using Microsoft.Net.Http.Headers;
using Polly;
using Polly.Extensions.Http;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpClientBuilderExtensions
{
    private const string PolicyHandler = "Polly";

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
                        "{Source} response with status code {StatusCode} contains {Header} header with value {RetryAfter}",
                        PolicyHandler,
                        GetStatusCodeOrMessage(response),
                        HeaderNames.RetryAfter,
                        response.Result.Headers.RetryAfter);
                    return retryAfter.Value;
                },
                (response, timespan, retryAttempt, _) =>
                {
                    logger.LogWarning("{Source} retry attempt {RetryAttempt} due to {StatusCode}. Waiting {Timespan} before next retry.",
                        PolicyHandler,
                        retryAttempt,
                        GetStatusCodeOrMessage(response),
                        timespan);
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
                (response, timespan) =>
                {
                    logger.LogWarning("{Source} circuit broken due to {StatusCode}. Reset in {Timespan}",
                        PolicyHandler,
                        GetStatusCodeOrMessage(response),
                        timespan);
                },
                () =>
                {
                    logger.LogInformation("{Source} circuit reset", PolicyHandler);
                });
    }

    private static string GetStatusCodeOrMessage(DelegateResult<HttpResponseMessage> response) => response.Exception?.Message ?? ((int)response.Result.StatusCode).ToString();
}

[ExcludeFromCodeCoverage]
public class ApiSettings
{
    public string? Key { get; set; }
    public string? Url { get; set; }
}