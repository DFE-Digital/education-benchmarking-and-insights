using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HighNeedsStartBenchmarkingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator LaInputField => page.Locator("#LaInput");
    private ILocator LaDropdown => page.Locator("#LaInput__listbox");
    private ILocator OthersComparatorsCards => page.Locator("#current-comparators-others");
    private ILocator SaveAndContinueButton => page.Locator(Selectors.Button, new PageLocatorOptions
    {
        HasText = "Benchmark high needs spending"
    });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task ChooseNthItemFromSelect(int n)
    {
        await LaInputField.ShouldBeVisible();
        await LaInputField.FocusAsync();

        for (var i = 0; i < n; i++)
        {
            await page.Keyboard.PressAsync(Keyboard.ArrowDownKey);
        }
    }

    public async Task TypeIntoInputField(string text)
    {
        await LaInputField.ShouldBeVisible();
        await LaInputField.PressSequentially(text);
        await LaDropdown.ShouldBeVisible();
    }

    public async Task PressTabKey()
    {
        await page.Keyboard.PressAsync(Keyboard.TabKey);
    }

    public async Task<HighNeedsStartBenchmarkingPage> PressEnterKey()
    {
        await page.Keyboard.PressAsync(Keyboard.EnterKey);
        return new HighNeedsStartBenchmarkingPage(page);
    }

    public async Task ComparatorsContains(string name)
    {
        await OthersComparatorsCards.ShouldBeVisible();

        var title = await OthersComparatorsCards.Locator(".app-removable-card > p").AllInnerTextsAsync();
        Assert.Contains(name, title);
    }

    public async Task<HighNeedsBenchmarkingPage> ClickSaveAndContinueButton()
    {
        await SaveAndContinueButton.ClickAsync();
        return new HighNeedsBenchmarkingPage(page);
    }
}