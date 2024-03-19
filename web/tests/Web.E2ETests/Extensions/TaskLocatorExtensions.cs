using Microsoft.Playwright;

namespace Web.E2ETests;

public static class TaskLocatorExtensions
{
    public static async Task<ILocator> ShouldBeChecked(this Task<ILocator> locator, bool isChecked = true)
    {
        var l = await locator;
        return await l.ShouldBeChecked(isChecked);
    }

    public static async Task<ILocator> ShouldBeEnabled(this Task<ILocator> locator)
    {
        var l = await locator;
        return await l.ShouldBeEnabled();
    }

    public static async Task<ILocator> ShouldHaveAttribute(this Task<ILocator> locator, string attributeName, string value)
    {
        var l = await locator;
        return await l.ShouldHaveAttribute(attributeName, value);
    }

    public static async Task<ILocator> ShouldHaveText(this Task<ILocator> locator, string expectedText)
    {
        var l = await locator;
        return await l.ShouldHaveText(expectedText);
    }

    public static async Task<ILocator> Press(this Task<ILocator> locator, string key)
    {
        var l = await locator;
        return await l.Press(key);
    }
}