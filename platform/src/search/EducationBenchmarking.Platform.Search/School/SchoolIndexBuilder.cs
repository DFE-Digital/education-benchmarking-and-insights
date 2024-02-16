using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.School;

public class SchoolIndexBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.School;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(SchoolIndex));
        var definition = new SearchIndex(Name, searchFields);
        var suggestFields = new[]
        {
            nameof(SchoolIndex.Name),
            nameof(SchoolIndex.Urn),
            nameof(SchoolIndex.Street),
            nameof(SchoolIndex.Locality),
            nameof(SchoolIndex.Address3),
            nameof(SchoolIndex.Town),
            nameof(SchoolIndex.County),
            nameof(SchoolIndex.Postcode)
        };

        var suggester = new SearchSuggester(SearchResourceNames.Suggesters.School, suggestFields);
        definition.Suggesters.Add(suggester);
        await client.CreateOrUpdateIndexAsync(definition);
    }
}