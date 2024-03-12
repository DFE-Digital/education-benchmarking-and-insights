using TechTalk.SpecFlow.Infrastructure;

namespace Platform.ApiTests.Drivers;

public class BenchmarkApiDriver : ApiDriver
{
    public BenchmarkApiDriver(ISpecFlowOutputHelper output) : base(TestConfiguration.Benchmark, output)
    {
    }
}