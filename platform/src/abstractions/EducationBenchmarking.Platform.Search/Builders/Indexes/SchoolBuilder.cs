using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EducationBenchmarking.Platform.Search.Indexes;

namespace EducationBenchmarking.Platform.Search.Builders.Indexes;

public class SchoolBuilder : IndexBuilder
{
    public override string Name => Names.Indexes.School;
    
    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(School));

        var definition = new SearchIndex(Name, searchFields);
        var suggester = new SearchSuggester(Names.Suggesters.School, nameof(School.Name), nameof(School.LaEstab), nameof(School.Urn));

        definition.Suggesters.Add(suggester);
        
        await client.CreateOrUpdateIndexAsync(definition);
    }
}