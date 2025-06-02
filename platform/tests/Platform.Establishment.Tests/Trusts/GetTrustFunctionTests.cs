using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.Trusts;
using Platform.Api.Establishment.Features.Trusts.Functions;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Trusts;

public class GetTrustFunctionTests : FunctionsTestBase
{
    private readonly string _companyNumber;
    private readonly GetTrustFunction _function;
    private readonly Mock<ITrustsService> _service;
    private readonly Trust _trust;

    public GetTrustFunctionTests()
    {
        var fixture = new Fixture();
        _companyNumber = fixture.Create<string>();
        var schools = fixture
            .Build<TrustSchool>()
            .CreateMany(10)
            .ToArray();
        _trust = fixture
            .Build<Trust>()
            .With(l => l.CompanyNumber, _companyNumber)
            .With(l => l.Schools, schools)
            .Create();

        _service = new Mock<ITrustsService>();
        _function = new GetTrustFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.GetAsync(_companyNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_trust);

        var result = await _function.RunAsync(CreateHttpRequestData(), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var actual = await result.ReadAsJsonAsync<Trust>();
        Assert.NotNull(actual);
        Assert.Equal(_companyNumber, actual.CompanyNumber);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        _service
            .Setup(d => d.GetAsync(_companyNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Trust?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}