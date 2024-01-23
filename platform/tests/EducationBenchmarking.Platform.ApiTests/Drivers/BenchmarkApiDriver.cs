using EducationBenchmarking.Platform.ApiTests.TestSupport;
using TechTalk.SpecFlow.Infrastructure;

namespace EducationBenchmarking.Platform.ApiTests.Drivers;

public class BenchmarkApiDriver : ApiDriver
{
    public BenchmarkApiDriver(ISpecFlowOutputHelper output) : base(Config.Apis.Benchmark, output)
    {
    }
}