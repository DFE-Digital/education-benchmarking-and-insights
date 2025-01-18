using System.Net;
using Moq;
using Platform.Api.Insight.Features.Income;
using Platform.Api.Insight.Features.Income.Models;
using Platform.Api.Insight.Features.Income.Responses;
using Platform.Api.Insight.Features.Income.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Income;

public class GetIncomeSchoolFunctionTests : FunctionsTestBase
{
    private readonly GetIncomeSchoolFunction _function;
    private readonly Mock<IIncomeService> _service;

    public GetIncomeSchoolFunctionTests()
    {
        _service = new Mock<IIncomeService>();
        _function = new GetIncomeSchoolFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.GetSchoolAsync(It.IsAny<string>()))
            .ReturnsAsync(new IncomeSchoolModel());

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<IncomeSchoolResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _service
            .Setup(d => d.GetSchoolAsync(It.IsAny<string>()))
            .ReturnsAsync((IncomeSchoolModel?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}