
using FluentAssertions;
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Helpers;

public static class LocatorAssert
{
   
    /// <summary>
    /// Asserts that the given element is visible in the DOM
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldBeVisible(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeVisibleAsync();

        return locator;
    }
    
    /// <summary>
    /// Asserts that the given element either has attribute <c>hidden</c> or does not exist in the DOM
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldNotBeVisible(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeHiddenAsync();
        
        return locator;
    }

    /// <summary>
    /// Asserts that the given element has inner text that is equal to the given text string
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="text">the text string that the given element should have for the assertion to pass</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldHaveText(this ILocator locator, string? text)
    {
        await Assertions.Expect(locator).ToHaveTextAsync(text ?? throw new ArgumentNullException(nameof(text)),
            new LocatorAssertionsToHaveTextOptions
        {
            UseInnerText = true
        });
        return locator;
    }
    
    /// <summary>
    /// Asserts that the given element has inner text that starts with the given text string
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="elementName">the name of the element</param>
    /// <param name="text">the expected string that the inner text of the element should start with</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldHaveTextStartingWith(this ILocator locator, string elementName, string text)
    {
        var elementInnerText = await locator.InnerTextAsync();
        elementInnerText.Should().StartWith(text, $"{elementName} should start with text {text}");
        
        return locator;
    }
    
    /// <summary>
    /// Asserts that the given element has inner text that is contains the given text string
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="text">the text string that the given element should contain for the assertion to pass</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldContainText(this ILocator locator, string text)
    {
        await Assertions.Expect(locator).ToContainTextAsync(text, new LocatorAssertionsToContainTextOptions
        {
            UseInnerText = true
        });
        return locator;
    }
    
    /// <summary>
    /// <para>Asserts that the input value of the given element is equal to the given input value string</para>
    /// <para>**Usage**</para>
    /// <code>
    /// // To check that a text input has the expected input value
    /// await TextInput.ShouldHaveInputValue("test", nameof(TextInput));
    /// </code>
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="inputValue">the expected input value string</param>
    /// <param name="elementName">name of the element that is being asserted</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldHaveInputValue(this ILocator locator, string inputValue, string elementName)
    {
        const string reason = "{0} input should have value";
        var locatorInput = await locator.InputValueAsync();
        locatorInput.Should().BeEquivalentTo(inputValue, reason, elementName);
        return locator;
    }

    /// <summary>
    /// <para>Asserts that the given element has the expected given attribute value for the given attribute name</para>
    /// <para>**Usage**</para>
    /// <code>
    /// // To check that a tab has the attribute of 'selected'
    /// await TabOne.ShouldHaveAttribute("selected", "true");
    /// </code>
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="attributeName">name of the attribute to be asserted</param>
    /// <param name="attributeValue">the expected string value of the asserted attribute</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldHaveAttribute(this ILocator locator, string attributeName, string attributeValue)
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

