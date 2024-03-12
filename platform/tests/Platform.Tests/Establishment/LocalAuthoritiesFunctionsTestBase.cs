using Platform.Api.Establishment;

namespace Platform.Tests.Establishment;

public class LocalAuthoritiesFunctionsTestBase : FunctionsTestBase
{
    protected readonly LocalAuthoritiesFunctions Functions;

    protected LocalAuthoritiesFunctionsTestBase()
    {
        Functions = new LocalAuthoritiesFunctions();
    }
}