using AutoFixture;
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
    private readonly Fixture _fixture = new();

    public static TheoryData<string?, int?, int?, Dictionary<string, IEnumerable<string>>?, (string Field, string Order)?, SearchRequest?> WhenSendRequestData = new()
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
            new Dictionary<string, IEnumerable<string>>(),
            null,
            new SearchRequest { SearchText = "term" }
        },
        {
            "term",
            1,
            2,
            new Dictionary<string, IEnumerable<string>>
            {
                { "field", ["value1", "value2"] }
            },
            ("field2", "value3"),
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
    public async Task ShouldSendRequest(string? term, int? pageSize = null, int? page = null, Dictionary<string, IEnumerable<string>>? filters = null, (string Field, string Order)? orderBy = null, SearchRequest? expected = null)
    {
        var response = new SearchResponse<School>();

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