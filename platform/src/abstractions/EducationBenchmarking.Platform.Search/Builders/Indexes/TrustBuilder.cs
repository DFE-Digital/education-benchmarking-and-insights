using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Indexes;

namespace EducationBenchmarking.Platform.Search.Builders.Indexes;

public class TrustBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.Trust;
    
    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(Trust));

        var definition = new SearchIndex(Name, searchFields);
        var suggester = new SearchSuggester(SearchResourceNames.Suggesters.Trust, nameof(Trust.Name));

        definition.Suggesters.Add(suggester);
        
        await client.CreateOrUpdateIndexAsync(definition);
    }
}