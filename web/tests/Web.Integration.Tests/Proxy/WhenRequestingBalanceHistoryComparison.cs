using System.Diagnostics.CodeAnalysis;
using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Proxy;

[SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows")]
public class WhenRequestingIncomeBalanceComparison(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private const int StartYear = 2017;
    private const int EndYear = 2022;
    private static readonly Fixture Fixture = new();
    private static readonly BalanceHistory[] HistorySchool = Fixture.CreateMany<BalanceHistory>().ToArray();
    private static readonly BalanceHistory[] HistoryComparatorSet = Fixture.CreateMany<BalanceHistory>().ToArray();
    private static readonly BalanceHistory[] HistoryNational = Fixture.CreateMany<BalanceHistory>().ToArray();

    public static IEnumerable<object?[]> BalanceHistoryTestData =>
        new List<object?[]>
        {
            new object?[]
            {
                "123456",
                "dimension",
                null,
                null,
                new HistoryComparison<BalanceHistory>
                {
                    StartYear = StartYear,
                    EndYear = EndYear,
                    School = HistorySchool,
                    ComparatorSetAverage = HistoryComparatorSet,
                    NationalAverage = HistoryNational
                }
            },
            new object?[]
            {
                "234567",
                "dimension",
                "phase",
                "financeType",
                new HistoryComparison<BalanceHistory>
                {
                    StartYear = StartYear,
                    EndYear = EndYear,
                    School = HistorySchool,
                    ComparatorSetAverage = HistoryComparatorSet,
                    NationalAverage = HistoryNational
                }
            }
        };

    [Theory]
    [MemberData(nameof(BalanceHistoryTestData))]
    public async Task CanReturnCorrectResponseForSchool(string urn, string dimension, string? phase, string? financeType, HistoryComparison<BalanceHistory> expected)
    {
        var school = new School
        {
            URN = urn,
            OverallPhase = phase,
            FinanceType = financeType
        };

        var historySchool = new BalanceHistoryRows
        {
            StartYear = StartYear,
            EndYear = EndYear,
            Rows = HistorySchool
        };

        var historyComparatorSet = new BalanceHistoryRows
        {
            StartYear = StartYear,
            EndYear = EndYear,
            Rows = HistoryComparatorSet
        };

        var historyNational = new BalanceHistoryRows
        {
            StartYear = StartYear,
            EndYear = EndYear,
            Rows = HistoryNational
        };

        var response = await client
            .SetupSchool(
                school,
                balanceHistorySchool: historySchool,
                balanceHistoryComparatorSet: historyComparatorSet,
                balanceHistoryNational: historyNational)
            .Get(Paths.ApiBalanceHistoryComparison("school", urn, dimension, phase, financeType));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<HistoryComparison<BalanceHistory>>(resultContent);
        Assert.Equivalent(expected, actual);
    }
}