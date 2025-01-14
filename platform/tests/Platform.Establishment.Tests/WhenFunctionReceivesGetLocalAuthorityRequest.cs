using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Functions.Extensions;
using Xunit;

namespace Platform.Establishment.Tests;

public class WhenFunctionReceivesGetLocalAuthorityRequest : LocalAuthoritiesFunctionsTestBase
{
    private readonly string _laCode;
    private readonly LocalAuthority _localAuthority;

    public WhenFunctionReceivesGetLocalAuthorityRequest()
    {
        var fixture = new Fixture();
        _laCode = fixture.Create<string>();
        var schools = fixture
            .Build<LocalAuthoritySchool>()
            .CreateMany(10)
            .ToArray();
        _localAuthority = fixture
            .Build<LocalAuthority>()
            .With(l => l.Code, _laCode)
            .With(l => l.Schools, schools)
            .Create();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        LocalAuthoritiesService
            .Setup(d => d.GetAsync(_laCode))
            .ReturnsAsync(_localAuthority);

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var actual = await result.ReadAsJsonAsync<LocalAuthority>();
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