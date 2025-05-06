using System.Net;
using Moq;
using Platform.Api.Benchmark.Features.FinancialPlans;
using Platform.Api.Benchmark.Features.FinancialPlans.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.FinancialPlans;

public class WhenDeleteFinancialPlanFunctionRuns : FunctionsTestBase
{
    private readonly DeleteFinancialPlanFunction _functions;
    private readonly Mock<IFinancialPlansService> _service = new();

    public WhenDeleteFinancialPlanFunctionRuns()
    {
        _functions = new DeleteFinancialPlanFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string urn = nameof(urn);
        const int year = 2021;

        _service
            .Setup(d => d.DeleteAsync(urn, year))
            .Verifiable();

        var result = await _functions.RunAsync(CreateHttpRequestData(), urn, year);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        _service.Verify();
    }
}