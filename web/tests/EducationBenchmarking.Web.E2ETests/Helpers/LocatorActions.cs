
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Helpers;

public static class LocatorActions
{
    /// <summary>
    /// <para>Executes multiple, awaitable locator actions in the given order</para>
    /// <para>Only use this when executing more than 1 action on the given element in sequence</para>
    /// <para>**Usage**</para>
    /// <code>
    /// // To scroll down to a locator and then click on it
    /// await page.ExecuteMultipleActions(l => l.ScrollIntoView(), l => l.Click());
    /// </code>
    /// </summary>
    /// <param name="locator">the locator of the element for the actions to be performed on</param>
    /// <param name="actions">sequence of the chained actions to be executed in order as lambda expressions</param>
    public static async Task ExecuteMultipleActions(this ILocator locator,
        params Func<ILocator, Task<ILocator>>[] actions)
    {
        var data = locator;
        foreach (var action in actions)
        {
            var response = await action(data);
            data = response;
        }
    }
    
    /// <summary>
    /// <para>Clicks on the centre of the given element.</para>
    /// <para>Executes the following actions in part:</para>
    /// <list type="ordinal">
    /// <item><description>Wait for actionability</description></item>
    /// <item><description>Scrolls the element into view if needed</description></item>
    /// <item><description>Clicks on the centre of the element</description></item>
    /// <item><description>Waits for the initiated action to pass or fail</description></item>
    /// </list>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> Click(this ILocator locator)
    {
        await locator.ClickAsync();

        return locator;
    }

    ///<summary>
    /// <para>Double clicks on the centre of the given element.</para>
    /// <para>Executes the following actions in part:</para>
    /// <list type="ordinal">
    /// <item><description>Wait for actionability</description></item>
    /// <item><description>Scrolls the element into view if needed</description></item>
    /// <item><description>Double clicks on the centre of the element</description></item>
    /// <item><description>Waits for the initiated action to pass or fail</description></item>
    /// </list>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> DoubleClick(this ILocator locator)
    {
        await locator.DblClickAsync();

        return locator;
    }

    ///<summary>
    /// <para>Taps on the centre of the given element</para>
    /// <para>Use instead of 'click' for touchscreens</para>
    /// <para>Executes the following actions in part:</para>
    /// <list type="ordinal">
    /// <item><description>Wait for actionability</description></item>
    /// <item><description>Scrolls the element into view if needed</description></item>
    /// <item><description>Touchscreen tap on centre of the element</description></item>
    /// <item><description>Waits for the initiated action to pass or fail</description></item>
    /// </list>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> Tap(this ILocator locator)
    {
        await locator.TapAsync();
        
        return locator;
    }
    
    ///<summary>
    /// <para>Checks checkbox and radio button elements</para>
    /// <para>Executes the following actions in part:</para>
    /// <list type="ordinal">
    /// <item><description>Ensures that the element is a checkbox or radio button</description></item>
    /// <item><description>Wait for actionability</description></item>
    /// <item><description>Scrolls the element into view if needed</description></item>
    /// <item><description>Mouse clicks on centre of the element</description></item>
    /// <item><description>Waits for the initiated action to pass or fail (if any)</description></item>
    /// <item><description>Ensures that the element is now checked</description></item>
    /// </list>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> Check(this ILocator locator)
    {
        await locator.CheckAsync();

        return locator;
    }

    ///<summary>
    /// <para>Un-checks checkbox and radio button elements</para>
    /// <para>Executes the following actions in part:</para>
    /// <list type="ordinal">
    /// <item><description>Ensures that the element is a checkbox or radio button</description></item>
    /// <item><description>Wait for actionability</description></item>
    /// <item><description>Scrolls the element into view if needed</description></item>
    /// <item><description>Mouse clicks on centre of the element</description></item>
    /// <item><description>Waits for the initiated action to pass or fail (if any)</description></item>
    /// <item><description>Ensures that the element is now un-checked</description></item>
    /// </list>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> UnCheck(this ILocator locator)
    {
        await locator.UncheckAsync();

        return locator;
    }

    /// <summary>
    /// <para>Types the given text string into the given text input element</para>
    /// <para>
    /// This action does not clear the text input. If the input already has a text value then this action will
    /// cause more text to be typed after it.
    /// </para>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <param name="inputValue">the string to be typed into the text input</param>
    /// <returns></returns>
    public static async Task<ILocator> Type(this ILocator locator, string inputValue)
    {
        await locator.TypeAsync(inputValue);

        return locator;
    }

    /// <summary>
    /// <para>Fills the given text input element with the given string</para>
    /// <para>
    /// This action can be used when any current text value of the element needs to be overriden
    /// </para>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <param name="inputValue">the string to be filled into the text input</param>
    /// <returns></returns>
    public static async Task<ILocator> Fill(this ILocator locator, string inputValue)
    {
        await locator.FillAsync(inputValue);

        return locator;
    }

