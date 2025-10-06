using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingFinancialBenchmarkingInsightsSummary(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task CanDisplayIntroductionSection()
    {
        var (page, trust) = await SetupNavigateInitPage();
        AssertIntroductionSection(page, trust);
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage()
    {
        const string companyNumber = "12345678";
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, companyNumber)
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .Navigate(Paths.TrustFinancialBenchmarkingInsightsSummary(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustFinancialBenchmarkingInsightsSummary(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Summary - Financial Benchmarking and Insights Tool - GOV.UK", trust.TrustName!);
    }

    private static void AssertIntroductionSection(IHtmlDocument page, Trust trust)
    {
        var introductionSection = page.Body.SelectSingleNode("//main/div/section[1]");

        var link = introductionSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustHome(trust.CompanyNumber), link.GetAttribute("href"));
    }
}