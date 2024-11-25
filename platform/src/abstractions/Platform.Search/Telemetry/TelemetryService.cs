using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights;
namespace Platform.Search.Telemetry;

public interface ITelemetryService
{
    void TrackSearchEvent(SearchTelemetryProperties properties);
    void TrackSuggestEvent(SuggestTelemetryProperties properties);
}

[ExcludeFromCodeCoverage]
public class TelemetryService(TelemetryClient telemetryClient) : ITelemetryService
{
    public void TrackSearchEvent(SearchTelemetryProperties properties)
    {
        var propertiesDict = BuildDictionary(properties,
            (nameof(SearchTelemetryProperties.Facets), string.Join(",", properties.Facets)),
            (nameof(SearchTelemetryProperties.Filter), properties.Filter)
        );

        telemetryClient.TrackEvent("Search", propertiesDict);
        telemetryClient.Flush();
    }

    public void TrackSuggestEvent(SuggestTelemetryProperties properties)
    {
        var propertiesDict = BuildDictionary(properties,
            (nameof(SuggestTelemetryProperties.SuggesterName), properties.SuggesterName),
            (nameof(SuggestTelemetryProperties.Fields), string.Join(",", properties.Fields)),
            (nameof(SuggestTelemetryProperties.Filter), properties.Filter)
        );

        telemetryClient.TrackEvent("Search", propertiesDict);
        telemetryClient.Flush();
    }

    private static Dictionary<string, string?> BuildDictionary(SearchTelemetryCommonProperties properties, params (string Key, string? Value)[] merge)
    {
        var merged = new Dictionary<string, string?>
        {
            [nameof(SearchTelemetryCommonProperties.SearchId)] = properties.SearchId.ToString(),
            [nameof(SearchTelemetryCommonProperties.SearchServiceName)] = properties.SearchServiceName,
            [nameof(SearchTelemetryCommonProperties.IndexName)] = properties.IndexName,
            [nameof(SearchTelemetryCommonProperties.SearchText)] = properties.SearchText,
            [nameof(SearchTelemetryCommonProperties.ResultCount)] = properties.ResultCount.ToString()
        };

        foreach (var kvp in merge)
        {
            merged[kvp.Key] = kvp.Value;
        }

        return merged;
    }
}

public abstract record SearchTelemetryCommonProperties(Guid SearchId, string SearchServiceName, string IndexName, string? SearchText, long? ResultCount);

public record SearchTelemetryProperties(Guid SearchId, string SearchServiceName, string IndexName, IList<string> Facets, string? Filter, string? SearchText, long? ResultCount)
    : SearchTelemetryCommonProperties(SearchId, SearchServiceName, IndexName, SearchText, ResultCount);

public record SuggestTelemetryProperties(Guid SearchId, string SearchServiceName, string IndexName, string? SuggesterName, IList<string> Fields, string? Filter, string? SearchText, long? ResultCount)
    : SearchTelemetryCommonProperties(SearchId, SearchServiceName, IndexName, SearchText, ResultCount);