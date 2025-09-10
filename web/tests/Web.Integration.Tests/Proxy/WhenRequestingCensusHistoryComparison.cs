using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingCensusHistoryComparison(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private const int StartYear = 2017;
    private const int EndYear = 2022;
    private static readonly Fixture Fixture = new();
    private static readonly CensusHistory[] HistorySchool = Fixture.CreateMany<CensusHistory>().ToArray();
    private static readonly CensusHistory[] HistoryComparatorSet = Fixture.CreateMany<CensusHistory>().ToArray();
    private static readonly CensusHistory[] HistoryNational = Fixture.CreateMany<CensusHistory>().ToArray();

    public static IEnumerable<object?[]> CensusHistoryTestData =>
        new List<object?[]>
        {
            new object?[]
            {
                "123456",
                "dimension",
                null,
                null,
                new HistoryComparison<CensusHistory>
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
                new HistoryComparison<CensusHistory>
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
            .SetupCensus(school, new CensusHistoryRows
            {
                StartYear = StartYear,
                EndYear = EndYear,
                Rows = HistorySchool
            }, new CensusHistoryRows
            {
                StartYear = StartYear,
                EndYear = EndYear,
                Rows = HistoryComparatorSet
            }, new CensusHistoryRows
            {
                StartYear = StartYear,
                EndYear = EndYear,
                Rows = HistoryNational
            })
            .Get(Paths.ApiCensusHistoryComparison(urn, dimension, phase, financeType));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<HistoryComparison<CensusHistory>>(resultContent);
        Assert.Equivalent(expected, actual);
    }
}