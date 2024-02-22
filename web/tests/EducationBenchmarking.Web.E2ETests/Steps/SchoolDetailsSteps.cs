using EducationBenchmarking.Web.E2ETests.Pages;

namespace EducationBenchmarking.Web.E2ETests.Steps;

[Binding]
public class SchoolDetailsSteps(SchoolDetailsPage schoolDetailsStepsPage)
{
    [Then(@"I am navigated to school details page for school with urn '(.*)'")]
    public async Task ThenIAmNavigatedToSchoolDetailsPageForSchoolWithUrn(string urn)
    {
        await schoolDetailsStepsPage.WaitForPage(urn);
        await schoolDetailsStepsPage.AssertPage();
    }
}