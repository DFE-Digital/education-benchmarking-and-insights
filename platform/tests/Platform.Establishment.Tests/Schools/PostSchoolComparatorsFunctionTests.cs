using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Schools;

public class PostSchoolComparatorsFunctionTests : FunctionsTestBase
{
    private readonly SchoolComparators _comparators;
    private readonly PostSchoolComparatorsFunction _function;
    private readonly SchoolComparatorsRequest _request;
    private readonly Mock<ISchoolComparatorsService> _service;
    private readonly string _urn;

    public PostSchoolComparatorsFunctionTests()
    {
        var fixture = new Fixture();

        _urn = fixture.Create<string>();
        _comparators = fixture.Create<SchoolComparators>();
        _request = fixture.Create<SchoolComparatorsRequest>();

        _service = new Mock<ISchoolComparatorsService>();
        _function = new PostSchoolComparatorsFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.ComparatorsAsync(_urn, It.IsAny<SchoolComparatorsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_comparators);

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(_request), _urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var actual = await result.ReadAsJsonAsync<SchoolComparators>();
        Assert.NotNull(actual);
        Assert.Equivalent(_comparators, actual);
    }
}