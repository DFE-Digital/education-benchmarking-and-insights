using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Proxy;

public class WhenRequestingExpenditureHistoryComparison(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();
    private static readonly ExpenditureHistory[] HistorySchool = Fixture.CreateMany<ExpenditureHistory>().ToArray();
    private static readonly ExpenditureHistory[] HistoryComparatorSet = Fixture.CreateMany<ExpenditureHistory>().ToArray();
    private static readonly ExpenditureHistory[] HistoryNational = Fixture.CreateMany<ExpenditureHistory>().ToArray();

    public static IEnumerable<object?[]> ExpenditureHistoryTestData =>
        new List<object?[]>
        {
            new object?[]
            {
                "urn",
                "dimension",
                null,
                null,
                new HistoryComparison<ExpenditureHistory>
                {
                    School = HistorySchool,
                    ComparatorSetAverage = HistoryComparatorSet,
                    NationalAverage = HistoryNational
                }
            },
            new object?[]
            {
                "urn",
                "dimension",
                "phase",
                "financeType",
                new HistoryComparison<ExpenditureHistory>
                {
                    School = HistorySchool,
                    ComparatorSetAverage = HistoryComparatorSet,
                    NationalAverage = HistoryNational
                }
            }
        };

    [Theory]
    [MemberData(nameof(ExpenditureHistoryTestData))]
    public async Task CanReturnCorrectResponseForSchool(string urn, string dimension, string? phase, string? financeType, HistoryComparison<ExpenditureHistory> expected)
    {
        var school = new School
        {
            URN = urn,
            OverallPhase = phase,
            FinanceType = financeType
        };

        var response = await client
            .SetupEstablishment(school)
            .SetupExpenditure(school, HistorySchool, HistoryComparatorSet, HistoryNational)
            .Get(Paths.ApiExpenditureHistoryComparison("school", urn, dimension, phase, financeType));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<HistoryComparison<ExpenditureHistory>>(resultContent);
        Assert.Equivalent(expected, actual);
    }
}