using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CustomData;

public class ChangeFinancialDataPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Change financial data"
        });

    private ILocator ContinueButton => page.Locator(Selectors.GovButton,
        new PageLocatorOptions
        {
            HasText = "Continue"
        });
    private ILocator Accordion => page.Locator("#accordion-financial-data");
    private ILocator CustomDataField(string category) => page.Locator($".table-custom-data > tbody > tr:has-text('{category}') > td input");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Accordion.ShouldBeVisible();
    }

    public async Task TypeIntoCustomDataFieldForCost(string category, string text)
    {
        await CustomDataField(category).ClearAsync();
        await CustomDataField(category).PressSequentially(text);
    }

    public async Task<ChangeNonFinancialDataPage> ClickContinue()
    {
        await ContinueButton.ClickAsync();
        return new ChangeNonFinancialDataPage(page);
    }
}