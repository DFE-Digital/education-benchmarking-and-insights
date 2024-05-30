using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark;
using Platform.Api.Benchmark.ComparatorSets;

namespace Platform.Tests.Benchmark;

public class ComparatorSetFunctionsTestBase : FunctionsTestBase
{
    protected readonly ComparatorSetFunctions Functions;
    protected readonly Mock<IComparatorSetService> Service;


    protected ComparatorSetFunctionsTestBase()
    {
        Service = new Mock<IComparatorSetService>();
        Functions = new ComparatorSetFunctions(Service.Object, new NullLogger<ComparatorSetFunctions>());
    }
}