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
    [Theory]
    [InlineData(true, null)]
    [InlineData(false, null)]
    [InlineData(true, "redirect-uri")]
    [InlineData(false, "redirect-uri")]
    public async Task CanDisplay(bool isEdit, string? redirectUri)
    {
        const string companyNumber = "12345";
        var page = await SetupNavigateInitPage(companyNumber, isEdit, redirectUri);
        AssertPageLayout(page, isEdit);
        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateSubmitted(companyNumber, isEdit, redirectUri).ToAbsolute());
    }

    private async Task<IHtmlDocument> SetupNavigateInitPage(string companyNumber, bool isEdit, string? redirectUri = null)
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, companyNumber)
            .Create();

        var key = SessionKeys.TrustComparatorSetUserDefined(trust.CompanyNumber!);
        var set = new UserDefinedTrustComparatorSet
        {
            Set = ["1", "2", "3"],
            TotalTrusts = 123,
            RunId = isEdit ? Guid.NewGuid().ToString() : null
        };
        var sessionState = new ConcurrentDictionary<string, byte[]>
        {
            [key] = Encoding.ASCII.GetBytes(set.ToJson())
        };

        var page = await Client.SetupEstablishment(trust)
            .SetupTrustInsightApi([
                new TrustCharacteristic
                {
                    CompanyNumber = "114504",
                    TrustName = "Abbey Academies Trust"
                }
            ])
            .SetupComparatorSetApi()
            .SetupHttpContextAccessor(sessionState)
            .Navigate(Paths.TrustComparatorsCreateSubmit(trust.CompanyNumber, redirectUri));

        return page;
    }

    private static void AssertPageLayout(IHtmlDocument page, bool isEdit)
    {
        var expectedTitle = isEdit
            ? "Updating benchmarking data - Financial Benchmarking and Insights Tool - GOV.UK"
            : "Generating benchmarking data - Financial Benchmarking and Insights Tool - GOV.UK";

        var expectedHeading = isEdit
            ? "Updating benchmarking data"
            : "Generating benchmarking data";

        DocumentAssert.TitleAndH1(page, expectedTitle, expectedHeading);
    }
}