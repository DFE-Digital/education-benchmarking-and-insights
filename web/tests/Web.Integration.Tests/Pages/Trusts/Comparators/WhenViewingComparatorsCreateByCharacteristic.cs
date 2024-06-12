using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreateByCharacteristic(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task CanSubmitCharacteristics()
    {
        var (page, trust) = await SetupNavigateInitPage();
        var action = page.QuerySelector("#submit-characteristic");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "TotalPupils", "true"
                },
                {
                    "TotalPupilsFrom", "10000"
                },
                {
                    "TotalPupilsTo", "10000"
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreatePreview(trust.CompanyNumber).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, Trust trust)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345")
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .SetupTrustInsightApi(new[]
            {
                new TrustCharacteristic
                {
                    CompanyNumber = trust.CompanyNumber,
                    TrustName = trust.TrustName
                }
            })
            .SetupComparatorApi(null, new ComparatorTrusts
            {
                Trusts = new[]
                {
                    "1",
                    "2",
                    "3"
                },
                TotalTrusts = 123
            })
            .SetupHttpContextAccessor()
            .Navigate(Paths.TrustComparatorsCreateByCharacteristic(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        DocumentAssert.BackLink(page, "Back", Paths.TrustComparatorsCreateBy(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Choose characteristics to find matching trusts - Financial Benchmarking and Insights Tool - GOV.UK",
            "Choose characteristics to find matching trusts");
        var cta = page.QuerySelector("#submit-characteristic");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.TrustComparatorsCreateByCharacteristic(trust.CompanyNumber));
    }
}