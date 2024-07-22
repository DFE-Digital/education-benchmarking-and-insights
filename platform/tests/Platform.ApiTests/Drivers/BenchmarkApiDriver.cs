using TechTalk.SpecFlow.Infrastructure;
namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class BenchmarkApiDriver(ISpecFlowOutputHelper output) : ApiDriver(TestConfiguration.Benchmark, output);