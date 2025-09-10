using Web.E2ETests.Drivers;

namespace Web.E2ETests.Hooks;

[Binding]
public class SharedBrowserHooks
{
    [BeforeFeature]
    public static void BeforeFeature(FeatureContext context)
    {
        context.FeatureContainer.Resolve<PageDriver>();
        context.FeatureContainer.Resolve<PageDriverWithJavaScriptDisabled>();
    }
}