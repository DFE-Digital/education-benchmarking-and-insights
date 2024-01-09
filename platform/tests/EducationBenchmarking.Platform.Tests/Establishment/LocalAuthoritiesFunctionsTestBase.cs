using EducationBenchmarking.Platform.Api.Establishment;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class LocalAuthoritiesFunctionsTestBase : FunctionsTestBase
{
    protected readonly LocalAuthoritiesFunctions Functions;

    protected LocalAuthoritiesFunctionsTestBase()
    {
        Functions = new LocalAuthoritiesFunctions();
    }
}