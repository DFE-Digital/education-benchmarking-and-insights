using Microsoft.Playwright;
namespace Web.E2ETests.Pages.School.Comparators;

public class CreateComparatorsByCharacteristicPage(IPage page) : ICreateComparatorsByPage
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Choose characteristics to find matching schools"
        });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }
}