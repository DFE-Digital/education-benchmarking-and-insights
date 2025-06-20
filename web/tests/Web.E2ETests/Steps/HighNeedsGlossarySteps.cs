using Web.E2ETests.Drivers;
using Web.E2ETests.Pages;
using Xunit;

namespace Web.E2ETests.Steps;

[Binding]
[Scope(Feature = "High needs glossary")]
public class HighNeedsGlossarySteps(PageDriver driver)
{
    private HighNeedsGlossaryPage? _highNeedsGlossaryPage;

    [Given("I am on high needs glossary page")]
    public async Task GivenIAmOnHighNeedsGlossaryPage()
    {
        var url = HighNeedsGlossaryUrl();
        var page = await driver.Current;
        await page.GotoAndWaitForLoadAsync(url);

        _highNeedsGlossaryPage = new HighNeedsGlossaryPage(page);
        await _highNeedsGlossaryPage.IsDisplayed();
    }

    [Then("there are (.*) items in the high needs glossary")]
    public async Task ThenThereAreItemsInTheHighNeedsGlossary(int count)
    {
        Assert.NotNull(_highNeedsGlossaryPage);
        await _highNeedsGlossaryPage.AssertHighNeedsGlossary(count);
    }

    [Then("there are (.*) items in the general glossary")]
    public async Task ThenThereAreItemsInTheGeneralGlossary(int count)
    {
        Assert.NotNull(_highNeedsGlossaryPage);
        await _highNeedsGlossaryPage.AssertGeneralGlossary(count);
    }

    private static string HighNeedsGlossaryUrl()
    {
        return $"{TestConfiguration.ServiceUrl}/guidance/high-needs-glossary";
    }
}