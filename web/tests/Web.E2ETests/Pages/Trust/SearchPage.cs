using Microsoft.Playwright;

namespace Web.E2ETests.Pages.Trust;

public class SearchPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator SearchTermInputField => page.Locator(Selectors.SearchTermInput);
    private ILocator SearchButton => page.Locator($"{Selectors.GovButton}[name='action'][value='reset']");
    private ILocator SuggestionsDropdown => page.Locator(Selectors.TermSuggestDropdown);
    private ILocator NthSuggestionItem(int index) => page.Locator($"{Selectors.SearchTermInput}__option--{index}");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
        await SearchTermInputField.ShouldBeVisible();
        await SearchButton.ShouldBeVisible().ShouldBeEnabled();
    }

    public async Task TypeIntoSearchBox(string text)
    {
        await SearchTermInputField.PressSequentially(text);
        await SuggestionsDropdown.ShouldBeVisible();
    }

    public async Task<T> PressEnterKey<T>(Func<IPage, T> next)
    {
        await page.Keyboard.PressAsync(Keyboard.EnterKey);
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return next(page);
    }

    public async Task SelectItemFromSuggesterWithKeyboard()
    {
        await SearchTermInputField.PressAsync(Keyboard.ArrowDownKey);
    }

    public async Task SelectItemFromSuggesterWithMouse()
    {
        await NthSuggestionItem(0).ClickAsync();
    }

    public async Task<T> ClickSearch<T>(Func<IPage, T> next)
    {
        await SearchButton.ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        return next(page);
    }

    public async Task AssertSearchResults(string keyword)
    {
        var listItems = await SuggestionsDropdown.Locator("li").AllAsync();
        foreach (var item in listItems)
        {
            await item.ShouldContainText(keyword);
        }
    }
}