using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Api.Establishment.Schools;
using Xunit;
namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesGetLocalAuthorityRequest : LocalAuthoritiesFunctionsTestBase
{
    private readonly string _laCode;
    private readonly LocalAuthority _localAuthority;
    private readonly IEnumerable<School> _schools;

    public WhenFunctionReceivesGetLocalAuthorityRequest()
    {
        var fixture = new Fixture();
        _laCode = fixture.Create<string>();
        _localAuthority = fixture
            .Build<LocalAuthority>()
            .With(l => l.Code, _laCode)
            .Create();
        _schools = fixture
            .Build<School>()
            .CreateMany(10);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        LocalAuthoritiesService
            .Setup(d => d.GetAsync(_laCode))
            .ReturnsAsync(_localAuthority);
        SchoolsService
            .Setup(s => s.QueryAsync(null, _laCode, null))
            .ReturnsAsync(_schools);

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var actual = await result.ReadAsJsonAsync<LocalAuthorityResponse>();
        Assert.NotNull(actual);
        Assert.Equal(_laCode, actual.Code);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        LocalAuthoritiesService
            .Setup(d => d.GetAsync(_laCode))
            .ReturnsAsync((LocalAuthority?)null);

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        LocalAuthoritiesService
            .Setup(d => d.GetAsync(_laCode))
            .Throws(new Exception());

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}