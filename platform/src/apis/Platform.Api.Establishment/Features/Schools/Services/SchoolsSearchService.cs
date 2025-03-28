
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Schools.Services;

public interface ISchoolsSearchService
{
    Task<SearchResponse<School>> SearchAsync(SearchRequest request);
}

[ExcludeFromCodeCoverage]
public class SchoolsSearchService(
    [FromKeyedServices(ResourceNames.Search.Indexes.SchoolFaceted)] IIndexClient client)
    : SearchService<School>(client), ISchoolsSearchService
{
    public Task<SearchResponse<School>> SearchAsync(SearchRequest request)
    {
        var facets = new[]
        {
            nameof(School.OverallPhase),
        };

        var response = SearchAsync(request, CreateFilterExpression, facets);

        return response;

        string? CreateFilterExpression(FilterCriteria[]? filterCriteriaArray)
        {
            if (filterCriteriaArray == null || filterCriteriaArray.Length == 0)
                return null;

            return $"({string.Join(" or ", filterCriteriaArray.Select(CreateFilterSingle))})";
        }

        string CreateFilterSingle(FilterCriteria filterCriteria)
        {
            return $"{filterCriteria.Field} eq '{filterCriteria.Value}'";
        }
    }
}