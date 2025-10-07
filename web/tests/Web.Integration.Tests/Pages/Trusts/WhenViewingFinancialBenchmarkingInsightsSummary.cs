using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingFinancialBenchmarkingInsightsSummary(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, _) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task CanDisplayIntroductionSection()
    {
        var (page, trust, _) = await SetupNavigateInitPage();
        AssertIntroductionSection(page, trust);
    }

    [Fact]
    public async Task CanDisplayKeyInformationSection()
    {
        var (page, trust, balance) = await SetupNavigateInitPage();
        AssertKeyInformationSection(page, trust, balance);
    }

    [Fact]
    public async Task CanDisplayNextStepsSection()
    {
        var (page, trust, _) = await SetupNavigateInitPage();
        AssertNextStepsSection(page, trust);
    }

    private async Task<(IHtmlDocument page, Trust trust, TrustBalance balance)> SetupNavigateInitPage()
    {
        const string companyNumber = "12345678";
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, companyNumber)
            .Create();

        var balance = Fixture.Build<TrustBalance>()
            .With(x => x.CompanyNumber, trust.CompanyNumber)
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .SetupInsights()
            .SetupBalance(trust, balance)
            .Navigate(Paths.TrustFinancialBenchmarkingInsightsSummary(trust.CompanyNumber));

        return (page, trust, balance);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustFinancialBenchmarkingInsightsSummary(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Summary - Financial Benchmarking and Insights Tool - GOV.UK", trust.TrustName!);
    }

    private static void AssertIntroductionSection(IHtmlDocument page, Trust trust)
    {
        var introductionSection = page.QuerySelector("section#introduction");
        Assert.NotNull(introductionSection);

        var link = introductionSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustHome(trust.CompanyNumber), link.GetAttribute("href"));
    }

    private static void AssertKeyInformationSection(IHtmlDocument page, Trust trust, TrustBalance balance)
    {
        var keyInformationSection = page.QuerySelector("section#key-information-section");
        Assert.NotNull(keyInformationSection);
        DocumentAssert.Heading2(keyInformationSection, "Key information about this trust");

        var headlineFiguresTexts = keyInformationSection.ChildNodes
            .QuerySelectorAll(".app-headline-figures.app-headline-summary")
            .Select(e => string.Join(" ", e.ChildNodes.Select(n => n.TextContent.Trim())).Trim());
        Assert.Equal([
            $"In year balance  £{balance.InYearBalance}",
            $"Revenue reserve  £{balance.RevenueReserve}",
            $"Academies  {trust.Schools.Length}"
        ], headlineFiguresTexts);

        var link = keyInformationSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustHome(trust.CompanyNumber), link.GetAttribute("href"));
    }

    private static void AssertNextStepsSection(IHtmlDocument page, Trust trust)
    {
        var nextStepsSection = page.QuerySelector("section#next-steps-section");
        Assert.NotNull(nextStepsSection);
        DocumentAssert.Heading2(nextStepsSection, "Next steps");

        var link = nextStepsSection.ChildNodes.QuerySelector("a");
        Assert.NotNull(link);
        Assert.Equal(Paths.TrustHome(trust.CompanyNumber), link.GetAttribute("href"));
    }
}