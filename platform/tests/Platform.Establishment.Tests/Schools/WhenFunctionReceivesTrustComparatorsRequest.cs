using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Functions.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Schools;

public class WhenFunctionReceivesSchoolComparatorsRequest : SchoolComparatorsFunctionsTestBase
{
    private readonly string _companyNumber;
    private readonly SchoolComparators _comparators;
    private readonly SchoolComparatorsRequest _request;

    public WhenFunctionReceivesSchoolComparatorsRequest()
    {
        var fixture = new Fixture();

        _companyNumber = fixture.Create<string>();
        _comparators = fixture.Create<SchoolComparators>();
        _request = fixture.Create<SchoolComparatorsRequest>();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.ComparatorsAsync(_companyNumber, It.IsAny<SchoolComparatorsRequest>()))
            .ReturnsAsync(_comparators);

        var result = await Functions.SchoolComparatorsAsync(CreateHttpRequestDataWithBody(_request), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var actual = await result.ReadAsJsonAsync<SchoolComparators>();
        Assert.NotNull(actual);
        Assert.Equivalent(_comparators, actual);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.ComparatorsAsync(_companyNumber, It.IsAny<SchoolComparatorsRequest>()))
            .Throws(new Exception());

        var result = await Functions.SchoolComparatorsAsync(CreateHttpRequestDataWithBody(_request), _companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}