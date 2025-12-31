using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App.Controllers.Api;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorityFinances;
using Xunit;

namespace Web.Tests.Controllers.Api.HighNeeds;

public class WhenHighNeedsApiReceivesHistoryRequest
{
    private readonly HighNeedsProxyController _api;
    private readonly Mock<ILocalAuthorityFinancesApi> _localAuthoritiesApi = new();
    private readonly Fixture _fixture;
    private readonly NullLogger<HighNeedsProxyController> _logger = new();

    public WhenHighNeedsApiReceivesHistoryRequest()
    {
        _api = new HighNeedsProxyController(_logger, _localAuthoritiesApi.Object);
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData("code", "?code=code&dimension=PerPupil")]
    public async Task ShouldGetHistoryHighNeedsForLocalAuthority(string code, string expectedQuery)
    {
        // arrange
        var actualQuery = string.Empty;
        var cancellationToken = CancellationToken.None;
        var results = _fixture.Build<HighNeedsHistory<HighNeedsYear>>()
            .With(x => x.StartYear, 2000)
            .With(x => x.EndYear, 2005)
            .Create();

        _localAuthoritiesApi
            .Setup(e => e.GetHighNeedsHistory(It.IsAny<ApiQuery?>(), cancellationToken))
            .Callback<ApiQuery?, CancellationToken>((query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var result = await _api.History(code, cancellationToken);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(result).Value;
        LocalAuthorityHighNeedsHistoryResponse[] actual = Assert.IsType<LocalAuthorityHighNeedsHistoryResponse[]>(json);
        Assert.Equal(results.EndYear - results.StartYear + 1, actual.Length);
        Assert.Equal(expectedQuery, actualQuery);
    }
}