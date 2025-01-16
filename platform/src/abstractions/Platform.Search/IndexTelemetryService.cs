using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights;

namespace Platform.Search;

public interface IIndexTelemetryService
{
    void TrackEvent(SearchTelemetryProperties properties);
    void TrackEvent(SuggestTelemetryProperties properties);
}

[ExcludeFromCodeCoverage]
public class IndexTelemetryService(TelemetryClient client) : IIndexTelemetryService
{
    public void TrackEvent(SearchTelemetryProperties properties)
    {
        client.TrackEvent("Search", properties.BuildDictionary());
        client.Flush();
    }

    public void TrackEvent(SuggestTelemetryProperties properties)
    {
        client.TrackEvent("Search", properties.BuildDictionary());
        client.Flush();
    }
}

[ExcludeFromCodeCoverage]
public abstract record IndexTelemetryCommonProperties(
    Guid SearchId,
    string SearchServiceName,
    string IndexName,
    string? SearchText,
    long? ResultCount)
{
    protected Dictionary<string, string?> BuildDictionary(Dictionary<string, string?> merge)
    {
        var merged = new Dictionary<string, string?>
        {
            [nameof(SearchId)] = SearchId.ToString(),
            [nameof(SearchServiceName)] = SearchServiceName,
            [nameof(IndexName)] = IndexName,
            [nameof(SearchText)] = SearchText,
            [nameof(ResultCount)] = ResultCount.ToString()
        };

        foreach (var kvp in merge)
        {
            merged[kvp.Key] = kvp.Value;
        }

        return merged;
    }
}

[ExcludeFromCodeCoverage]
public record SearchTelemetryProperties(
    Guid SearchId,
    string SearchServiceName,
    string IndexName,
    IList<string> Facets,
    string? Filter,
    string? SearchText,
    long? ResultCount)
    : IndexTelemetryCommonProperties(SearchId, SearchServiceName, IndexName, SearchText, ResultCount)
{
    public Dictionary<string, string?> BuildDictionary()
    {
        var props = new Dictionary<string, string?>
        {
            [nameof(Facets)] = string.Join(",", Facets),
            [nameof(Filter)] = Filter
        };

        return BuildDictionary(props);
    }
}

[ExcludeFromCodeCoverage]
public record SuggestTelemetryProperties(
    Guid SearchId,
    string SearchServiceName,
    string IndexName,
    string? SuggesterName,
    IList<string> Fields,
    string? Filter,
    string? SearchText,
    long? ResultCount)
    : IndexTelemetryCommonProperties(SearchId, SearchServiceName, IndexName, SearchText, ResultCount)
{
    public Dictionary<string, string?> BuildDictionary()
    {
        var props = new Dictionary<string, string?>
        {
            [nameof(SuggesterName)] = SuggesterName,
            [nameof(Fields)] = string.Join(",", Fields),
            [nameof(Filter)] = Filter
        };

        return BuildDictionary(props);
    }
}