    /// <summary>
    /// <para>Asserts that the given element is at an expected coordinate position in the viewport</para>
    /// <para>This assertion is useful for checking the position of drag objects on items</para>
    /// <para>**Usage**</para>
    /// <code>
    /// // To check that a drag object is in an expected location
    /// await DragObject.ShouldBeAtPosition(nameof(DragObject), 100, 300);
    /// </code>
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="objectName">name of the element to be asserted</param>
    /// <param name="expectedX">the 'x' coordinate in the viewport of where the element is expected to be</param>
    /// <param name="expectedY">the 'y' coordinate in the viewport of where the element is expected to be</param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static async Task<ILocator> ShouldBeAtPosition(this ILocator locator, string objectName, float expectedX, float expectedY)
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
        bool actualCheckedStatus = await locator.IsCheckedAsync();
        string expectedStatus = isChecked ? "checked" : "unchecked";
        actualCheckedStatus.Should().Be(isChecked, $"the checkbox status should be {expectedStatus}");
        return locator;
    }

    public static async Task<ILocator> ShouldBeChecked(this Task<ILocator> locator, bool isChecked)
    {
        var l = await locator;
        return await l.ShouldBeChecked(isChecked);
    }
    
    
    /// <summary>
    /// Asserts whether the element is enabled in the DOM
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldBeEnabled(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeEnabledAsync();

        return locator;
    }
    
    /// <summary>
    /// Asserts whether the element is disabled in the DOM
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldBeDisabled(this ILocator locator)
    {
        await Assertions.Expect(locator).ToBeDisabledAsync();

        return locator;
    }

    /// <summary>
    /// <para>Asserts whether the content of the given table element is equal to the given expected data</para>
    /// <para>Expected data should have exactly the same rows and cells as the actual table</para>
    /// <para>**Usage**</para>
    /// <code>
    /// // To check that a drag object is in an expected location
    /// await InfoTable.ShouldHaveTableContent(expectedData, false);
    /// </code>
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="expectedData">
    /// the list of listed strings that should appear in the table. Each string represents a cell with each list
    /// of strings representing a row
    /// </param>
    /// <param name="includeHeaderRow">
    /// option of whether the header row of the actual table should be asserted with the expected data
    /// </param>
    /// <returns></returns>
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

        for (int i = 0; i < expectedData.Count; i++)
        {
            var expectedTableCell = expectedData[i];
            var actualTableCell = actualData[i];
            
            for (int j = 0; j < expectedTableCell.Count; j++)
            {
                actualTableCell[j].Should().Be(expectedTableCell[j], "actual table cells should have the expected data");
            }
        }

        return locator;
    }

    /// <summary>
    /// Asserts the count of elements that match the given locator in the DOM
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="expectedCount">the count of how many elements that are expected to be in the DOM</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldHaveCount(this ILocator locator, int expectedCount)
    {
        await Assertions.Expect(locator).ToHaveCountAsync(expectedCount);
        
        return locator;
    }
    /// <summary>
    /// <para>Returns the number of elements matching the locator.</para>
    /// <param name="locator">the locator of the element for the count to be performed on</param>
    /// <para>**Usage**</para>
    /// <code>int count = await page.GetByRole(AriaRole.Listitem).CountAsync();</code>
    /// </summary>
    public static async Task<int> Count(this ILocator locator)
    {
        return await locator.CountAsync();
        
    }

    /// <summary>
    /// Asserts that the given CSS attribute has a value that matches the given expected value
    /// </summary>
    /// <param name="locator">the locator of the element for the assertion to be performed on</param>
    /// <param name="name">the name of the CSS attribute which the value of is being asserted</param>
    /// <param name="expectedValue">the expected value of the given css attribute</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldHaveCss(this ILocator locator, string name, string expectedValue)
    {
        await Assertions.Expect(locator).ToHaveCSSAsync(name, expectedValue);
        
        return locator;
    }

    /// <summary>
    /// Asserts that two given strings are equal
    /// </summary>
    /// <param name="text1">the string to be compared with the second</param>
    /// <param name="text2">the string to be compared with the first</param>
    public static void AreEqual( string text1, string text2)
    {
        text1.Should().Be(text2,  $"the expected text {text1} should match the actual text {text2}");
    }

    /// <summary>
    /// Asserts that the correct radio button option is checked and all others are unchecked
    /// </summary>
    /// <param name="radioGroupLocator">locator for the radio group to be asserted</param>
    /// <param name="optionCheckedStatuses">list of the radio buttons in fieldset. Key = radio option label, Value = isChecked)</param>
    /// <param name="exactLabels">The expected labels require an exact match to the actual component labels</param>
    /// <returns></returns>
    public static async Task<ILocator> ShouldHaveRadioButtonOptions(this ILocator radioGroupLocator,
        List<KeyValuePair<string, bool>> optionCheckedStatuses, bool exactLabels)
    {
        foreach (var option in optionCheckedStatuses)
        {
            await radioGroupLocator
                .GetByLabel(option.Key, new LocatorGetByLabelOptions{Exact = exactLabels})
                .ShouldBeVisible()
                .ShouldBeChecked(option.Value);
        }
        
        return radioGroupLocator;
    }
    /// <summary>
    /// check the visibility of the given locator
    /// </summary>
    /// <param name="locator">locator to check the visibility for</param>
    /// <returns></returns>
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