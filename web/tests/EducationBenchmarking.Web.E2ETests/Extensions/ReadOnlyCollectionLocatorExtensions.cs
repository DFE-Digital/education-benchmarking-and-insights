using Microsoft.Playwright;

namespace EducationBenchmarking.Web.E2ETests;

public static class ReadOnlyCollectionLocatorExtensions
{
    public static async Task ShouldNotBeVisible(this IReadOnlyCollection<ILocator> locators)
    {
        foreach (var locator in locators)
        {
            await locator.ShouldNotBeVisible();
        }
    }

    public static async Task ShouldBeVisible(this IReadOnlyCollection<ILocator> locators)
    {
        foreach (var locator in locators)
        {
            await locator.ShouldBeVisible();
        }
    }
}