using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.School.Features.Search.Models;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.School.Features.Search.Services;

public interface ISchoolSearchService
{
    Task<SuggestResponse<SchoolSummaryResponse>> SuggestAsync(SchoolSuggestRequest request, CancellationToken cancellationToken = default);
    Task<SearchResponse<SchoolSummaryResponse>> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class SchoolSearchService(
    [FromKeyedServices(ResourceNames.Search.Indexes.School)] IIndexClient client)
    : SearchService<SchoolSummaryResponse>(client), ISchoolSearchService
{
    public Task<SuggestResponse<SchoolSummaryResponse>> SuggestAsync(SchoolSuggestRequest request, CancellationToken cancellationToken = default)
    {
        var fields = new[]
        {
            nameof(SchoolSummaryResponse.SchoolName),
            nameof(SchoolSummaryResponse.URN),
            nameof(SchoolSummaryResponse.AddressStreet),
            nameof(SchoolSummaryResponse.AddressLocality),
            nameof(SchoolSummaryResponse.AddressLine3),
            nameof(SchoolSummaryResponse.AddressTown),
            nameof(SchoolSummaryResponse.AddressCounty),
            nameof(SchoolSummaryResponse.AddressPostcode)
        };

        return SuggestAsync(request, request.FilterExpression, fields, cancellationToken);
    }

    public Task<SearchResponse<SchoolSummaryResponse>> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default) => SearchAsync(request, request.FilterExpression, cancellationToken: cancellationToken);
}