using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Balance;
using Platform.Api.Insight.Features.Balance.Models;
using Platform.Api.Insight.Features.Balance.Responses;
using Platform.Api.Insight.Features.Balance.Services;
using Platform.Api.Insight.Shared;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Balance;

public class GetBalanceSchoolHistoryFunctionTests : FunctionsTestBase
{
    private readonly GetBalanceSchoolHistoryFunction _function;
    private readonly Mock<IBalanceService> _service;
    private readonly Fixture _fixture;

    public GetBalanceSchoolHistoryFunctionTests()
    {
        _service = new Mock<IBalanceService>();
        _fixture = new Fixture();
        _function = new GetBalanceSchoolHistoryFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var history = _fixture.CreateMany<BalanceHistoryModel>(5);
        var years = new YearsModel { StartYear = 2019, EndYear = 2023 };

        _service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((years, history));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BalanceHistoryResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((null, Array.Empty<BalanceHistoryModel>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}