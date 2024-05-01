using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Platform.Infrastructure.Search;
using Platform.Search.Builders;

namespace Platform.Search.LocalAuthority;

public class LocalAuthorityIndexBuilder : IndexBuilder
{
    public override string Name => SearchResourceNames.Indexes.LocalAuthority;

    public override async Task Build(SearchIndexClient client)
    {
        var searchFields = new FieldBuilder().Build(typeof(LocalAuthorityIndex));
        var definition = new SearchIndex(Name, searchFields);
        var suggestFields = new[]
        {
            nameof(LocalAuthorityIndex.Name),
            nameof(LocalAuthorityIndex.Code)
        };

        var suggester = new SearchSuggester(SearchResourceNames.Suggesters.LocalAuthority, suggestFields);
        definition.Suggesters.Add(suggester);
        await client.CreateOrUpdateIndexAsync(definition);
    }
}