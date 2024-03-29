using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingDetails(BenchmarkingWebAppClient client) : PageBase(client)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies, true)]
    [InlineData(EstablishmentTypes.Maintained, false)]
    public async Task CanDisplay(string financeType, bool isTrust)
    {
        var (page, school) = await SetupNavigateInitPage(financeType, isTrust);

        AssertPageLayout(page, school);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained, false);

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.SchoolHome(school.Urn).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolDetails(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolDetails(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolDetails(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolDetails(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType, bool isTrust)
    {
        var school = isTrust
            ? Fixture.Build<School>()
                .With(x => x.FinanceType, financeType)
                .Create()
            : Fixture.Build<School>()
                .With(x => x.FinanceType, financeType)
                .With(x => x.CompanyNumber, "1223545")
                .With(x => x.TrustOrCompanyName, "Test Trust")
                .With(x => x.OfstedRating, "0")
                .Create();

        var finances = Fixture.Build<Finances>()
            .With(x => x.SchoolName, school.Name)
            .With(x => x.Urn, school.Urn)
            .Create();

        var schools = Fixture.Build<School>().CreateMany(30).ToArray();

        var page = await Client
            .SetupEstablishment(school)
            .SetupBenchmark(schools)
            .SetupInsights(school, finances)
            .Navigate(Paths.SchoolDetails(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolDetails(school.Urn).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.SchoolHome(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Contact details - Financial Benchmarking and Insights Tool - GOV.UK",
            "Contact details");

        if (school.IsPartOfTrust)
        {
            DocumentAssert.Heading2(page, $"Part of {school.TrustOrCompanyName}");
        }
    }
}