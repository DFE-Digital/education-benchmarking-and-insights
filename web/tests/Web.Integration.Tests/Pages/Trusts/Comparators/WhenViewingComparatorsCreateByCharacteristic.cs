using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreateByCharacteristic(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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
    public async Task CanSubmitCharacteristics(string? redirectUri)
    {
        var (page, trust) = await SetupNavigateInitPage(redirectUri);
        var action = page.QuerySelector("#submit-characteristic");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                { "TotalPupils", "true" },
                { "TotalPupilsFrom", "10000" },
                { "TotalPupilsTo", "10000" }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreatePreview(trust.CompanyNumber, redirectUri).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage(string? redirectUri = null)
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345678")
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .SetupTrustInsightApi([
                new TrustCharacteristic
                {
                    CompanyNumber = trust.CompanyNumber,
                    TrustName = trust.TrustName
                }
            ])
            .SetupComparatorApi(null, new ComparatorTrusts
            {
                Trusts =
                [
                    "1",
                    "2",
                    "3"
                ],
                TotalTrusts = 123
            })
            .SetupHttpContextAccessor()
            .Navigate(Paths.TrustComparatorsCreateByCharacteristic(trust.CompanyNumber, redirectUri));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, string? redirectUri = null)
    {
        DocumentAssert.BackLink(page, "Back", Paths.TrustComparatorsCreateBy(trust.CompanyNumber, redirectUri).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Choose characteristics to find matching trusts - Financial Benchmarking and Insights Tool - GOV.UK",
            "Choose characteristics to find matching trusts");
        var cta = page.QuerySelector("#submit-characteristic");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.TrustComparatorsCreateByCharacteristic(trust.CompanyNumber, redirectUri));
    }
}