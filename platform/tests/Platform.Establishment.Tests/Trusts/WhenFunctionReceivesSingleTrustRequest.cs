using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.Trusts;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Trusts;

public class WhenFunctionReceivesSingleTrustRequest : TrustsFunctionsTestBase
{
    private readonly string _companyNumber;
    private readonly Trust _trust;

    public WhenFunctionReceivesSingleTrustRequest()
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
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetAsync(_companyNumber))
            .ReturnsAsync(_trust);

        var result = await Functions.SingleTrustAsync(CreateHttpRequestData(), _companyNumber);

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
        Service
            .Setup(d => d.GetAsync(_companyNumber))
            .ReturnsAsync((Trust?)null);

        var result = await Functions.SingleTrustAsync(CreateHttpRequestData(), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetAsync(_companyNumber))
            .Throws(new Exception());

        var result = await Functions.SingleTrustAsync(CreateHttpRequestData(), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}