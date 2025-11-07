using AutoFixture;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenProgressBandingsServiceIsCalled
{
    private readonly Mock<ISchoolInsightApi> _schoolInsightApi;
    private readonly ProgressBandingsService _service;
    private readonly Fixture _fixture = new();

    public WhenProgressBandingsServiceIsCalled()
    {
        _schoolInsightApi = new Mock<ISchoolInsightApi>();
        _service = new ProgressBandingsService(_schoolInsightApi.Object);
    }

    [Fact]
    public async Task ShouldReturnNullForEmptySet()
    {
        // arrange
        var urns = Array.Empty<string>();
        var cancellationToken = CancellationToken.None;

        // act
        var result = await _service.GetKS4ProgressBandings(urns, cancellationToken);

        // assert
        _schoolInsightApi.Verify(a => a.GetCharacteristicsAsync(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        Assert.Null(result);
    }

    [Fact]
    public async Task ShouldReturnValidBandingsForValidSet()
    {
        // arrange
        var urns = _fixture.CreateMany<string>(10).ToArray();
        var cancellationToken = CancellationToken.None;

        var characteristics = _fixture
            .Build<SchoolCharacteristic>()
            .With(c => c.KS4ProgressBanding, _fixture.Create<KS4ProgressBandings.Banding>().ToStringValue())
            .CreateMany(urns.Length)
            .ToArray();

        var actualQuery = string.Empty;
        _schoolInsightApi
            .Setup(a => a.GetCharacteristicsAsync(It.IsAny<ApiQuery>(), cancellationToken))
            .Callback<ApiQuery?, CancellationToken?>((query, _) =>
            {
                actualQuery = query?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(characteristics));

        // act
        var result = await _service.GetKS4ProgressBandings(urns, cancellationToken);

        // assert
        _schoolInsightApi.Verify();
        var expected = new KS4ProgressBandings(characteristics.Select(r => new KeyValuePair<string, string?>(r.URN!, r.KS4ProgressBanding)));
        Assert.Equivalent(expected, result);
        Assert.Equal($"?urns={string.Join("&urns=", urns)}", actualQuery);
    }

    [Fact]
    public async Task ShouldReturnEmptyBandingsForInvalidResponse()
    {
        // arrange
        var urns = _fixture.CreateMany<string>(10).ToArray();
        var cancellationToken = CancellationToken.None;

        _schoolInsightApi
            .Setup(a => a.GetCharacteristicsAsync(It.IsAny<ApiQuery>(), cancellationToken))
            .ReturnsAsync(ApiResult.BadRequest());

        // act
        var result = await _service.GetKS4ProgressBandings(urns, cancellationToken);

        // assert
        _schoolInsightApi.Verify();
        Assert.Equivalent(new KS4ProgressBandings([]), result);
    }
}