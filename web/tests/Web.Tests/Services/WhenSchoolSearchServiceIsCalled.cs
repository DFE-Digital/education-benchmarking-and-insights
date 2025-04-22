using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenSchoolSearchServiceIsCalled
{
    private readonly Mock<IEstablishmentApi> _api = new();

    public static TheoryData<string?, int?, int?, SearchFilters?, SearchOrderBy?, SearchRequest?> WhenSendRequestData = new()
    {
        {
            "term",
            null,
            null,
            null,
            null,
            new SearchRequest { SearchText = "term" }
        },
        {
            "term",
            null,
            null,
            new SearchFilters(),
            null,
            new SearchRequest { SearchText = "term" }
        },
        {
            "term",
            1,
            2,
            new SearchFilters("field", ["value1", "value2"]),
            new SearchOrderBy("field2", "value3"),
            new SearchRequest
            {
                SearchText = "term",
                PageSize = 1,
                Page = 2,
                Filters = [
                    new FilterCriteria { Field = "field", Value = "value1"},
                    new FilterCriteria { Field = "field", Value = "value2"}
                ],
                OrderBy = new OrderByCriteria { Field = "field2", Value = "value3"}
            }
        }
    };

    [Theory]
    [MemberData(nameof(WhenSendRequestData))]
    public async Task ShouldSendRequest(string? term, int? pageSize = null, int? page = null, SearchFilters? filters = null, SearchOrderBy? orderBy = null, SearchRequest? expected = null)
    {
        var response = new SearchResponse<SchoolSummary>();

        SearchRequest? actualRequest = null;
        _api.Setup(x => x.SearchSchools(It.IsAny<SearchRequest>()))
            .Callback<SearchRequest>(r =>
            {
                actualRequest = r;
            })
            .ReturnsAsync(ApiResult.Ok(response));

        var service = new SearchService(_api.Object);

        await service.SchoolSearch(term, pageSize, page, filters, orderBy);
        Assert.Equivalent(expected, actualRequest);
    }
}