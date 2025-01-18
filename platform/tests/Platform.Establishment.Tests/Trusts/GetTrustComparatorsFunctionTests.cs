using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.Trusts;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Requests;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Trusts;

public class GetTrustComparatorsFunctionTests : FunctionsTestBase
{
    private readonly string _companyNumber;
    private readonly TrustComparators _comparators;
    private readonly TrustComparatorsRequest _request;
    private readonly GetTrustComparatorsFunction _functions;
    private readonly Mock<ITrustComparatorsService> _service;

    public GetTrustComparatorsFunctionTests()
    {
        var fixture = new Fixture();

        _companyNumber = fixture.Create<string>();
        _comparators = fixture.Create<TrustComparators>();
        _request = fixture.Create<TrustComparatorsRequest>();

        _service = new Mock<ITrustComparatorsService>();
        _functions = new GetTrustComparatorsFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.ComparatorsAsync(_companyNumber, It.IsAny<TrustComparatorsRequest>()))
            .ReturnsAsync(_comparators);

        var result = await _functions.RunAsync(CreateHttpRequestDataWithBody(_request), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var actual = await result.ReadAsJsonAsync<TrustComparators>();
        Assert.NotNull(actual);
        Assert.Equivalent(_comparators, actual);
    }
}