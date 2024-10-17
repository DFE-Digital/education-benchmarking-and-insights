using Microsoft.Playwright;
namespace Web.E2ETests.Pages.School.Comparators;

public class CreateComparatorsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1,
        new PageLocatorOptions
        {
            HasText = "Choose your own set of schools"
        });

    private ILocator ContinueButton => page.Locator(Selectors.GovButton,
        new PageLocatorOptions
        {
            HasText = "Continue"
        });

    public async Task IsDisplayed()
    {
        await PageH1Heading.ShouldBeVisible();
    }

    public async Task SignIn(string organisation)
    {
        await page.SignInWithOrganisation(organisation);
    }

    public async Task<CreateComparatorsByPage> ClickContinue()
    {
        await ContinueButton.ClickAsync();
        return new CreateComparatorsByPage(page);
    }
}