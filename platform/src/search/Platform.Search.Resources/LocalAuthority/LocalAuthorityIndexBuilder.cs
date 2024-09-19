using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure;
using Platform.Search.Resources.Builders;

namespace Platform.Search.Resources.LocalAuthority;

public class LocalAuthorityIndexBuilder : IndexBuilder
{
    public override string Name => ResourceNames.Search.Indexes.LocalAuthority;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(LocalAuthorityIndex));
        var definition = new SearchIndex(Name, searchFields);
        var suggestFields = new[]
        {
            nameof(LocalAuthorityIndex.Name),
            nameof(LocalAuthorityIndex.Code)
        };

        var suggester = new SearchSuggester(ResourceNames.Search.Suggesters.LocalAuthority, suggestFields);
        definition.Suggesters.Add(suggester);
        await client.CreateOrUpdateIndexAsync(definition);
    }
}