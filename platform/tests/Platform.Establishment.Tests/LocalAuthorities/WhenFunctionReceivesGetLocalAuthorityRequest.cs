using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

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
        Service
            .Setup(d => d.GetAsync(_laCode))
            .ReturnsAsync(_localAuthority);

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<LocalAuthority>();
        Assert.NotNull(body);
        Assert.Equal(_laCode, body.Code);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        Service
            .Setup(d => d.GetAsync(_laCode))
            .ReturnsAsync((LocalAuthority?)null);

        var result = await Functions.SingleLocalAuthorityAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}