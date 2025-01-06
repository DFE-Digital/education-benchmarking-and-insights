using Microsoft.Playwright;

namespace Web.E2ETests.Pages.Trust.Benchmarking;

public class CreateComparatorsByNamePage(IPage page) : ICreateComparatorsByPage
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Choose trusts to benchmark against"
        });

    private ILocator TrustSearchInputField => page.Locator(Selectors.TrustSearchInput);
    private ILocator TrustSuggestionsDropdown => page.Locator(Selectors.TrustSuggestDropdown);
    private ILocator ChooseTrustButton => page.Locator("#choose-trust");
    private ILocator CreateSetButton => page.Locator("#create-set");

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task TypeIntoSearchBox(string text)
    {
        await TrustSearchInputField.PressSequentially(text);
        await TrustSuggestionsDropdown.ShouldBeVisible();
    }

    public async Task SelectItemFromSuggester()
    {
        await TrustSearchInputField.PressAsync(Keyboard.ArrowDownKey);
        await page.Keyboard.PressAsync(Keyboard.EnterKey);
    }

    public async Task ClickChooseTrustButton()
    {
        await ChooseTrustButton.ClickAsync();
    }

    public async Task<Trust.Benchmarking.TrustBenchmarkSpendingPage> ClickCreateSetButton()
    {
        await CreateSetButton.ShouldBeVisible();
        await CreateSetButton.ClickAsync();
        await page.WaitForURLAsync(u => u.EndsWith("?comparator-generated=true"));
        return new TrustBenchmarkSpendingPage(page);
    }
}