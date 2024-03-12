using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark;
using Platform.Api.Benchmark.Db;

namespace Platform.Tests.Benchmark;

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