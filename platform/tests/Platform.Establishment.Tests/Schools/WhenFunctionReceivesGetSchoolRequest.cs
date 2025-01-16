using System.Net;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Schools;

public class WhenFunctionReceivesGetSchoolRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new School());

        var result = await Functions.SingleSchoolAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<School>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {

        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync((School?)null);

        var result = await Functions.SingleSchoolAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SingleSchoolAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}