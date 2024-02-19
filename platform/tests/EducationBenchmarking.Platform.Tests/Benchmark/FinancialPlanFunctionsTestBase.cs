using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class FinancialPlanFunctionsTestBase : FunctionsTestBase
{
    protected readonly FinancialPlanFunctions Functions;
    protected readonly Mock<IFinancialPlanDb> Db;


    protected FinancialPlanFunctionsTestBase()
    {
        Db = new Mock<IFinancialPlanDb>();
        Functions = new FinancialPlanFunctions(new NullLogger<FinancialPlanFunctions>(), Db.Object);
    }
}