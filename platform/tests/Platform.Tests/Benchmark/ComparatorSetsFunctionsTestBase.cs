using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark;
using Platform.Api.Benchmark.ComparatorSets;

namespace Platform.Tests.Benchmark;

public class ComparatorSetsFunctionsTestBase : FunctionsTestBase
{
    protected readonly ComparatorSetsFunctions Functions;
    protected readonly Mock<IComparatorSetsService> Service;


    protected ComparatorSetsFunctionsTestBase()
    {
        Service = new Mock<IComparatorSetsService>();
        Functions = new ComparatorSetsFunctions(Service.Object, new NullLogger<ComparatorSetsFunctions>());
    }
}