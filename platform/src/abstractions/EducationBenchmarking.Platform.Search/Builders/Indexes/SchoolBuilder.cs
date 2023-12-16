using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Indexes;

namespace EducationBenchmarking.Platform.Search.Builders.Indexes;

public class SchoolBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.School;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(School));
        var definition = new SearchIndex(Name, searchFields);
        var suggestFields = new[]
        {
            nameof(School.Name),
            nameof(School.Urn),
            nameof(School.Street),
            nameof(School.Locality),
            nameof(School.Address3),
            nameof(School.Town),
            nameof(School.County),
            nameof(School.Postcode)
        };
        
        var suggester = new SearchSuggester(SearchResourceNames.Suggesters.School, suggestFields);
        definition.Suggesters.Add(suggester);
        await client.CreateOrUpdateIndexAsync(definition);
    }
}