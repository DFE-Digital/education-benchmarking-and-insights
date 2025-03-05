using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests.Pages.LocalAuthority;

public class HighNeedsStartBenchmarkingPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator($"main {Selectors.H1}");
    private ILocator LaInputField => page.Locator("#LaInput");
    private ILocator LaDropdown => page.Locator("#LaInput__listbox");
    private ILocator ComparatorsTable => page.Locator("#current-comparators-la");

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
        await ComparatorsTable.ShouldBeVisible();

        var cells = await ComparatorsTable.Locator("tbody > tr > td:first-child").AllInnerTextsAsync();
        Assert.Contains(name, cells);
    }
}