namespace Platform.ApiTests.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global
public class BenchmarkApiDriver(IReqnrollOutputHelper output) : ApiDriver(TestConfiguration.Benchmark, output);