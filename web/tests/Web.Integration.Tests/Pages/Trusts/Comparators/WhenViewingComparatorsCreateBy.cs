using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts.Comparators;

public class WhenViewingComparatorsCreateBy(SchoolBenchmarkingWebAppClient client)
    : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust);
    }

    [Fact]
    public async Task CanNavigateToComparatorsByName()
    {
        var (page, trust) = await SetupNavigateInitPage();
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "by", "name"
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateByName(trust.CompanyNumber).ToAbsolute());
    }

    [Fact]
    public async Task CanNavigateToComparatorsByCharacteristic()
    {
        var (page, trust) = await SetupNavigateInitPage();
        var action = page.QuerySelector(".govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "by", "characteristic"
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustComparatorsCreateByCharacteristic(trust.CompanyNumber).ToAbsolute());
    }

    private async Task<(IHtmlDocument page, Trust Trust)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345")
            .Create();

        var page = await Client.SetupEstablishment(trust)
            .Navigate(Paths.TrustComparatorsCreateBy(trust.CompanyNumber));

        return (page, trust);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your trust", Paths.TrustHome(trust.CompanyNumber).ToAbsolute()),
            ("Comparator sets", Paths.TrustComparators(trust.CompanyNumber).ToAbsolute())
        };
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page,
            "How do you want to choose your own set of trusts? - Financial Benchmarking and Insights Tool - GOV.UK",
            "How do you want to choose your own set of trusts?");

        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Continue", Paths.TrustComparatorsCreateBy(trust.CompanyNumber));
    }
}