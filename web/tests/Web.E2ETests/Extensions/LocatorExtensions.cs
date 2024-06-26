using FluentAssertions;
using Microsoft.Playwright;
using Xunit;

namespace Web.E2ETests;

public static class LocatorExtensions
{
    public static async Task<ILocator> Click(this ILocator locator, LoadState state = LoadState.NetworkIdle)
    {
        await locator.ClickAsync();
        await locator.Page.WaitForLoadStateAsync(state);
        return locator;
    }

    public static async Task<ILocator> SelectOption(this ILocator locator, string option)
    {
        await locator.SelectOptionAsync(option);

        return locator;
    }
    public static async Task<ILocator> ShouldBeVisible(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeVisibleAsync();

        return locator;
    }

    public static async Task<ILocator> ShouldNotBeVisible(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeHiddenAsync();

        return locator;
    }

    public static async Task<ILocator> ShouldHaveText(this ILocator locator, string? text)
    {
        await Assertions.Expect(locator).ToHaveTextAsync(text ?? throw new ArgumentNullException(nameof(text)),
            new LocatorAssertionsToHaveTextOptions
            {
                UseInnerText = true
            });
        return locator;
    }

    public static async Task<ILocator> ShouldHaveAttribute(this ILocator locator, string attributeName,
        string attributeValue)
    {
        await Assertions.Expect(locator).ToHaveAttributeAsync(attributeName, attributeValue);

        return locator;
    }

    public static async Task<ILocator> ShouldBeChecked(this ILocator locator, bool isChecked)
    {
        var actualCheckedStatus = await locator.IsCheckedAsync();
        var expectedStatus = isChecked ? "checked" : "unchecked";

        actualCheckedStatus.Should().Be(isChecked, $"the checkbox status should be {expectedStatus}");

        return locator;
    }

    public static async Task<ILocator> ShouldBeEnabled(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeEnabledAsync();
        return locator;
    }

    public static async Task<ILocator> ShouldHaveTableContent(this ILocator locator, List<List<string>> expectedData,
        bool includeHeaderRow)
    {
        var actualData = new List<List<string>>();

        if (includeHeaderRow)
        {
            var headerCells = await locator.Locator("th").AllAsync();
            var headerData = new List<string>();
            foreach (var cell in headerCells)
            {
                headerData.Add(await cell.InnerTextAsync());
            }

            actualData.Add(headerData);
        }

        var rows = await locator.Locator("tbody").Locator("tr").AllAsync();

        foreach (var row in rows)
        {
            var cells = await row.Locator("td").AllAsync();

            var rowData = new List<string>();
            foreach (var cell in cells)
            {
                rowData.Add(await cell.InnerTextAsync());
            }

            actualData.Add(rowData);
        }

        for (var i = 0; i < expectedData.Count; i++)
        {
            var expectedTableCell = expectedData[i];
            var actualTableCell = actualData[i];

            for (var j = 0; j < expectedTableCell.Count; j++)
            {
                actualTableCell[j].Should()
                    .Be(expectedTableCell[j], "actual table cells should have the expected data");
            }
        }

        return locator;
    }

    public static async Task<ILocator> ShouldHaveTableHeaders(this ILocator locator, string[] expected)
    {
        var cells = await locator.Locator("th").AllAsync();
        var actual = new List<string>();
        foreach (var cell in cells)
        {
            actual.Add(await cell.InnerTextAsync());
        }

        for (var i = 0; i < actual.Count; i++)
        {
            actual[i].Should().Be(expected[i], "Actual table headers should have the expected data");
        }

        return locator;
    }

    public static async Task<ILocator> TextEqual(this ILocator locator, string expected)
    {
        var actual = await locator.InnerTextAsync();
        actual.Should().Be(expected, $"the expected text {expected} should match the actual text {actual}");
        return locator;
    }

    public static async Task<ILocator> ShouldHaveSelectedOption(this ILocator locator, string expected)
    {
        const string exp = "select => select.options[select.selectedIndex].text";
        var actual = await locator.EvaluateAsync<string>(exp);
        Assert.Equal(expected, actual);
        return locator;
    }

    public static async Task<ILocator> AssertLocatorClass(this ILocator locator, string locatorClass)
    {
        await Assertions.Expect(locator).ToHaveClassAsync(locatorClass);
        return locator;
    }

    public static async Task<ILocator> PressSequentially(this ILocator locator, string inputValue)
    {
        await locator.PressSequentiallyAsync(inputValue, new() { Delay = 100 });
        return locator;
    }

    public static async Task<ILocator> Press(this ILocator locator, string key)
    {
        await locator.PressAsync(key);
        return locator;
    }

    public static async Task<ILocator> Check(this ILocator locator)
    {
        await locator.CheckAsync();
        return locator;
    }

    public static async Task<int> Count(this ILocator locator)
    {
        return await locator.CountAsync();
    }

    public static async Task<ILocator> Fill(this ILocator locator, string inputValue)
    {
        await locator.FillAsync(inputValue);
        return locator;
    }

    public static async Task<bool> CheckVisible(this ILocator locator)
    {
        return await locator.IsVisibleAsync();
    }
}