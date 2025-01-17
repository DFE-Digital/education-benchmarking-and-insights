using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Balance;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Balance;

public class WhenFunctionReceivesGetSchoolBalanceHistoryRequest : BalanceSchoolFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var history = Fixture.CreateMany<BalanceHistoryModel>(5);
        var years = new BalanceYearsModel { StartYear = 2019, EndYear = 2023 };

        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((years, history));

        var result = await Functions.SchoolBalanceHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BalanceHistoryResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((null, Array.Empty<BalanceHistoryModel>()));

        var result = await Functions.SchoolBalanceHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}