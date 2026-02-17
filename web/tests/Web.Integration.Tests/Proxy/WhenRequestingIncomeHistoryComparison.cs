using System.Diagnostics.CodeAnalysis;
using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Proxy;

[SuppressMessage("Usage", "xUnit1042:The member referenced by the MemberData attribute returns untyped data rows")]
public class WhenRequestingIncomeHistoryComparison(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private const int StartYear = 2017;
    private const int EndYear = 2022;
    private static readonly Fixture Fixture = new();
    private static readonly IncomeHistory[] HistorySchool = Fixture.CreateMany<IncomeHistory>().ToArray();
    private static readonly IncomeHistory[] HistoryComparatorSet = Fixture.CreateMany<IncomeHistory>().ToArray();
    private static readonly IncomeHistory[] HistoryNational = Fixture.CreateMany<IncomeHistory>().ToArray();

    public static IEnumerable<object?[]> IncomeHistoryTestData =>
        new List<object?[]>
        {
            new object?[]
            {
                "123456",
                "dimension",
                null,
                null,
                new HistoryComparison<IncomeHistory>
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
                new HistoryComparison<IncomeHistory>
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
    [MemberData(nameof(IncomeHistoryTestData))]
    public async Task CanReturnCorrectResponseForSchool(string urn, string dimension, string? phase, string? financeType, HistoryComparison<IncomeHistory> expected)
    {
        var school = new School
        {
            URN = urn,
            OverallPhase = phase,
            FinanceType = financeType
        };

        var historySchool = new IncomeHistoryRows
        {
            StartYear = StartYear,
            EndYear = EndYear,
            Rows = HistorySchool
        };

        var historyComparatorSet = new IncomeHistoryRows
        {
            StartYear = StartYear,
            EndYear = EndYear,
            Rows = HistoryComparatorSet
        };

        var historyNational = new IncomeHistoryRows
        {
            StartYear = StartYear,
            EndYear = EndYear,
            Rows = HistoryNational
        };

        var response = await client
            .SetupSchool(
                school,
                historySchool: historySchool,
                historyComparatorSet: historyComparatorSet,
                historyNational: historyNational)
            .Get(Paths.ApiIncomeHistoryComparison("school", urn, dimension, phase, financeType));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<HistoryComparison<IncomeHistory>>(resultContent);
        Assert.Equivalent(expected, actual);
    }
}