using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class ComparatorSetFunctionsTestBase : FunctionsTestBase
{
    protected readonly ComparatorSetFunctions Functions;
    protected readonly Mock<IComparatorSetDb> Db;


    protected ComparatorSetFunctionsTestBase()
    {
        Db = new Mock<IComparatorSetDb>();
        Functions = new ComparatorSetFunctions(Db.Object, new NullLogger<ComparatorSetFunctions>());
    }
}