using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreateByName(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task CanAddTrustByName()
    {
        var (page, trust) = await SetupNavigateInitPage();
        var action = page.QuerySelector("#choose-trust");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.InnerHtml += "<input name=\"trustInput\" value=\"Abbey Academies Trust\" />" +
                           "<input name=\"companyNumber\" value=\"07318714\" />";
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateByName(trust.CompanyNumber).ToAbsolute());

        var cta = page.QuerySelector("#create-set");
        DocumentAssert.PrimaryCta(cta, "Create a set using these trusts", Paths.TrustComparatorsCreateSubmit(trust.CompanyNumber));
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "87654321")
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .SetupTrustInsightApi(new[]
            {
                new TrustCharacteristic
                {
                    CompanyNumber = "07318714",
                    TrustName = "Abbey Academies Trust",
                    SchoolsInTrust = 12,
                    TotalPupils = 345,
                    TotalIncome = 123_456_789
                }
            })
            .SetupHttpContextAccessor()
            .Navigate(Paths.TrustComparatorsCreateByName(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        DocumentAssert.BackLink(page, "Back", Paths.TrustComparatorsCreateBy(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Choose trusts to benchmark against - Financial Benchmarking and Insights Tool - GOV.UK",
            "Choose trusts to benchmark against");
        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Choose trust", Paths.TrustComparatorsCreateByName(trust.CompanyNumber));
    }
}