using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreateByName(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(null)]
    [InlineData("redirect-uri")]
    public async Task CanDisplay(string? redirectUri)
    {
        var (page, trust) = await SetupNavigateInitPage(redirectUri);
        AssertPageLayout(page, trust, redirectUri);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("redirect-uri")]
    public async Task CanAddTrustByName(string? redirectUri)
    {
        var (page, trust) = await SetupNavigateInitPage(redirectUri);
        var action = page.QuerySelector("#choose-trust");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.InnerHtml += "<input name=\"trustInput\" value=\"Abbey Academies Trust\" />" +
                           "<input name=\"companyNumber\" value=\"07318714\" />";
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateByName(trust.CompanyNumber, redirectUri).ToAbsolute());

        var cta = page.QuerySelector("#create-set");
        DocumentAssert.PrimaryCta(cta, "Create a set using these trusts", Paths.TrustComparatorsCreateSubmit(trust.CompanyNumber, redirectUri));
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage(string? redirectUri = null)
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "87654321")
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .SetupTrustInsightApi([
                new TrustCharacteristic
                {
                    CompanyNumber = "07318714",
                    TrustName = "Abbey Academies Trust",
                    SchoolsInTrust = 12,
                    TotalPupils = 345,
                    TotalIncome = 123_456_789
                }
            ])
            .SetupHttpContextAccessor()
            .Navigate(Paths.TrustComparatorsCreateByName(trust.CompanyNumber, redirectUri));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, string? redirectUri = null)
    {
        DocumentAssert.BackLink(page, "Back", Paths.TrustComparatorsCreateBy(trust.CompanyNumber, redirectUri).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Choose trusts to benchmark against - Financial Benchmarking and Insights Tool - GOV.UK",
            "Choose trusts to benchmark against");
        var cta = page.QuerySelector("main .govuk-button");
        DocumentAssert.PrimaryCta(cta, "Choose trust", Paths.TrustComparatorsCreateByName(trust.CompanyNumber, redirectUri));
    }
}