using System.Diagnostics.CodeAnalysis;
using Azure;
using FluentValidation;
using HealthChecks.AzureSearch.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Platform.Infrastructure;
// ReSharper disable UnusedMethodReturnValue.Global

namespace Platform.Search;

[ExcludeFromCodeCoverage]
public static class SearchExtensions
{
    public static IServiceCollection AddPlatformSearch(this IServiceCollection serviceCollection)
    {
        var options = GetOptions();

        serviceCollection
            .AddSingleton(Options.Create(options))
            .AddSingleton<IIndexTelemetryService, IndexTelemetryService>()
            .AddKeyedSingleton<IIndexClient, SchoolIndexClient>(ResourceNames.Search.Indexes.School)
            .AddKeyedSingleton<IIndexClient, TrustIndexClient>(ResourceNames.Search.Indexes.Trust)
            .AddKeyedSingleton<IIndexClient, LocalAuthorityIndexClient>(ResourceNames.Search.Indexes.LocalAuthority)
            .AddKeyedSingleton<IIndexClient, SchoolComparatorsIndexClient>(ResourceNames.Search.Indexes.SchoolComparators)
            .AddKeyedSingleton<IIndexClient, TrustComparatorsIndexClient>(ResourceNames.Search.Indexes.TrustComparators)
            .AddTransient<IValidator<SuggestRequest>, PostSuggestRequestValidator>();

        return serviceCollection;
    }

    public static IHealthChecksBuilder AddPlatformSearch(this IHealthChecksBuilder builder)
    {
        var options = GetOptions();

        foreach (var index in ResourceNames.Search.Indexes.All)
        {
            builder.AddAzureSearch(s =>
                {
                    s.Endpoint = options.Endpoint.AbsoluteUri;
                    s.AuthKey = options.Key;
                    s.IndexName = index;
                }, $"azuresearch:{index}");
        }

        return builder;
    }

    private static PlatformSearchOptions GetOptions()
    {
        var name = Environment.GetEnvironmentVariable("Search__Name");
        var key = Environment.GetEnvironmentVariable("Search__Key");
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(key);

        return new PlatformSearchOptions(name, key);
    }
}

[ExcludeFromCodeCoverage]
public class PlatformSearchOptions(string name, string key)
{
    public string Key => key;
    public Uri Endpoint => new($"https://{name}.search.windows.net/");
    public AzureKeyCredential Credential => new(Key);
}

