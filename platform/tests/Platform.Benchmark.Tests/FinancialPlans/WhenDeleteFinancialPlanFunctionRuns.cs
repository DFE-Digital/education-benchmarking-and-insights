using System.Net;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark.Features.FinancialPlans;
using Platform.Api.Benchmark.Features.FinancialPlans.Models;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.FinancialPlans;

public class WhenDeleteFinancialPlanFunctionRuns : FunctionsTestBase
{
    private readonly DeleteFinancialPlanFunction _functions;
    private readonly Mock<IFinancialPlansService> _service;

    public WhenDeleteFinancialPlanFunctionRuns()
    {
        _service = new Mock<IFinancialPlansService>();
        _functions = new DeleteFinancialPlanFunction(new NullLogger<DeleteFinancialPlanFunction>(), _service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.DetailsAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new FinancialPlanDetails());

        _service.Setup(d => d.DeleteAsync(It.IsAny<string>(), It.IsAny<int>()));

        var result = await _functions.RunAsync(CreateHttpRequestData(), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        _service
            .Setup(d => d.DeleteAsync(It.IsAny<string>(), It.IsAny<int>()))
            .Throws(new Exception());

        var result = await _functions.RunAsync(CreateHttpRequestData(), "1", 2021);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}