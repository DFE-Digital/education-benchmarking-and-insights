using System.Collections.Concurrent;
using System.Text;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreateSubmit(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var page = await SetupNavigateInitPage();
        AssertPageLayout(page);
    }

    private async Task<IHtmlDocument> SetupNavigateInitPage()
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
            .SetupComparatorSetApi()
            .SetupHttpContextAccessor(sessionState)
            .Navigate(Paths.TrustComparatorsCreateSubmit(trust.CompanyNumber));

        return page;
    }

    private static void AssertPageLayout(IHtmlDocument page)
    {
        DocumentAssert.TitleAndH1(page,
            "Generating benchmarking data - Financial Benchmarking and Insights Tool - GOV.UK",
            "Generating benchmarking data");
    }
}