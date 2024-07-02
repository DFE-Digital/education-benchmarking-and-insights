using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.Comparators;

public class WhenViewingComparatorsCreateBy(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanNavigateToComparatorsByName()
    {
        var (page, school) = await SetupNavigateInitPage();
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "by", "name"
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorsCreateByName(school.URN).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToComparatorsByCharacteristic()
    {
        var (page, school) = await SetupNavigateInitPage();
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "by", "characteristic"
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorsCreateByCharacteristic(school.URN).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var page = await Client.SetupEstablishment(school)
            .Navigate(Paths.SchoolComparatorsCreateBy(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.URN).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page,
            "How do you want to choose your set of schools? - Financial Benchmarking and Insights Tool - GOV.UK",
            "How do you want to choose your set of schools?");

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolComparatorsCreateBy(school.URN));
    }
}