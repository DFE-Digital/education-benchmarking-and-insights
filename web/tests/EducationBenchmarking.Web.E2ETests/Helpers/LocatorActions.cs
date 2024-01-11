
using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests.Helpers;

public static class LocatorActions
{
    public static async Task<ILocator> Click(this ILocator locator)
    {
        await locator.ClickAsync();

        return locator;
    }

    public static async Task<ILocator> DoubleClick(this ILocator locator)
    {
        await locator.DblClickAsync();

        return locator;
    }
    
    public static async Task<ILocator> Tap(this ILocator locator)
    {
        await locator.TapAsync();
        
        return locator;
    }
    
    public static async Task<ILocator> Check(this ILocator locator)
    {
        await locator.CheckAsync();

        return locator;
    }

    public static async Task<ILocator> UnCheck(this ILocator locator)
    {
        await locator.UncheckAsync();

        return locator;
    }
    
    [Obsolete("Obsolete")]
    public static async Task<ILocator> Type(this ILocator locator, string inputValue)
    {
        await locator.TypeAsync(inputValue);

        return locator;
    }
    
    public static async Task<ILocator> Fill(this ILocator locator, string inputValue)
    {
        await locator.FillAsync(inputValue);

        return locator;
    }
    
    public static async Task<ILocator> Clear(this ILocator locator)
    {
        await locator.ClearAsync();

        return locator;
    }
    
    public static async Task<ILocator> DragTo(this ILocator dragLocator, ILocator dropLocator)
    {
        await dragLocator.DragToAsync(dropLocator);

        return dragLocator;
    }
    
    public static async Task<ILocator> Press(this ILocator locator, string keyName)
    {
        await locator.PressAsync(keyName);

        return locator;
    }
    
    public static async Task<ILocator> Hover(this ILocator locator)
    {
        await locator.HoverAsync();

        return locator;
    }
    
    public static async Task<ILocator> Select(this ILocator locator, string option)
    {
        await locator.SelectOptionAsync(option);

        return locator;
    }
    
    public static async Task<ILocator> ScrollIntoView(this ILocator locator)
    {
        await locator.ScrollIntoViewIfNeededAsync();

        return locator;
        
    }
    
    public static async Task<ILocator> FilterAndClickFromList(this ILocator locator, string filterBy)
    {
        await locator.Filter(new LocatorFilterOptions {HasText = filterBy}).Click();
        
        return locator;
    }
    
    public static async Task<string> GetInputFieldValue(this ILocator locator)
    {
        return await locator.InputValueAsync();
    }
    
    public static async Task<string> GetInnerText(this ILocator locator)
    {
      return await locator.InnerTextAsync();
    }
    
    public static async Task<ILocator> WaitForElementToBeVisible(this ILocator locator)
    {
        await locator.WaitForAsync();

        return locator;
    }

    public static async Task<ILocator> SetCheckBox(this ILocator locator, bool option)
    {
       await locator.SetCheckedAsync(option);
        return locator;
    }
    
    public static async Task<LocatorBoundingBoxResult> BoundingBox(this ILocator locator)
    {
        var boundingBox = await locator.BoundingBoxAsync();

        return boundingBox ??
               throw new InvalidOperationException(
                   "No bounding box data returned for this element. Check that the locator of the element is valid");
    }
}