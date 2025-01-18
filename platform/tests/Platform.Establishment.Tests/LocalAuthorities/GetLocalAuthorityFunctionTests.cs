using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

public class GetLocalAuthorityFunctionTests : FunctionsTestBase
{
    private readonly string _laCode;
    private readonly LocalAuthority _localAuthority;
    private readonly GetLocalAuthorityFunction _functions;
    private readonly Mock<ILocalAuthoritiesService> _service;

    public GetLocalAuthorityFunctionTests()
    {
        _service = new Mock<ILocalAuthoritiesService>();
        _functions = new GetLocalAuthorityFunction(_service.Object);

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
        _service
            .Setup(d => d.GetAsync(_laCode))
            .ReturnsAsync(_localAuthority);

        var result = await _functions.RunAsync(CreateHttpRequestData(), _laCode);

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
        _service
            .Setup(d => d.GetAsync(_laCode))
            .ReturnsAsync((LocalAuthority?)null);

        var result = await _functions.RunAsync(CreateHttpRequestData(), _laCode);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}