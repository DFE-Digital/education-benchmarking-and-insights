using EducationBenchmarking.Platform.Api.Insight;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class MiscFunctionsTestBase : FunctionsTestBase
{
    protected readonly MiscFunctions Functions;

    protected MiscFunctionsTestBase()
    {
        Functions = new MiscFunctions();
    }
}