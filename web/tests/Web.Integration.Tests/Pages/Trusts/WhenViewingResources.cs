using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingResources(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, resources) = await SetupNavigateInitPage();

        AssertPageLayout(page, trust, resources);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustResources(companyName));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustResources(companyName).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustResources(companyName));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustResources(companyName).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust, CommercialResources[] resources)> SetupNavigateInitPage()
    {
        var trust = Fixture.Build<Trust>()
            .With(t => t.CompanyNumber, "12345678")
            .Create();

        var arrangeResources = new List<CommercialResources>();

        foreach (var category in Category.All)
        {
            if (!CommercialResourcesBuilder.CategoryToSubCategories.TryGetValue(category, out var validSubCategories))
                continue;

            foreach (var subCategory in validSubCategories)
            {
                arrangeResources.Add(new CommercialResources
                {
                    Title = $"Resource for {category} - {subCategory}",
                    Url = $"https://example.com/{category}/{subCategory}",
                    Category = [category],
                    SubCategory = [subCategory]
                });
            }
        }

        var resources = arrangeResources.ToArray();

        var page = await Client.SetupEstablishment(trust)
            .SetupInsights()
            .SetupInsightsCommercialResources(resources)
            .Navigate(Paths.TrustResources(trust.CompanyNumber));

        return (page, trust, resources);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, CommercialResources[] resources)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustResources(trust.CompanyNumber).ToAbsolute());

        DocumentAssert.TitleAndH1(page, "Find ways to spend less - Financial Benchmarking and Insights Tool - GOV.UK", "Find ways to spend less");

        AssertAccordionContents(page, resources);
    }

    private static void AssertAccordionContents(IHtmlDocument page, CommercialResources[] resources)
    {
        var accordion = page.GetElementById("accordion-resources");
        Assert.NotNull(accordion);

        var accordionHeadings = page.QuerySelectorAll("[id^='accordion-resources-heading-']");

        var accordionContent = page.QuerySelectorAll("[id^='accordion-resources-content-']");

        for (var i = 0; i < 9; i++)
        {
            var heading = accordionHeadings[i].Text().Trim();
            var expectedHeading = Category.All[i];
            Assert.Equal(heading, expectedHeading);

            var subHeadings = accordionContent[i].QuerySelectorAll("h3");

            foreach (var subHeading in subHeadings)
            {
                var subHeadingText = subHeading.TextContent.Trim();

                var expectedResources = resources
                    .Where(r => r.Category.Contains(expectedHeading) && r.SubCategory.Contains(subHeadingText))
                    .ToList();

                var resourceList = subHeading.NextElementSibling;
                Assert.NotNull(resourceList);

                var resourceLinks = resourceList.QuerySelectorAll("li a");

                foreach (var link in resourceLinks)
                {
                    var actualTitle = link.TextContent.Trim().Replace(" Opens in a new window", "");
                    ;
                    var actualUrl = link.GetAttribute("href");

                    var matchingResource = expectedResources.FirstOrDefault(r => r.Title == actualTitle);

                    Assert.NotNull(matchingResource);
                    Assert.Equal(matchingResource.Url, actualUrl);
                }
            }
        }
    }
}