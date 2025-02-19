using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority high needs benchmarking")]
public class HighNeedsBenchmarkingSteps(PageDriver driver)
{
    private HighNeedsBenchmarkingPage? _highNeedsBenchmarkingPage;

    [Given("I am on local authority high needs benchmarking for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHighNeedsBenchmarkingForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHighNeedsBenchmarkingUrl(laCode);
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsBenchmarkingPage = new HighNeedsBenchmarkingPage(page);
        await _highNeedsBenchmarkingPage.IsDisplayed();
    }

    private static string LocalAuthorityHighNeedsBenchmarkingUrl(string laCode) => $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs";
}