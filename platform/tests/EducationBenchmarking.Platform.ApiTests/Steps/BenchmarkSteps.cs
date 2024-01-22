using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

public abstract class BenchmarkSteps
{
    protected readonly ApiDriver Api;

    protected BenchmarkSteps(ITestOutputHelper output)
    {
        Api = new ApiDriver(Config.Apis.Benchmark ?? throw new NullException(Config.Apis.Benchmark), output);
    }
}