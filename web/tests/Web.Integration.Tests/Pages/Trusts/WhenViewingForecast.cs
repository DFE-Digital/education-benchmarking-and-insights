using System.Net;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Pages.Trusts;

public class WhenViewingForecast(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    private const int Year = 2022;

    [Fact]
    public async Task CanDisplay()
    {
        var (page, trust, metrics) = await SetupNavigateInitPage();
        AssertPageLayout(page, trust, metrics);
    }

    [Fact]
    public async Task CanDisplayMissingFinancials()
    {
        var (page, trust, metrics) = await SetupNavigateInitPage(false);
        AssertPageLayout(page, trust, metrics);
    }

    [Fact]
    public async Task CanDisplayNotFound()
    {
        const string companyNumber = "87654321";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustForecast(companyNumber));

        PageAssert.IsNotFoundPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(companyNumber).ToAbsolute(), HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CanDisplayForbidden()
    {
        const string companyNumber = "12121212";
        var page = await Client.SetupEstablishmentWithNotFound()
            .Navigate(Paths.TrustForecast(companyNumber));

        PageAssert.IsForbiddenPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(companyNumber).ToAbsolute(), HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        const string companyNumber = "87654321";
        var page = await Client.SetupEstablishmentWithException()
            .Navigate(Paths.TrustForecast(companyNumber));

        PageAssert.IsProblemPage(page);
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(companyNumber).ToAbsolute(), HttpStatusCode.InternalServerError);
    }

    private async Task<(IHtmlDocument page, Trust trust, BudgetForecastReturnMetric[] metrics)> SetupNavigateInitPage(bool hasMetrics = true)
    {
        var trust = Fixture.Build<Trust>()
            .With(t => t.CompanyNumber, "87654321")
            .Create();

        var returns = Fixture.Build<BudgetForecastReturn>()
            .With(m => m.Year, Year)
            .CreateMany(5)
            .ToArray();

        BudgetForecastReturnMetric[] metrics = [];

        if (hasMetrics)
        {
            metrics =
            [
                new BudgetForecastReturnMetric
                {
                    Metric = BudgetForecastReturnMetricType.ExpenditureAsPercentageOfIncome,
                    Value = Fixture.CreateDecimal(0, 100),
                    Year = Year
                },
                new BudgetForecastReturnMetric
                {
                    Metric = BudgetForecastReturnMetricType.RevenueReserveAsPercentageOfIncome,
                    Value = Fixture.CreateDecimal(0, 100),
                    Year = Year
                },
                new BudgetForecastReturnMetric
                {
                    Metric = BudgetForecastReturnMetricType.Slope,
                    Value = Fixture.CreateDecimal(0, 100),
                    Year = Year
                },
                new BudgetForecastReturnMetric
                {
                    Metric = BudgetForecastReturnMetricType.SlopeFlag,
                    Value = 0,
                    Year = Year
                },
                new BudgetForecastReturnMetric
                {
                    Metric = BudgetForecastReturnMetricType.GrantFundingAsPercentageOfIncome,
                    Value = Fixture.CreateDecimal(0, 100),
                    Year = Year
                },
                new BudgetForecastReturnMetric
                {
                    Metric = BudgetForecastReturnMetricType.StaffCostsAsPercentageOfIncome,
                    Value = Fixture.CreateDecimal(0, 100),
                    Year = Year
                },
                new BudgetForecastReturnMetric
                {
                    Metric = BudgetForecastReturnMetricType.SelfGeneratedIncomeAsPercentageOfIncome,
                    Value = Fixture.CreateDecimal(0, 100),
                    Year = Year
                }
            ];
        }

        var page = await Client.SetupEstablishment(trust)
            .SetupBudgetForecast(trust, returns, metrics, Year)
            .Navigate(Paths.TrustForecast(trust.CompanyNumber));

        return (page, trust, metrics);
    }

    private static void AssertPageLayout(IHtmlDocument page, Trust trust, BudgetForecastReturnMetric[] metrics)
    {
        DocumentAssert.AssertPageUrl(page, Paths.TrustForecast(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.TrustHome(trust.CompanyNumber).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Forecast and risks - Financial Benchmarking and Insights Tool - GOV.UK", "Forecast and risks");

        if (metrics.Length > 0)
        {
            var yearCommentary = page.QuerySelector("#bfr-metrics-year").GetInnerText();
            Assert.Equal("This data comes from the 2022 BFR.", yearCommentary);

            var metricsTable = page.QuerySelector("#bfr-metrics tbody");
            Assert.NotNull(metricsTable);
            var metricsRows = metricsTable.GetElementsByTagName("tr");
            Assert.Equal(4, metricsRows.Length);

            Assert.Equal($"Revenue reserves as a percentage of income {metrics.Single(m => m.Metric == BudgetForecastReturnMetricType.RevenueReserveAsPercentageOfIncome).Value}%", metricsRows.ElementAt(0).GetInnerText().Trim());
            Assert.Equal($"Staff costs as a percentage of income {metrics.Single(m => m.Metric == BudgetForecastReturnMetricType.StaffCostsAsPercentageOfIncome).Value}%", metricsRows.ElementAt(1).GetInnerText().Trim());
            Assert.Equal($"Expenditure as percentage of income {metrics.Single(m => m.Metric == BudgetForecastReturnMetricType.ExpenditureAsPercentageOfIncome).Value}%", metricsRows.ElementAt(2).GetInnerText().Trim());
            Assert.Equal($"Self-generated income vs grant funding {metrics.Single(m => m.Metric == BudgetForecastReturnMetricType.SelfGeneratedIncomeAsPercentageOfIncome).Value}% / {metrics.Single(m => m.Metric == BudgetForecastReturnMetricType.GrantFundingAsPercentageOfIncome).Value}%", metricsRows.ElementAt(3).GetInnerText().Trim());
        }
        else
        {
            var warningText = page.QuerySelector(".govuk-warning-text").GetInnerText();
            Assert.Contains("This trust has no submission for the current period.", warningText);
        }
    }
}