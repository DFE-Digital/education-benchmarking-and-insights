using System.Net;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Comparison;

public class WhenRequestingTrustComparisonItSpendDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly TrustItSpend[] _trustItSpend;

    public WhenRequestingTrustComparisonItSpendDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _trustItSpend = Fixture.Build<TrustItSpend>().CreateMany().ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "87654321")
            .Create();

        var comparatorSet = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = "456"
            }
        };

        var userDefinedSet = new UserDefinedSchoolComparatorSet
        {
            Set = ["00000001", "00000002", "00000003"]
        };

        var response = await _client
            .SetupUserData(comparatorSet)
            .SetupComparatorSet(trust, userDefinedSet)
            .SetupItSpend(trustSpend: _trustItSpend)
            .Get(Paths.TrustComparisonItSpendDownload(trust.CompanyNumber!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "benchmark-it-spending-previous-year-87654321.csv"
        };
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(
                "CompanyNumber,TrustName,Connectivity,OnsiteServers,ItLearningResources,AdministrationSoftwareAndSystems,LaptopsDesktopsAndTablets,OtherHardware,ItSupport",
                csvLines.First());
            Assert.Equal(_trustItSpend.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnNotFoundWhenUserDataMissing()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "87654321")
            .Create();

        var response = await _client
            .SetupUserData()
            .Get(Paths.TrustComparisonItSpendDownload(trust.CompanyNumber!));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CanReturnNotFoundWhenComparatorSetMissing()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "87654321")
            .Create();

        var comparatorSet = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = "456"
            }
        };

        var userDefinedSet = new UserDefinedSchoolComparatorSet
        {
            Set = []
        };

        var response = await _client
            .SetupUserData(comparatorSet)
            .SetupComparatorSet(trust, userDefinedSet)
            .Get(Paths.TrustComparisonItSpendDownload(trust.CompanyNumber!));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "87654321")
            .Create();

        var comparatorSet = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = "456"
            }
        };

        var userDefinedSet = new UserDefinedSchoolComparatorSet
        {
            Set = ["00000001", "00000002", "00000003"]
        };

        var response = await _client
            .SetupUserData(comparatorSet)
            .SetupComparatorSet(trust, userDefinedSet)
            .SetupItSpendWithException()
            .Get(Paths.TrustComparisonItSpendDownload(trust.CompanyNumber!));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}