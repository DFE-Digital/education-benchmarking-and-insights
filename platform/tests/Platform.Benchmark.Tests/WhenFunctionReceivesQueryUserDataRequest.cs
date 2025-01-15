using System.Net;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Benchmark.UserData;
using Xunit;

namespace Platform.Benchmark.Tests;

public class WhenFunctionReceivesQueryUserDataRequest : UserDataFunctionsTestBase
{
    [Theory]
    [InlineData("userId", null, null, null, null, null, null)]
    [InlineData("userId", null, null, null, null, null, true)]
    public async Task ShouldReturn200OnValidRequest(string userId, string? type, string? status, string? id, string? organisationId, string? organisationType, bool? active)
    {
        var query = new Dictionary<string, StringValues>
        {
            {
                "userId", userId
            },
            {
                "type", type
            },
            {
                "status", status
            },
            {
                "id", id
            },
            {
                "organisationId", organisationId
            },
            {
                "organisationType", organisationType
            },
            {
                "active", active == true ? "true" : null
            }
        };

        Service
            .Setup(d => d.QueryAsync(new[]
            {
                userId
            }, type, status, id, organisationId, organisationType, active))
            .ReturnsAsync(Array.Empty<UserData>())
            .Verifiable(Times.Once);

        var result = await Functions.QueryAsync(CreateHttpRequestData(query));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Service.Verify();
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<bool?>()))
            .Throws(new Exception());

        var result = await Functions.QueryAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}