using Microsoft.Playwright;

namespace Web.E2ETests.Pages.School.Comparators;

public class CreateComparatorsByNamePage(IPage page) : ICreateComparatorsByPage
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Choose schools to benchmark against"
        });

    private ILocator SchoolSearchInputField => page.Locator(Selectors.SchoolSearchInput);
    private ILocator SchoolSuggestionsDropdown => page.Locator(Selectors.SchoolSuggestDropdown);
    private ILocator ChooseSchoolButton => page.Locator("#choose-school");
    private ILocator CreateSetButton => page.Locator("#create-set");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task TypeIntoSchoolSearchBox(string text)
    {
        await SchoolSearchInputField.PressSequentially(text);
        await SchoolSuggestionsDropdown.ShouldBeVisible();
    }

    public async Task SelectItemFromSuggester()
    {
        await SchoolSearchInputField.PressAsync(Keyboard.ArrowDownKey);
        await page.Keyboard.PressAsync(Keyboard.EnterKey);
    }

    public async Task ClickChooseSchoolButton()
    {
        await ChooseSchoolButton.ClickAsync();
    }

    public async Task<HomePage> ClickCreateSetButton()
    {
        await CreateSetButton.ShouldBeVisible();
        await CreateSetButton.ClickAsync();
        await page.WaitForURLAsync(u => u.EndsWith("?comparator-generated=true"));
        return new HomePage(page);
    }
}