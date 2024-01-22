using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

public abstract class EstablishmentSteps
{
    protected readonly ApiDriver Api;

    protected EstablishmentSteps(ITestOutputHelper output)
    {
        Api = new ApiDriver(Config.Apis.Establishment ?? throw new NullException(Config.Apis.Establishment), output);
    }
}