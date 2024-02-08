using TechTalk.SpecFlow.Infrastructure;

namespace EducationBenchmarking.Platform.ApiTests.Drivers;

public class BenchmarkApiDriver : ApiDriver
{
    public BenchmarkApiDriver(ISpecFlowOutputHelper output) : base(TestConfiguration.Benchmark, output)
    {
    }
}