using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingDetails(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, schools) = await SetupNavigateInitPage();

        AssertPageLayout(page, trust, schools);
    }

    [Fact]
    public async Task CanNavigateBack()
    {
        /*
         See decision log: temp remove navigation to be review post private beta
         var (page, trust, _) = await SetupNavigateInitPage();

        var anchor = page.QuerySelector(".govuk-back-link");
        page = await Client.Follow(anchor);

        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(trust.CompanyNumber).ToAbsolute());*/
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustDetails(companyName));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustDetails(companyName).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustDetails(companyName));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustDetails(companyName).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust, School[] schools)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .Create();

        var schools = Fixture.Build<School>()
            .With(x => x.TrustCompanyNumber, trust.CompanyNumber)
            .CreateMany(30).ToArray();


        var page = await Client.SetupEstablishment(trust, schools)
            .SetupBalance(trust)
            .SetupInsights()
            .SetupMetricRagRating()
            .SetupBalance(trust)
            .Navigate(Paths.TrustDetails(trust.CompanyNumber));

        return (page, trust, schools);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, School[] schools)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustDetails(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.TrustHome(trust.CompanyNumber).ToAbsolute());

        Assert.NotNull(trust.TrustName);
        DocumentAssert.TitleAndH1(page, "Contact details - Financial Benchmarking and Insights Tool - GOV.UK", "Contact details");

        var details = page.QuerySelector("dl.govuk-summary-list");
        Assert.NotNull(details);

        var schoolList = page.QuerySelector("#current ul");
        Assert.NotNull(schoolList);
        Assert.Equal(schools.Length, schoolList.Children.Length);

        foreach (var schoolElement in schoolList.Children)
        {
            var schoolName = schoolElement.QuerySelector("a")?.TextContent;
            var school = schools.FirstOrDefault(s => s.SchoolName == schoolName);
            Assert.NotNull(school);
            Assert.Equal(trust.CompanyNumber, school.TrustCompanyNumber);
        }
    }
}
