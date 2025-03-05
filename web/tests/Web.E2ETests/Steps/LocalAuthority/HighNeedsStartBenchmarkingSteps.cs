using Web.E2ETests.Drivers;
using Web.E2ETests.Pages.LocalAuthority;
using Xunit;

namespace Web.E2ETests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority high needs start benchmarking")]
public class HighNeedsStartBenchmarkingSteps(PageDriver driver, PageDriverWithJavaScriptDisabled driverNoJs)
{
    private HighNeedsStartBenchmarkingPage? _highNeedsStartBenchmarkingPage;
    private bool _javascriptDisabled;

    [Given("JavaScript is '(.*)'")]
    public void GivenJavaScriptIs(string enabled)
    {
        _javascriptDisabled = enabled == "disabled";
    }

    [Given("I am on local authority high needs start benchmarking for local authority with code '(.*)'")]
    public async Task GivenIAmOnLocalAuthorityHighNeedsStartBenchmarkingForLocalAuthorityWithCode(string laCode)
    {
        var url = LocalAuthorityHighNeedsStartBenchmarkingUrl(laCode);
        var page = await (_javascriptDisabled ? driverNoJs : driver).Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsStartBenchmarkingPage = new HighNeedsStartBenchmarkingPage(page);
        await _highNeedsStartBenchmarkingPage.IsDisplayed();
    }

    [When("I select the first valid item from the select")]
    public async Task WhenISelectTheFirstValidItemFromTheSelect()
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        await _highNeedsStartBenchmarkingPage.ChooseNthItemFromSelect(1);
    }

    [When("I type '(.*)' into the input field")]
    public async Task WhenITypeIntoTheInputField(string name)
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        await _highNeedsStartBenchmarkingPage.TypeIntoInputField(name);
    }

    [When("I press the Tab key")]
    public async Task WhenIPressTheTabKey()
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        await _highNeedsStartBenchmarkingPage.PressTabKey();
    }

    [When("I press the Enter key")]
    [When("I press the Enter key again")]
    public async Task WhenIPressTheEnterKey()
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        _highNeedsStartBenchmarkingPage = await _highNeedsStartBenchmarkingPage.PressEnterKey();
    }

    [Then("the comparator '(.*)' is added to the comparators")]
    public async Task ThenTheComparatorIsAddedToTheComparators(string name)
    {
        Assert.NotNull(_highNeedsStartBenchmarkingPage);
        await _highNeedsStartBenchmarkingPage.ComparatorsContains(name);
    }

    private static string LocalAuthorityHighNeedsStartBenchmarkingUrl(string laCode)
    {
        return $"{TestConfiguration.ServiceUrl}/local-authority/{laCode}/high-needs/benchmarking";
    }
}