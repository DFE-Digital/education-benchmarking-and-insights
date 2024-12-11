using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain;
using Xunit;
namespace Web.Integration.Tests.Proxy;

public class WhenRequestingCensusHistoryComparison(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();
    private static readonly CensusHistory[] HistorySchool = Fixture.CreateMany<CensusHistory>().ToArray();
    private static readonly CensusHistory[] HistoryComparatorSet = Fixture.CreateMany<CensusHistory>().ToArray();
    private static readonly CensusHistory[] HistoryNational = Fixture.CreateMany<CensusHistory>().ToArray();

    public static IEnumerable<object?[]> CensusHistoryTestData =>
        new List<object?[]>
        {
            new object?[]
            {
                "urn",
                "dimension",
                null,
                null,
                new HistoryComparison<CensusHistory>
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
                new HistoryComparison<CensusHistory>
                {
                    School = HistorySchool,
                    ComparatorSetAverage = HistoryComparatorSet,
                    NationalAverage = HistoryNational
                }
            }
        };

    [Theory]
    [MemberData(nameof(CensusHistoryTestData))]
    public async Task CanReturnCorrectResponseForSchool(string urn, string dimension, string? phase, string? financeType, HistoryComparison<CensusHistory> expected)
    {
        var school = new School
        {
            URN = urn,
            OverallPhase = phase,
            FinanceType = financeType
        };

        var response = await client
            .SetupEstablishment(school)
            .SetupCensus(school, HistorySchool, HistoryComparatorSet, HistoryNational)
            .Get(Paths.ApiCensusHistoryComparison(urn, dimension, phase, financeType));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<HistoryComparison<CensusHistory>>(resultContent);
        Assert.Equivalent(expected, actual);
    }
}