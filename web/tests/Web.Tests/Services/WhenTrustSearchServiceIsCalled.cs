using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenTrustSearchServiceIsCalled
{
    private readonly Mock<IEstablishmentApi> _api = new();

    public static TheoryData<string?, int?, int?, (string Field, string Order)?, SearchRequest?> WhenSendRequestData = new()
    {
        {
            "term",
            null,
            null,
            null,
            new SearchRequest { SearchText = "term" }
        },
        {
            "term",
            null,
            null,
            null,
            new SearchRequest { SearchText = "term" }
        },
        {
            "term",
            1,
            2,
            ("field2", "value3"),
            new SearchRequest
            {
                SearchText = "term",
                PageSize = 1,
                Page = 2,
                OrderBy = new OrderByCriteria { Field = "field2", Value = "value3"}
            }
        }
    };

    [Theory]
    [MemberData(nameof(WhenSendRequestData))]
    public async Task ShouldSendRequest(string? term, int? pageSize = null, int? page = null, (string Field, string Order)? orderBy = null, SearchRequest? expected = null)
    {
        var response = new SearchResponse<TrustSummary>();

        SearchRequest? actualRequest = null;
        _api.Setup(x => x.SearchTrusts(It.IsAny<SearchRequest>()))
            .Callback<SearchRequest>(r =>
            {
                actualRequest = r;
            })
            .ReturnsAsync(ApiResult.Ok(response));

        var service = new SearchService(_api.Object);

        await service.TrustSearch(term, pageSize, page, orderBy);
        Assert.Equivalent(expected, actualRequest);
    }
}