    /// <summary>
    /// Clears the given text input of any input value
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> Clear(this ILocator locator)
    {
        await locator.ClearAsync();

        return locator;
    }

    /// <summary>
    /// Drags the given drag element to the given drop zone element
    /// </summary>
    /// <param name="dragLocator">the locator of the element to be dragged</param>
    /// <param name="dropLocator">the locator of the element where the drag object should be dropped</param>
    /// <returns></returns>
    public static async Task<ILocator> DragTo(this ILocator dragLocator, ILocator dropLocator)
    {
        await dragLocator.DragToAsync(dropLocator);

        return dragLocator;
    }
    
    /// <summary>
    /// <para>Focuses on the given element and presses a keyboard key(s)</para>
    /// <para>The following keys are supported and should be specified as follows:</para>
    /// <para>
    /// <c>F1</c> - <c>F12</c>, <c>Digit0</c>- <c>Digit9</c>, <c>KeyA</c>- <c>KeyZ</c>,
    /// <c>Backquote</c>, <c>Minus</c>, <c>Equal</c>, <c>Backslash</c>, <c>Backspace</c>,
    /// <c>Tab</c>, <c>Delete</c>, <c>Escape</c>, <c>ArrowDown</c>, <c>End</c>, <c>Enter</c>,
    /// <c>Home</c>, <c>Insert</c>, <c>PageDown</c>, <c>PageUp</c>, <c>ArrowRight</c>, <c>ArrowUp</c>,
    /// </para>
    /// <para>Key presses can also be combined. Such as: <c>Control+o</c></para>
    /// <para>**Usage**</para>
    /// <code>
    /// await textInput.Press("Enter");
    /// </code>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <param name="keyName">the name of the key(s) to be pressed (see examples above)</param>
    /// <returns></returns>
    public static async Task<ILocator> Press(this ILocator locator, string keyName)
    {
        await locator.PressAsync(keyName);

        return locator;
    }

    /// <summary>Hovers the cursor over the given element</summary>
    /// <summary>
    /// <list type="ordinal">
    /// <item><description>Wait for actionability</description></item>
    /// <item><description>Scrolls the element into view if needed</description></item>
    /// <item><description>Hover the cursor over the centre of the element</description></item>
    /// <item><description>Waits for the initiated action to pass or fail</description></item>
    /// </list>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> Hover(this ILocator locator)
    {
        await locator.HoverAsync();

        return locator;
    }
    
    /// <summary>
    /// Selects a given option on a select element (only) from the array of given option elements
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <param name="option">option to be selected from option elements within given select elements</param>
    /// <returns></returns>
    public static async Task<ILocator> Select(this ILocator locator, string option)
    {
        await locator.SelectOptionAsync(option);

        return locator;
    }

    /// <summary>
    /// Scrolls the given element to view if it is not already shown in the viewport
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> ScrollIntoView(this ILocator locator)
    {
        await locator.ScrollIntoViewIfNeededAsync();

        return locator;
        
    }

    /// <summary>
    /// Filters down the elements that match the given selector (by it's text) and then clicks on that element
    /// </summary>
    /// <param name="locator">the locator of the elements for the action to be performed on</param>
    /// <param name="filterBy">the text string that matches the required element</param>
    /// <returns></returns>
    public static async Task<ILocator> FilterAndClickFromList(this ILocator locator, string filterBy)
    {
        await locator.Filter(new LocatorFilterOptions {HasText = filterBy}).Click();
        
        return locator;
    }

    /// <summary>
    /// <para>Returns the input value of the given element (as a string)</para>
    /// <para>Works for <c>&lt;input&gt;</c>, <c>&lt;textarea&gt;</c> and <c>&lt;select&gt;</c> elements</para>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<string> GetInputFieldValue(this ILocator locator)
    {
        return await locator.InputValueAsync();
    }

    /// <summary>
    /// Returns the inner text of the given element (as a string)
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<string> GetInnerText(this ILocator locator)
    {
      return await locator.InnerTextAsync();
    }

    /// <summary>
    /// Waits for the given element to be visible before continuing
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    public static async Task<ILocator> WaitForElementToBeVisible(this ILocator locator)
    {
        await locator.WaitForAsync();

        return locator;
    }
    /// <summary>
    /// set the state of checkbox or radio element
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    ///  <param name="option">the state that needs to be set for the element</param>
    /// <returns></returns>
    public static async Task<ILocator> SetCheckBox(this ILocator locator, bool option)
    {
       await locator.SetCheckedAsync(option);
        return locator;
    }

    /// <summary>
    /// <para>Returns the bounding box values of the given element</para>
    /// <para>The bounding box result includes the X, Y, Height and Width values for the element</para>
    /// </summary>
    /// <param name="locator">the locator of the element for the action to be performed on</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<LocatorBoundingBoxResult> BoundingBox(this ILocator locator)
    {
        var boundingBox = await locator.BoundingBoxAsync();

        return boundingBox ??
               throw new InvalidOperationException(
                   "No bounding box data returned for this element. Check that the locator of the element is valid");
    }
}