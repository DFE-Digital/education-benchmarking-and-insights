using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Infrastructure.Search;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public class SchoolSearchServiceOptions : SearchServiceOptions
{
}

[ExcludeFromCodeCoverage]
public class SchoolSearchService : SearchService, ISearchService<School>
{
    private static readonly string[] Facets = Array.Empty<string>();
    private const string IndexName = "school-index";
    
    public SchoolSearchService(IOptions<SchoolSearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
    }

    public Task<SearchOutput<School>> SearchAsync(PostSearchRequest request)
    {
        return SearchAsync<School>(request, CreateFilterExpression, Facets);
    }
    
    public Task<SuggestOutput<School>> SuggestAsync(PostSuggestRequest request, CancellationToken cancellationToken)
    {
        var fields = new[]
        {
            nameof(School.Urn), 
            nameof(School.Name),
            nameof(School.Town),
            nameof(School.Postcode)
        };
        
        return SuggestAsync<School>(request, cancellationToken, selectFields: fields);
    }

    private static string? CreateFilterExpression(FilterCriteria[] filters)
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