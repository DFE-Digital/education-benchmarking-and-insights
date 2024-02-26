using Microsoft.Playwright;
using Xunit;

namespace EducationBenchmarking.Web.E2ETests.Pages.School;

public class SchoolDetailsPage(IPage page)
{
    private ILocator PageH1Heading => page.Locator(Selectors.H1);
    private ILocator ChangeSchoolLink => page.Locator(Selectors.ChangeSchoolLink);
    private ILocator BackLink => page.Locator(Selectors.GovBackLink);
    private ILocator GiasPageLink => page.Locator(Selectors.SchoolGiasPageLink);
    private ILocator EmailAddressField => page.Locator(Selectors.SchoolDetailsEmailAddress);


    public async Task IsDisplayed()
    {
      await PageH1Heading.ShouldBeVisible();
      await ChangeSchoolLink.ShouldBeVisible();
      await BackLink.ShouldBeVisible();
      await GiasPageLink.ShouldBeVisible();
     await EmailAddressField.ShouldHaveText("Log in to view contact information");
     Assert.True(await GiasPageLink.GetAttributeAsync("target") == "_blank");

    }
}