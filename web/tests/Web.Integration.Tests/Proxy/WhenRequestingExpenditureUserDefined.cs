using System.Net;
using AutoFixture;
using Newtonsoft.Json;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingExpenditureUserDefined(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    private static readonly Fixture Fixture = new();
    private static readonly TrustExpenditure[] TrustExpenditure = Fixture.CreateMany<TrustExpenditure>().ToArray();

    public static IEnumerable<object?[]> UserDefinedTrustTestData =>
        new List<object?[]>
        {
            new object?[]
            {
                "12345678",
                "dimension",
                "category",
                TrustExpenditure
            }
        };

    [Theory]
    [MemberData(nameof(UserDefinedTrustTestData))]
    public async Task CanReturnCorrectResponseForTrust(string companyNumber, string dimension, string? category, IEnumerable<TrustExpenditure> expected)
    {
        var trust = new Trust
        {
            CompanyNumber = companyNumber
        };

        const string comparatorSetId = nameof(comparatorSetId);
        var userData = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = comparatorSetId
            }
        };

        var comparatorSet = Fixture.Create<UserDefinedTrustComparatorSet>();

        var response = await client
            .SetupEstablishment(trust)
            .SetupUserData(userData)
            .SetupComparatorSetApi(comparatorSet)
            .SetupExpenditureForTrusts(TrustExpenditure)
            .Get(Paths.ApiExpenditureUserDefined("trust", companyNumber, dimension, category));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<IEnumerable<TrustExpenditure>>(resultContent)?.ToList();
        Assert.NotNull(actual);
        Assert.Equivalent(expected, actual);
        Assert.NotNull(actual.Select(a => a.TotalExpenditure).First());
        Assert.NotNull(actual.Select(a => a.CentralTotalExpenditure).First());
        Assert.NotNull(actual.Select(a => a.SchoolTotalExpenditure).First());
    }
}