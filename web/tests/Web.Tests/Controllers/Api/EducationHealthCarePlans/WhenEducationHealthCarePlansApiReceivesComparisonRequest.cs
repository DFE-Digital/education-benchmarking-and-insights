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

public class WhenEducationHealthCarePlansApiReceivesComparisonRequest
{
    private readonly EducationHealthCarePlansProxyController _api;
    private readonly Mock<IEducationHealthCarePlansApi> _educationHealthCarePlansApi = new();
    private readonly Fixture _fixture;
    private readonly NullLogger<EducationHealthCarePlansProxyController> _logger = new();

    public WhenEducationHealthCarePlansApiReceivesComparisonRequest()
    {
        _api = new EducationHealthCarePlansProxyController(_logger, _educationHealthCarePlansApi.Object);
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData("code", new[] { "123", "456", "789" }, "?code=code&code=123&code=456&code=789&dimension=Per1000Pupil")]
    public async Task ShouldGetComparisonEhcpForLocalAuthority(string code, string[] set, string expectedQuery)
    {
        // arrange
        var actualQuery = string.Empty;
        var cancellationToken = CancellationToken.None;
        var results = _fixture.Build<LocalAuthorityNumberOfPlans>()
            .CreateMany()
            .ToArray();

        _educationHealthCarePlansApi
            .Setup(e => e.GetEducationHealthCarePlans(It.IsAny<ApiQuery?>(), cancellationToken))
            .Callback<ApiQuery?, CancellationToken>((query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var result = await _api.Comparison(code, set, cancellationToken);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(result).Value;
        EducationHealthCarePlansComparisonResponse[] actual = Assert.IsType<EducationHealthCarePlansComparisonResponse[]>(json);
        Assert.Equal(results.Length, actual.Length);
        Assert.Equal(expectedQuery, actualQuery);
    }

    [Theory]
    [InlineData("code", new[] { "" })]
    [InlineData("code", new string[0])]
    public async Task ShouldReturnNotFoundForMissingSet(string code, string[] set)
    {
        // arrange
        var cancellationToken = CancellationToken.None;

        // act
        var result = await _api.Comparison(code, set, cancellationToken);

        // assert
        Assert.IsType<NotFoundResult>(result);
    }
}