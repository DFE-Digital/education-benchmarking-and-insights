﻿using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingHome(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private static readonly Dictionary<int, string> AllCostCategories = new()
    {
        {
            1, Category.TeachingStaff
        },
        {
            2, Category.NonEducationalSupportStaff
        },
        {
            3, Category.EducationalSupplies
        },
        {
            4, Category.EducationalIct
        },
        {
            5, Category.PremisesStaffServices
        },
        {
            6, Category.Utilities
        },
        {
            7, Category.AdministrativeSupplies
        },
        {
            8, Category.CateringStaffServices
        },
        {
            9, Category.Other
        }
    };

    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, ratings, schools) = await SetupNavigateInitPage();

        AssertPageLayout(page, trust, ratings, schools);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustHome(companyName));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(companyName).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustHome(companyName));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(companyName).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust, RagRating[] ratings, TrustSchool[] schools)> SetupNavigateInitPage()
    {
        var random = new Random();

        var trust = Fixture.Build<Trust>()
            .Create();

        var schools = Fixture.Build<TrustSchool>()
            .With(x => x.OverallPhase, () => OverallPhaseTypes.All.ElementAt(random.Next(0, OverallPhaseTypes.All.Length - 1)))
            .CreateMany(20).ToArray();

        var values = AllCostCategories.Where(k => k.Key != 9).Select(c => c.Value);
        var queue = new Queue<string>();
        var ratings = Fixture.Build<RagRating>()
            .OmitAutoProperties()
            .With(x => x.Category, () =>
            {
                if (queue.Count == 0)
                {
                    foreach (var value in values)
                    {
                        queue.Enqueue(value);
                    }
                }

                return queue.Dequeue();
            })
            .With(x => x.RAG, () => Lookups.StatusPriorityMap.Keys.ElementAt(random.Next(0, Lookups.StatusPriorityMap.Keys.Count - 1)))
            .CreateMany(50).ToArray();

        foreach (var rating in ratings)
        {
            rating.URN = schools.ElementAt(random.Next(0, schools.Length - 1)).URN;
        }

        var page = await Client.SetupEstablishment(trust, schools)
            .SetupInsights()
            .SetupMetricRagRating(ratings)
            .SetupBalance(trust)
            .Navigate(Paths.TrustHome(trust.CompanyNumber));

        return (page, trust, ratings, schools);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, RagRating[] ratings, TrustSchool[] schools)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.TrustHome(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);

        var dataSourceElement = page.QuerySelector("main > div > div:nth-child(2) > div > p");
        Assert.NotNull(dataSourceElement);

        DocumentAssert.TextEqual(dataSourceElement, "This trust's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");

        Assert.NotNull(trust.TrustName);
        DocumentAssert.TitleAndH1(page, "Your trust - Financial Benchmarking and Insights Tool - GOV.UK", trust.TrustName);

        // headlines
        var highPriorityHeadline = page.QuerySelector("li.app-headline-high") as IHtmlListItemElement;
        Assert.Equal($"{ratings.Count(r => r.Category != "Other" && r.RAG == "red")}\n\nHigh priority costs", highPriorityHeadline.GetInnerText());

        var mediumPriorityHeadline = page.QuerySelector("li.app-headline-medium") as IHtmlListItemElement;
        Assert.Equal($"{ratings.Count(r => r.Category != "Other" && r.RAG == "amber")}\n\nMedium priority costs", mediumPriorityHeadline.GetInnerText());

        var lowPriorityHeadline = page.QuerySelector("li.app-headline-low") as IHtmlListItemElement;
        Assert.Equal($"{ratings.Count(r => r.Category != "Other" && r.RAG == "green")}\n\nLow priority costs", lowPriorityHeadline.GetInnerText());

        // cost categories
        var costCategoryRag = page.QuerySelector("table.table-cost-category-rag") as IHtmlTableElement;
        var costCategoryRagRows = costCategoryRag?.Bodies.First().Rows;
        Assert.Equal(AllCostCategories.Count - 1, costCategoryRagRows?.Length);

        // school phases

        var primarySchoolRag = page.QuerySelector("#school-rag-primary-schools") as IHtmlTableElement;
        var primarySchoolRagRows = primarySchoolRag?.Bodies.First().Rows;
        Assert.Equal(schools.Count(s => s.OverallPhase == OverallPhaseTypes.Primary), primarySchoolRagRows?.Length ?? 0);

        var secondarySchoolRag = page.QuerySelector("#school-rag-secondary-schools") as IHtmlTableElement;
        var secondarySchoolRagRows = secondarySchoolRag?.Bodies.First().Rows;
        Assert.Equal(schools.Count(s => s.OverallPhase == OverallPhaseTypes.Secondary), secondarySchoolRagRows?.Length ?? 0);

        var specialSchoolRag = page.QuerySelector("#school-rag-special") as IHtmlTableElement;
        var specialSchoolRagRows = specialSchoolRag?.Bodies.First().Rows;
        Assert.Equal(schools.Count(s => s.OverallPhase is OverallPhaseTypes.Special), specialSchoolRagRows?.Length ?? 0);

        var alternativeProvisionSchoolRag = page.QuerySelector("#school-rag-alternative-provision") as IHtmlTableElement;
        var alternativeProvisionSchoolRagRows = alternativeProvisionSchoolRag?.Bodies.First().Rows;
        Assert.Equal(schools.Count(s => s.OverallPhase is OverallPhaseTypes.AlternativeProvision), alternativeProvisionSchoolRagRows?.Length ?? 0);

        var postSixteenSchoolRag = page.QuerySelector("#school-rag-post-16") as IHtmlTableElement;
        var postSixteenSchoolRagRows = postSixteenSchoolRag?.Bodies.First().Rows;
        Assert.Equal(schools.Count(s => s.OverallPhase == OverallPhaseTypes.PostSixteen), postSixteenSchoolRagRows?.Length ?? 0);

        var universityTechnicalCollegeRag = page.QuerySelector("#school-rag-university-technical-colleges") as IHtmlTableElement;
        var universityTechnicalCollegeRagRows = universityTechnicalCollegeRag?.Bodies.First().Rows;
        Assert.Equal(schools.Count(s => s.OverallPhase == OverallPhaseTypes.UniversityTechnicalCollege), universityTechnicalCollegeRagRows?.Length ?? 0);
    }
}