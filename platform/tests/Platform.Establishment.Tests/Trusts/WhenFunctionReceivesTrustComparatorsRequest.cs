using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.Trusts;
using Platform.Functions.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Trusts;

public class WhenFunctionReceivesTrustComparatorsRequest : TrustComparatorsFunctionsTestBase
{
    private readonly string _companyNumber;
    private readonly TrustComparators _comparators;
    private readonly TrustComparatorsRequest _request;

    public WhenFunctionReceivesTrustComparatorsRequest()
    {
        var fixture = new Fixture();

        _companyNumber = fixture.Create<string>();
        _comparators = fixture.Create<TrustComparators>();
        _request = fixture.Create<TrustComparatorsRequest>();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.ComparatorsAsync(_companyNumber, It.IsAny<TrustComparatorsRequest>()))
            .ReturnsAsync(_comparators);

        var result = await Functions.TrustComparatorsAsync(CreateHttpRequestDataWithBody(_request), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var actual = await result.ReadAsJsonAsync<TrustComparators>();
        Assert.NotNull(actual);
        Assert.Equivalent(_comparators, actual);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.ComparatorsAsync(_companyNumber, It.IsAny<TrustComparatorsRequest>()))
            .Throws(new Exception());

        var result = await Functions.TrustComparatorsAsync(CreateHttpRequestDataWithBody(_request), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}