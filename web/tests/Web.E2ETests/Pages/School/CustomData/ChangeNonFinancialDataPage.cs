using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CustomData;

public class ChangeNonFinancialDataPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Change non-financial data"
        });

    private ILocator ContinueButton => page.Locator(Selectors.GovButton,
        new PageLocatorOptions
        {
            HasText = "Continue"
        });
    private ILocator Warning => page.Locator(".govuk-warning-text");
    private ILocator CustomDataField(string item) => page.Locator($".table-custom-data > tbody > tr:has-text('{item}') > td input");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await Warning.ShouldBeVisible();
    }

    public async Task TypeIntoCustomDataFieldForItem(string item, string text)
    {
        await CustomDataField(item).ClearAsync();
        await CustomDataField(item).PressSequentially(text);
    }

    public async Task<ChangeWorkforceDataPage> ClickContinue()
    {
        await ContinueButton.ClickAsync();
        return new ChangeWorkforceDataPage(page);
    }
}