using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Schools;
using Platform.Api.Establishment.Trusts;
using Xunit;
namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesGetTrustRequest : TrustsFunctionsTestBase
{
    private readonly string _companyNumber;
    private readonly IEnumerable<School> _schools;
    private readonly Trust _trust;

    public WhenFunctionReceivesGetTrustRequest()
    {
        var fixture = new Fixture();
        _companyNumber = fixture.Create<string>();
        _trust = fixture
            .Build<Trust>()
            .With(l => l.CompanyNumber, _companyNumber)
            .Create();
        _schools = fixture
            .Build<School>()
            .CreateMany(10);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        TrustsService
            .Setup(d => d.GetAsync(_companyNumber))
            .ReturnsAsync(_trust);
        SchoolsService
            .Setup(s => s.QueryAsync(_companyNumber, null, null))
            .ReturnsAsync(_schools);

        var result = await Functions.SingleTrustAsync(CreateHttpRequestData(), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var actual = await result.ReadAsJsonAsync<TrustResponse>();
        Assert.NotNull(actual);
        Assert.Equal(_companyNumber, actual.CompanyNumber);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        TrustsService
            .Setup(d => d.GetAsync(_companyNumber))
            .ReturnsAsync((Trust?)null);

        var result = await Functions.SingleTrustAsync(CreateHttpRequestData(), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        TrustsService
            .Setup(d => d.GetAsync(_companyNumber))
            .Throws(new Exception());

        var result = await Functions.SingleTrustAsync(CreateHttpRequestData(), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}