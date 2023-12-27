using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Search.Builders;

namespace EducationBenchmarking.Platform.Search.Trust;

public class TrustIndexBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.Trust;
    
    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(TrustIndex));

        var definition = new SearchIndex(Name, searchFields);
        var suggester = new SearchSuggester(SearchResourceNames.Suggesters.Trust, nameof(TrustIndex.CompanyNumber), nameof(TrustIndex.Name));

        definition.Suggesters.Add(suggester);
        
        await client.CreateOrUpdateIndexAsync(definition);
    }
}