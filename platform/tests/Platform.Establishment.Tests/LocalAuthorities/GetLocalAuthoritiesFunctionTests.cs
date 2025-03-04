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

public class GetLocalAuthoritiesFunctionTests : FunctionsTestBase
{
    private readonly GetLocalAuthoritiesFunction _function;
    private readonly IEnumerable<LocalAuthority> _localAuthorities;
    private readonly Mock<ILocalAuthoritiesService> _service;

    public GetLocalAuthoritiesFunctionTests()
    {
        _service = new Mock<ILocalAuthoritiesService>();
        _function = new GetLocalAuthoritiesFunction(_service.Object);

        var fixture = new Fixture();
        _localAuthorities = fixture
            .Build<LocalAuthority>()
            .CreateMany();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.GetAllAsync())
            .ReturnsAsync(_localAuthorities);

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<IEnumerable<LocalAuthority>>();
        Assert.NotNull(body);
        Assert.Equivalent(_localAuthorities, body);
    }
}