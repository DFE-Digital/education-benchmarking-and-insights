using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Infrastructure.Search;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Search;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySearchServiceOptions : SearchServiceOptions;

[ExcludeFromCodeCoverage]
public class LocalAuthoritySearchService : SearchService, ISearchService<LocalAuthority>
{
    private static readonly string[] Facets = { "" };
    private const string IndexName = "local-authority-index";

    public LocalAuthoritySearchService(IOptions<LocalAuthoritySearchServiceOptions> options) : base(options.Value.Endpoint, IndexName, options.Value.Credential)
    {
    }

    public async Task<SearchOutput<LocalAuthority>> SearchAsync(PostSearchRequest request)
    {
        return await SearchAsync<LocalAuthority>(request, CreateFilterExpression, Facets);
    }

    public Task<SuggestOutput<LocalAuthority>> SuggestAsync(PostSuggestRequest request)
    {
        throw new System.NotImplementedException();
    }

    private static string? CreateFilterExpression(FilterCriteria[] requestFilters)
    {
        return null;
    }
}