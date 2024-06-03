using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark;
using Platform.Api.Benchmark.FinancialPlans;

namespace Platform.Tests.Benchmark;

public class FinancialPlansFunctionsTestBase : FunctionsTestBase
{
    protected readonly FinancialPlansFunctions Functions;
    protected readonly Mock<IFinancialPlansService> Service;


    protected FinancialPlansFunctionsTestBase()
    {
        Service = new Mock<IFinancialPlansService>();
        Functions = new FinancialPlansFunctions(new NullLogger<FinancialPlansFunctions>(), Service.Object);
    }
}