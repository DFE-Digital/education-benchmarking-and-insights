using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.Comparators;

public class WhenViewingComparatorsCreateByName(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanAddSchoolByName()
    {
        var (page, school) = await SetupNavigateInitPage();
        var action = page.QuerySelector("#choose-school");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.InnerHtml += "<input name=\"schoolInput\" value=\"Forest Row Church of England Primary School\" />" +
                           "<input name=\"urn\" value=\"114504\" />";
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorsCreateByName(school.URN).ToAbsolute());

        var cta = page.QuerySelector("#create-set");
        DocumentAssert.PrimaryCta(cta, "Create a set using these schools", Paths.SchoolComparatorsCreateSubmit(school.URN));
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var page = await Client.SetupEstablishment(school)
            .SetupSchoolInsightApi(new[]
            {
                new SchoolCharacteristic
                {
                    URN = "114504",
                    SchoolName = "Forest Row Church of England Primary School",
                    OverallPhase = "Primary",
                    Address = "Forest Row, RH18 5EB"
                }
            })
            .SetupHttpContextAccessor()
            .Navigate(Paths.SchoolComparatorsCreateByName(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolComparatorsCreateBy(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Choose schools to benchmark against - Financial Benchmarking and Insights Tool - GOV.UK",
            "Choose schools to benchmark against");
        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Choose school", Paths.SchoolComparatorsCreateByName(school.URN));
    }
}