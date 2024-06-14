using System.Collections.Concurrent;
using System.Text;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Domain.Benchmark;
using Web.App.Domain.Insight;
using Web.App.Extensions;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreatePreview(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345")
            .Create();

        var key = SessionKeys.TrustComparatorSetUserDefined(trust.CompanyNumber!);
        var set = new UserDefinedTrustComparatorSet
        {
            Set = ["1", "2", "3"],
            TotalTrusts = 123
        };
        var sessionState = new ConcurrentDictionary<string, byte[]>
        {
            [key] = Encoding.ASCII.GetBytes(set.ToJson())
        };

        var page = await Client.SetupEstablishment(trust)
            .SetupTrustInsightApi(new[]
            {
                new TrustCharacteristic
                {
                    CompanyNumber = "114504",
                    TrustName = "Abbey Academies Trust"
                }
            })
            .SetupHttpContextAccessor(sessionState)
            .Navigate(Paths.TrustComparatorsCreatePreview(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        DocumentAssert.BackLink(page, "Back", Paths.TrustComparatorsCreateBy(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Trusts successfully matched - Financial Benchmarking and Insights Tool - GOV.UK",
            "Trusts successfully matched");
        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Create a set using these trusts", Paths.TrustComparatorsCreateSubmit(trust.CompanyNumber));
        var change = page.QuerySelector("#change-set");
        DocumentAssert.Link(change, "Change characteristics and create a new set", Paths.TrustComparatorsCreateByCharacteristic(trust.CompanyNumber).ToAbsolute());
    }
}