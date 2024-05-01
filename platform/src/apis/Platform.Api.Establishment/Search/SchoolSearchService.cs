using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment.Search;


[ExcludeFromCodeCoverage]
public class SchoolSearchService : SearchService, ISearchService<SchoolResponseModel>
{
    private static readonly string[] Facets = Array.Empty<string>();
    private const string IndexName = SearchResourceNames.Indexes.School;

    public SchoolSearchService(IOptions<SearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
    }

    public Task<SearchResponseModel<SchoolResponseModel>> SearchAsync(PostSearchRequestModel request)
    {
        return SearchAsync<SchoolResponseModel>(request, CreateFilterExpression, Facets);
    }

    public Task<SuggestResponseModel<SchoolResponseModel>> SuggestAsync(PostSuggestRequestModel request)
    {
        var fields = new[]
        {
            nameof(SchoolResponseModel.Urn),
            nameof(SchoolResponseModel.Name),
            nameof(SchoolResponseModel.Town),
            nameof(SchoolResponseModel.Postcode)
        };

        return SuggestAsync<SchoolResponseModel>(request, selectFields: fields);
    }

    private static string? CreateFilterExpression(FilterCriteriaRequestModel[] filters)
    {
        if (filters is not { Length: > 0 })
        {
            return null;
        }

        var filterExpressions = new List<string>();
        var urnFilters = filters.Where(f => f.Field?.ToLower() == "urn").ToList();
        var nameFilters = filters.Where(f => f.Field?.ToLower() == "name").ToList();
        var laestabFilters = filters.Where(f => f.Field?.ToLower() == "laestab").ToList();

        var urnFilterValues = urnFilters.Select(f => f.Value).ToList();
        if (urnFilterValues.Count > 0)
        {
            filterExpressions.Add($"search.in(Urn, '{string.Join("|", urnFilterValues)}', '|')");
        }

        var nameFilterValues = nameFilters.Select(f => Sanitize(f.Value)).ToList();
        if (nameFilterValues.Count > 0)
        {
            filterExpressions.Add($"({string.Join(") or ( ", nameFilterValues.Select(a => $"Name eq '{a}'"))})");
        }

        var laestabFilterValues = laestabFilters.Select(f => f.Value).ToList();
        if (laestabFilterValues.Count > 0)
        {
            filterExpressions.Add($"search.in(LaEstab, '{string.Join("|", laestabFilterValues)}', '|')");
        }

        return string.Join(" or ", filterExpressions);
    }
}