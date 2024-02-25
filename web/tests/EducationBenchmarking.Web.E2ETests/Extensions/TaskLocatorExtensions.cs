using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests;

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
}