using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App.Controllers.Api;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.NonFinancial;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.NonFinancial;
using Xunit;

namespace Web.Tests.Controllers.Api.EducationHealthCarePlans;

public class WhenEducationHealthCarePlansApiReceivesHistoryRequest
{
    private readonly EducationHealthCarePlansProxyController _api;
    private readonly Mock<IEducationHealthCarePlansApi> _educationHealthCarePlansApi = new();
    private readonly Fixture _fixture;
    private readonly NullLogger<EducationHealthCarePlansProxyController> _logger = new();

    public WhenEducationHealthCarePlansApiReceivesHistoryRequest()
    {
        _api = new EducationHealthCarePlansProxyController(_logger, _educationHealthCarePlansApi.Object);
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData("code", "?code=code&dimension=Per1000Pupil")]
    public async Task ShouldGetHistoryEhcpForLocalAuthority(string code, string expectedQuery)
    {
        // arrange
        var actualQuery = string.Empty;
        var cancellationToken = CancellationToken.None;
        var results = _fixture.Build<EducationHealthCarePlansHistory<LocalAuthorityNumberOfPlansYear>>()
            .With(x => x.StartYear, 2000)
            .With(x => x.EndYear, 2005)
            .Create();

        _educationHealthCarePlansApi
            .Setup(e => e.GetEducationHealthCarePlansHistory(It.IsAny<ApiQuery?>(), cancellationToken))
            .Callback<ApiQuery?, CancellationToken>((query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var result = await _api.History(code, cancellationToken);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(result).Value;
        EducationHealthCarePlansHistoryResponse[] actual = Assert.IsType<EducationHealthCarePlansHistoryResponse[]>(json);
        Assert.Equal(results.EndYear - results.StartYear + 1, actual.Length);
        Assert.Equal(expectedQuery, actualQuery);
    }
}