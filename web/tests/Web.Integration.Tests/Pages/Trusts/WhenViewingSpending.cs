using System.Net;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts;

public partial class WhenViewingSpending(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
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
    public async Task CanDisplayNoFilter()
    {
        var (page, trust, ratings, schools) = await SetupNavigateInitPage();

        AssertPageLayout(page, trust, ratings, schools, [], []);
    }

    [Fact]
    public async Task CanDisplayCategoryFilter()
    {
        var (page, trust, ratings, schools) = await SetupNavigateInitPage([Category.TeachingStaff]);

        AssertPageLayout(page, trust, ratings, schools, [Category.TeachingStaff], []);
    }

    [Fact]
    public async Task CanChangeFilter()
    {
        var (page, trust, ratings, schools) = await SetupNavigateInitPage();
        var action = page.QuerySelector("aside .govuk-button");
        Assert.NotNull(action);

        page = await Client.SubmitForm(page.Forms[0], action, f =>
        {
            f.SetFormValues(new Dictionary<string, string>
            {
                {
                    "priority", "medium"
                },
                {
                    "category", "utilities"
                }
            });
        });

        DocumentAssert.AssertPageUrl(page, Paths.TrustSpending(trust.CompanyNumber, [Category.Utilities], ["medium"]).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustSpending(companyName, [], []));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustSpending(companyName, [], []).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyName = "12345678";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustSpending(companyName, [], []));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustSpending(companyName, [], []).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust, RagRating[] ratings, School[] schools)> SetupNavigateInitPage(string[]? categories = null, string[]? priorities = null)
    {
        var random = new Random();

        var trust = Fixture.Build<Trust>()
            .Create();

        var schools = Fixture.Build<School>()
            .With(x => x.OverallPhase, () => OverallPhaseTypes.All.ElementAt(random.Next(0, OverallPhaseTypes.All.Length - 1)))
            .With(x => x.TrustCompanyNumber, trust.CompanyNumber)
            .CreateMany(20).ToArray();

        var ratings = Fixture.Build<RagRating>()
            .OmitAutoProperties()
            .With(x => x.Category, () => categories?.Length > 0 ? categories.First() : AllCostCategories.Values.ElementAt(random.Next(0, AllCostCategories.Keys.Count - 1)))
            .With(x => x.RAG, () => priorities?.Length > 0 ? priorities.First() : Lookups.StatusPriorityMap.Keys.ElementAt(random.Next(0, Lookups.StatusPriorityMap.Keys.Count - 1)))
            .With(x => x.Value)
            .CreateMany(50).ToArray();

        foreach (var rating in ratings)
        {
            rating.URN = schools.ElementAt(random.Next(0, schools.Length - 1)).URN;
        }

        var page = await Client.SetupEstablishment(trust, schools)
            .SetupInsights()
            .SetupMetricRagRating(ratings)
            .SetupBalance(trust)
            .Navigate(Paths.TrustSpending(trust.CompanyNumber, categories ?? [], priorities ?? []));

        return (page, trust, ratings, schools);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, RagRating[] ratings, School[] schools, string[] categories, string[] priorities)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustSpending(trust.CompanyNumber, categories, priorities).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.TrustHome(trust.CompanyNumber).ToAbsolute());

        var dataSourceElement = page.QuerySelector("main > div > div:nth-child(2) > div > p");
        Assert.NotNull(dataSourceElement);

        DocumentAssert.TextEqual(dataSourceElement, "This trust's data covers the financial year September 2021 to August 2022 academies accounts return (AAR).");

        Assert.NotNull(trust.TrustName);
        DocumentAssert.TitleAndH1(page, "Spending priorities for this trust - Financial Benchmarking and Insights Tool - GOV.UK", "Spending priorities for this trust");

        if (categories.Length > 0)
        {
            var heading2s = page.QuerySelectorAll("#spending-tables h2");
            foreach (var heading2 in heading2s)
            {
                Assert.Contains(heading2.GetInnerText(), categories);
            }

            var heading3s = page.QuerySelectorAll("#spending-tables h3");
            foreach (var heading3 in heading3s)
            {
                Assert.EndsWith(" priority", heading3.GetInnerText());
            }
        }
        else
        {
            var heading2s = page.QuerySelectorAll("#spending-tables h2");
            foreach (var heading2 in heading2s)
            {
                Assert.EndsWith(" priority", heading2.GetInnerText());
            }

            var heading3s = page.QuerySelectorAll("#spending-tables h3");
            foreach (var heading3 in heading3s)
            {
                Assert.Contains(heading3.GetInnerText(), AllCostCategories.Values);
            }
        }

        var tables = page.QuerySelectorAll(".table-trust-spending-and-costs");
        var rowCount = 0;
        foreach (var table in tables)
        {
            var head = table.QuerySelector("thead");
            Assert.Equal("Rank School Expenditure", head.GetInnerText().Trim());

            var rows = table.QuerySelectorAll("tbody tr");
            foreach (var row in rows)
            {
                var cols = row.QuerySelectorAll("td");
                var name = cols.ElementAt(1).GetInnerText().Trim();
                var expenditure = cols.ElementAt(2).GetInnerText().Trim();
                var value = decimal.Parse(DecimalRegex().Match(expenditure).Value);
                var units = UnitsRegex().Matches(expenditure)[0].Groups[1].Value;

                Assert.Contains(name, schools.Select(s => s.SchoolName));
                var school = schools.Single(s => s.SchoolName == name);
                var rating = ratings.Where(r => r.URN == school.URN);
                Assert.Contains(value, rating.Select(r => r.Value));
                Assert.Contains(units, new[] { "per pupil", "per square metre" });

                rowCount++;
            }
        }

        Assert.Equal(ratings.Length, rowCount);
    }

    [GeneratedRegex(@"[\d\.]+")]
    private static partial Regex DecimalRegex();

    [GeneratedRegex(@"£[\d\.]+\s([\w\s]+)")]
    private static partial Regex UnitsRegex();
}