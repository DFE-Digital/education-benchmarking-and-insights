using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.Comparators;

public class WhenViewingComparatorsCreate(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolComparatorsCreate(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorsCreate(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolComparatorsCreate(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorsCreate(urn).ToAbsolute(),
            HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var page = await Client.SetupEstablishment(school)
            .Navigate(Paths.SchoolComparatorsCreate(school.URN));

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
            "Choose your own set of schools - Financial Benchmarking and Insights Tool - GOV.UK",
            "Choose your own set of schools");

        var cta = page.QuerySelector("main .govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.SchoolComparatorsCreateBy(school.URN));
    }
}