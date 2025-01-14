using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark.FinancialPlans;
using Platform.Test;

namespace Platform.Benchmark.Tests;

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