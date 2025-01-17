using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Balance;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Balance;

public class WhenFunctionReceivesGetSchoolBalanceRequest : BalanceSchoolFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = Fixture.Build<BalanceSchoolModel>().Create();

        Service
            .Setup(d => d.GetSchoolAsync(It.IsAny<string>()))
            .ReturnsAsync(model);

        var result = await Functions.SchoolBalanceAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BalanceSchoolResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Service
            .Setup(d => d.GetSchoolAsync(It.IsAny<string>()))
            .ReturnsAsync((BalanceSchoolModel?)null);

        var result = await Functions.SchoolBalanceAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}