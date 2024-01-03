using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public class TrustSearchServiceOptions : SearchServiceOptions
{
}

[ExcludeFromCodeCoverage]
public class TrustSearchService : SearchService, ISearchService<Trust>
{
    private static readonly string[] Facets = { "" };
    private const string IndexName = "trust-index";
    public TrustSearchService(IOptions<TrustSearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
    }

    public Task<SearchOutput<Trust>> SearchAsync(PostSearchRequest request)
    {
        return SearchAsync<Trust>(request, CreateFilterExpression, Facets);
    }

    public Task<SuggestOutput<Trust>> SuggestAsync(PostSuggestRequest request, CancellationToken cancellationToken)
    {
        var fields = new[]
        {
            nameof(Trust.CompanyNumber), 
            nameof(Trust.Name)
        };
        
        return SuggestAsync<Trust>(request, cancellationToken, selectFields: fields);
    }

    private static string? CreateFilterExpression(FilterCriteria[] requestFilters)
    {
        return null;
    }
}