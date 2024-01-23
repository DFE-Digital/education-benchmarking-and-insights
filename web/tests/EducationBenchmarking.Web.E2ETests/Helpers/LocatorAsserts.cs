using FluentAssertions;
using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Helpers;

public static class LocatorAssert
{
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

    public static async Task<ILocator> ShouldHaveTextStartingWith(this ILocator locator, string elementName,
        string text)
    {
        var elementInnerText = await locator.InnerTextAsync();
        elementInnerText.Should().StartWith(text, $"{elementName} should start with text {text}");

        return locator;
    }

    public static async Task<ILocator> ShouldContainText(this ILocator locator, string text)
    {
        await Assertions.Expect(locator).ToContainTextAsync(text, new LocatorAssertionsToContainTextOptions
        {
            UseInnerText = true
        });
        return locator;
    }

    public static async Task<ILocator> ShouldHaveInputValue(this ILocator locator, string inputValue,
        string elementName)
    {
        const string reason = "{0} input should have value";
        var locatorInput = await locator.InputValueAsync();
        locatorInput.Should().BeEquivalentTo(inputValue, reason, elementName);
        return locator;
    }

    public static async Task<ILocator> ShouldHaveAttribute(this ILocator locator, string attributeName,
        string attributeValue)
    {
        await Assertions.Expect(locator).ToHaveAttributeAsync(attributeName, attributeValue);

        return locator;
    }

    public static async Task<ILocator> ShouldContainAttribute(this ILocator locator, string attributeName,
        string attributeValue)
    {
        var actualAttributeValue = await locator.GetAttributeAsync(attributeName);
        actualAttributeValue.Should().Contain(attributeValue);

        return locator;
    }

    public static async Task<ILocator> ShouldBeAtPosition(this ILocator locator, string objectName, float expectedX,
        float expectedY)
    {
        const string reason = "{0} should have {1} coordinate {2}";
        var objectLocation = await locator.BoundingBoxAsync();

        if (objectLocation == null)
        {
            throw new NullReferenceException($"No coordinates found for {objectName}");
        }

        if (true)
        {
            objectLocation.X.Should().Be(expectedX, reason, objectName, "X", expectedX);
            objectLocation.Y.Should().Be(expectedY, reason, objectName, "Y", expectedY);
        }

        return locator;
    }

    public static async Task<ILocator> ShouldBeChecked(this ILocator locator, bool isChecked)
    {
        var actualCheckedStatus = await locator.IsCheckedAsync();
        var expectedStatus = isChecked ? "checked" : "unchecked";

        actualCheckedStatus.Should().Be(isChecked, $"the checkbox status should be {expectedStatus}");

        return locator;
    }

    public static async Task<ILocator> ShouldBeChecked(this Task<ILocator> locator, bool isChecked)
    {
        var l = await locator;
        return await l.ShouldBeChecked(isChecked);
    }

    public static async Task<ILocator> ShouldBeEnabled(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeEnabledAsync();

        return locator;
    }

    public static async Task<ILocator> ShouldBeDisabled(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeDisabledAsync();

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

    public static async Task<ILocator> ShouldHaveCount(this ILocator locator, int expectedCount)
    {
        await Assertions.Expect(locator).ToHaveCountAsync(expectedCount);

        return locator;
    }

    public static async Task<int> Count(this ILocator locator)
    {
        return await locator.CountAsync();
    }

    public static async Task<ILocator> ShouldHaveCss(this ILocator locator, string name, string expectedValue)
    {
        await Assertions.Expect(locator).ToHaveCSSAsync(name, expectedValue);

        return locator;
    }

    public static void AreEqual(string text1, string text2)
    {
        text1.Should().Be(text2, $"the expected text {text1} should match the actual text {text2}");
    }

    public static async Task<ILocator> ShouldHaveRadioButtonOptions(this ILocator radioGroupLocator,
        List<KeyValuePair<string, bool>> optionCheckedStatuses, bool exactLabels)
    {
        foreach (var option in optionCheckedStatuses)
        {
            await radioGroupLocator
                .GetByLabel(option.Key, new LocatorGetByLabelOptions { Exact = exactLabels })
                .ShouldBeVisible()
                .ShouldBeChecked(option.Value);
        }

        return radioGroupLocator;
    }

    public static async Task<ILocator> ShouldHaveSelectedOption(this ILocator dropdownLocator, string expectedValue)
    {
        var selectedValue =
            await dropdownLocator.EvaluateAsync<string>(
                "select => select.options[select.selectedIndex].text");
        Assert.True(
            string.Equals(expectedValue, selectedValue),
            $"Expected option: {expectedValue}. Actual: {selectedValue}");
        return dropdownLocator;
    }

    public static async Task<bool> CheckVisible(this ILocator locator)
    {
        return await locator.IsVisibleAsync();
    }

    public static async Task<ILocator> AssertLocatorClass(this ILocator locator, string locatorClass)
    {
        await Assertions.Expect(locator).ToHaveClassAsync(locatorClass);
        return locator;
    }
}