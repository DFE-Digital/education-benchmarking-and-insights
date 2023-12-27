using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.Establishment;

public class EstablishmentIndexBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.Establishment;
    
    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(EstablishmentIndex));

        var definition = new SearchIndex(Name, searchFields);
        var suggestFields = new[]
        {
            nameof(EstablishmentIndex.Name),
            nameof(EstablishmentIndex.Identifier)
        };
        var suggester = new SearchSuggester(SearchResourceNames.Suggesters.Establishment, suggestFields);

        definition.Suggesters.Add(suggester);
        
        await client.CreateOrUpdateIndexAsync(definition);
    }
}