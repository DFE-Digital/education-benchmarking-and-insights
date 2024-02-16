using System.Diagnostics.CodeAnalysis;
using CorrelationId.HttpClient;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Web.Extensions;

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