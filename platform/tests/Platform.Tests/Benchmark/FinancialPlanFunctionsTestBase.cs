using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark;
using Platform.Api.Benchmark.FinancialPlans;

namespace Platform.Tests.Benchmark;

public class FinancialPlanFunctionsTestBase : FunctionsTestBase
{
    protected readonly FinancialPlanFunctions Functions;
    protected readonly Mock<IFinancialPlanService> Service;


    protected FinancialPlanFunctionsTestBase()
    {
        Service = new Mock<IFinancialPlanService>();
        Functions = new FinancialPlanFunctions(new NullLogger<FinancialPlanFunctions>(), Service.Object);
    }
}