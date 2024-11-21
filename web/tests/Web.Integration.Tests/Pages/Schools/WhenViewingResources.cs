using System.Net;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools;

public class WhenViewingResources(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly List<string> AllCostCategories =
    [
        Category.TeachingStaff,
        Category.NonEducationalSupportStaff,
        Category.EducationalSupplies,
        Category.EducationalIct,
        Category.PremisesStaffServices,
        Category.Utilities,
        Category.AdministrativeSupplies,
        Category.CateringStaffServices,
        Category.Other
    ];

    [Fact]
    public async Task CanDisplay()
    {
        var (page, school, rating) = await SetupNavigateInitPage();

        AssertPageLayout(page, school, rating);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolResources(urn));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolResources(urn).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string urn = "12345";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.SchoolResources(urn));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolResources(urn).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, School school, RagRating[] rating)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        Assert.NotNull(school.URN);
        var rating = CreateRagRatings(school.URN);

        var page = await Client.SetupEstablishment(school)
            .SetupMetricRagRating(rating)
            .SetupInsights()
            .SetupExpenditure(school)
            .SetupUserData()
            .Navigate(Paths.SchoolResources(school.URN));

        return (page, school, rating);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school, RagRating[] rating)
    {
        DocumentAssert.AssertPageUrl(page, Paths.SchoolResources(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Find ways to spend less - Financial Benchmarking and Insights Tool - GOV.UK",
            "Find ways to spend less");

        var recommended = page.GetElementById("recommended");

        Assert.NotNull(recommended);

        var categorySections = recommended.QuerySelectorAll(".govuk-grid-column-two-thirds h2.govuk-heading-s");

        var expectedCount = rating
            .Where(x => x.RAG is "red" or "amber")
            .Where(x => x.Category is not Category.Other)
            .Count(x => x.Category is not Category.TeachingStaff && x.Category is not Category.EducationalSupplies && x.Category is not Category.EducationalIct || x.Value >= x.Median);

        Assert.Equal(expectedCount, categorySections.Length);

        foreach (var section in categorySections)
        {
            var sectionHeading = section.TextContent.Trim();

            Assert.NotEqual(Category.Other, sectionHeading);
        }
    }

    private RagRating[] CreateRagRatings(string urn)
    {
        var random = new Random();

        var statusKeys = Lookups.StatusPriorityMap.Keys.ToList();

        var ratings = new List<RagRating>();

        var otherRating = Fixture.Build<RagRating>()
            .With(r => r.Category, Category.Other)
            .With(r => r.RAG, "red")
            .With(r => r.URN, urn)
            .Create();

        ratings.Add(otherRating);

        foreach (var category in AllCostCategories.Where(x => x != Category.Other))
        {
            var rating = Fixture.Build<RagRating>()
                .With(r => r.Category, category)
                .With(r => r.RAG, () => statusKeys[random.Next(statusKeys.Count)])
                .With(r => r.URN, urn)
                .Create();
            ratings.Add(rating);
        }

        return ratings.ToArray();
    }
}