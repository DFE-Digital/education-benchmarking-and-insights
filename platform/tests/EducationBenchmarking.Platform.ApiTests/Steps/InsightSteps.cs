using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

public abstract class InsightSteps
{
    protected readonly ApiDriver Api;

    protected InsightSteps(ITestOutputHelper output)
    {
        Api = new ApiDriver(Config.Apis.Insight ?? throw new NullException(Config.Apis.Insight), output);
    }
}