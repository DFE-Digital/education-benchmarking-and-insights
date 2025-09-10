using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.CustomData;

public class ChangeWorkforceDataPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Change workforce data"
        });

    private ILocator SaveChangesButton => page.Locator(Selectors.GovButton,
        new PageLocatorOptions
        {
            HasText = "Save changes to data"
        });

    private ILocator ValidationError => page.Locator(Selectors.GovErrorSummary);
    private ILocator CustomDataField(string item) => page.Locator($".table-custom-data > tbody > tr:has-text('{item}') > td input");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task TypeIntoCustomDataFieldForItem(string item, string text)
    {
        await CustomDataField(item).ClearAsync();
        await CustomDataField(item).PressSequentially(text);
    }

    public async Task<CreateCustomDataSubmittedPage> ClickSaveChangesButton()
    {
        await SaveChangesButton.ClickAsync();
        return new CreateCustomDataSubmittedPage(page);
    }

    public async Task ValidationErrorIsDisplayed()
    {
        await ValidationError.ShouldBeVisible();
    }

    public async Task ValidationErrorContainsErrorMessage(string message)
    {
        await ValidationError.ShouldContainText(message);
    }
}