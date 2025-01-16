using System.Net;
using AutoFixture;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Benchmark.UserData;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Benchmark.Tests;

public class WhenFunctionReceivesQueryUserDataRequest : UserDataFunctionsTestBase
{
    [Theory]
    [InlineData("userId", null, null, null, null, null)]
    public async Task ShouldReturn200OnValidRequest(string userId, string? type, string? status, string? id, string? organisationId, string? organisationType)
    {
        var query = new Dictionary<string, StringValues>
        {
            { "userId", userId },
            { "type", type },
            { "status", status },
            { "id", id },
            { "organisationId", organisationId },
            { "organisationType", organisationType }
        };

        var userData = Fixture.CreateMany<UserData>();

        Service
            .Setup(d => d.QueryAsync(new[] { userId }, type, status, id, organisationId, organisationType))
            .ReturnsAsync(userData)
            .Verifiable(Times.Once);

        var result = await Functions.QueryAsync(CreateHttpRequestData(query));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var actual = await result.ReadAsJsonAsync<UserData[]>();
        Assert.NotNull(actual);
        Assert.Equivalent(userData, actual);
        Service.Verify();
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>()))
            .Throws(new Exception());

        var result = await Functions.QueryAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}