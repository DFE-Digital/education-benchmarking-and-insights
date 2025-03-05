using System.Security.Claims;
using AutoFixture;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;
using Web.App.Services;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;

namespace Web.Tests.Services;

public class WhenUserDataServiceIsCalled
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IUserDataApi> _api = new();
    private readonly NullLogger<UserDataService> _logger = new();
    private static readonly string UserGuid = Guid.NewGuid().ToString();
    private readonly ClaimsPrincipal _authUser = new(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, UserGuid)], "authenticated"));

    [Fact]
    public async Task ShouldReturnUserDataWhenGettingSchoolComparatorSetActive()
    {
        const string urn = nameof(urn);
        string? actualQuery = null;
        var response = _fixture.Build<UserData>().CreateMany().ToArray();

        _api.Setup(api => api.GetAsync(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(q =>
            {
                actualQuery = q?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new UserDataService(_api.Object, _logger);

        var result = await service.GetSchoolComparatorSetActiveAsync(_authUser, urn);

        Assert.Equal(response.First(), result);
        Assert.Equal($"?userId={UserGuid}&type=comparator-set&organisationType=school&organisationId={urn}", actualQuery);
    }

    [Fact]
    public async Task ShouldReturnUserDataWhenGettingCustomDataActive()
    {
        const string urn = nameof(urn);
        string? actualQuery = null;
        var response = _fixture.Build<UserData>().CreateMany().ToArray();

        _api.Setup(api => api.GetAsync(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(q =>
            {
                actualQuery = q?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new UserDataService(_api.Object, _logger);

        var result = await service.GetCustomDataActiveAsync(_authUser, urn);

        Assert.Equal(response.First(), result);
        Assert.Equal($"?userId={UserGuid}&type=custom-data&organisationType=school&organisationId={urn}", actualQuery);
    }

    [Fact]
    public async Task ShouldReturnUserDataWhenGettingTrustComparatorSet()
    {
        const string companyNumber = nameof(companyNumber);
        string? actualQuery = null;
        var response = _fixture.Build<UserData>().CreateMany().ToArray();

        _api.Setup(api => api.GetAsync(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(q =>
            {
                actualQuery = q?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new UserDataService(_api.Object, _logger);

        var result = await service.GetTrustComparatorSetAsync(_authUser, companyNumber);

        Assert.Equal(response.First(), result);
        Assert.Equal($"?userId={UserGuid}&type=comparator-set&organisationType=trust&organisationId={companyNumber}", actualQuery);
    }

    [Fact]
    public async Task ShouldReturnUserDataIdsWhenGettingSchoolData()
    {
        const string urn = nameof(urn);
        string? actualQuery = null;
        var response = _fixture.Build<UserData>()
            .CreateMany()
            .ToArray();
        response.First().Type = "comparator-set";
        response.Last().Type = "custom-data";

        _api.Setup(api => api.GetAsync(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(q =>
            {
                actualQuery = q?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new UserDataService(_api.Object, _logger);

        var (customData, comparatorSet) = await service.GetSchoolDataAsync(_authUser, urn);

        Assert.Equal(response.First().Id, comparatorSet);
        Assert.Equal(response.Last().Id, customData);
        Assert.Equal($"?userId={UserGuid}&organisationType=school&organisationId={urn}", actualQuery);
    }

    [Fact]
    public async Task ShouldReturnUserDataIdsWhenGettingTrustData()
    {
        const string companyNumber = nameof(companyNumber);
        string? actualQuery = null;
        var response = _fixture.Build<UserData>()
            .CreateMany()
            .ToArray();
        response.First().Type = "comparator-set";
        response.Last().Type = "custom-data";

        _api.Setup(api => api.GetAsync(It.IsAny<ApiQuery?>()))
            .Callback<ApiQuery?>(q =>
            {
                actualQuery = q?.ToQueryString();
            })
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new UserDataService(_api.Object, _logger);

        var (customData, comparatorSet) = await service.GetTrustDataAsync(_authUser, companyNumber);

        Assert.Equal(response.First().Id, comparatorSet);
        Assert.Equal(response.Last().Id, customData);
        Assert.Equal($"?userId={UserGuid}&organisationType=trust&organisationId={companyNumber}", actualQuery);
    }
}
