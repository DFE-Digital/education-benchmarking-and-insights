using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App.Controllers.Api;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Xunit;

namespace Web.Tests.Controllers.Api.HighNeeds;

public class WhenHighNeedsApiReceivesComparisonRequest
{
    private readonly HighNeedsProxyController _api;
    private readonly Mock<ILocalAuthoritiesApi> _localAuthoritiesApi = new();
    private readonly Fixture _fixture;
    private readonly NullLogger<HighNeedsProxyController> _logger = new();

    public WhenHighNeedsApiReceivesComparisonRequest()
    {
        _api = new HighNeedsProxyController(_logger, _localAuthoritiesApi.Object);
        _fixture = new Fixture();
    }

    [Theory]
    [InlineData("code", new[] { "123", "456", "789" }, "?code=code&code=123&code=456&code=789&dimension=PerPupil")]
    public async Task ShouldGetComparisonHighNeedsForLocalAuthority(string code, string[] set, string expectedQuery)
    {
        // arrange
        var actualQuery = string.Empty;
        var cancellationToken = CancellationToken.None;
        var results = _fixture.Build<LocalAuthority<App.Domain.LocalAuthorities.HighNeeds>>()
            .CreateMany()
            .ToArray();

        _localAuthoritiesApi
            .Setup(e => e.GetHighNeeds(It.IsAny<ApiQuery?>(), cancellationToken))
            .Callback<ApiQuery?, CancellationToken>((query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(results));

        // act
        var result = await _api.Comparison(code, set, cancellationToken);

        // assert
        dynamic? json = Assert.IsType<JsonResult>(result).Value;
        LocalAuthorityHighNeedsComparisonResponse[] actual = Assert.IsType<LocalAuthorityHighNeedsComparisonResponse[]>(json);
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