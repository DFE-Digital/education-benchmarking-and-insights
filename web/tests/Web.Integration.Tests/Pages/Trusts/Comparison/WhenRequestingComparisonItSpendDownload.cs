using System.Net;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Trusts.Comparison;

public class WhenRequestingTrustComparisonItSpendDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly TrustItSpend[] _trustItSpend;
    private readonly TrustItSpendForecastYear[] _trustForecast;

    public WhenRequestingTrustComparisonItSpendDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _trustItSpend = Fixture.Build<TrustItSpend>().CreateMany(5).ToArray();
        _trustForecast = Fixture.Build<TrustItSpendForecastYear>().CreateMany(3).ToArray();
    }

    [Fact]
    public async Task CanReturnOkWithTrustClaim()
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
            .SetupItSpend(trustSpend: _trustItSpend, trustForecast: _trustForecast)
            .Get(Paths.TrustComparisonItSpendDownload(trust.CompanyNumber!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "benchmark-it-spending-previous-year-87654321.csv",
            "benchmark-it-spending-forecast-87654321.csv"
        };

        var files = new List<(string fileName, string content)>();
        await foreach (var file in response.GetFilesFromZip())
        {
            Assert.Contains(file.fileName, expectedFileNames);

            files.Add(file);
        }

        Assert.Equal(2, files.Count);

        var previousYearFile = files.Single(f => f.fileName == "benchmark-it-spending-previous-year-87654321.csv");
        var previousYearContent = previousYearFile.content;
        Assert.NotNull(previousYearContent);
        var previousYearLines = previousYearContent.Split(Environment.NewLine);
        Assert.Equal(
            "CompanyNumber,TrustName,Connectivity,OnsiteServers,ItLearningResources,AdministrationSoftwareAndSystems,LaptopsDesktopsAndTablets,OtherHardware,ItSupport",
            previousYearLines.First());
        Assert.Equal(_trustItSpend.Length, previousYearLines.Length - 1);

        var forecastFile = files.Single(f => f.fileName == "benchmark-it-spending-forecast-87654321.csv");
        var forecastContent = forecastFile.content;
        Assert.NotNull(forecastContent);
        var forecastLines = forecastContent.Split(Environment.NewLine);
        Assert.Equal(
            "Year,Connectivity,OnsiteServers,ItLearningResources,AdministrationSoftwareAndSystems,LaptopsDesktopsAndTablets,OtherHardware,ItSupport",
            forecastLines.First());
        Assert.Equal(_trustForecast.Length, forecastLines.Length - 1);
    }

    [Fact]
    public async Task CanReturnOkWithNoTrustClaim()
    {
        var trust = Fixture.Build<Trust>()
            .With(x => x.CompanyNumber, "12345678")
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
            .SetupItSpend(trustSpend: _trustItSpend, trustForecast: _trustForecast)
            .Get(Paths.TrustComparisonItSpendDownload(trust.CompanyNumber!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "benchmark-it-spending-previous-year-12345678.csv",
        };

        var files = new List<(string fileName, string content)>();
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            files.Add(tuple);
        }

        Assert.Single(files);

        var file = files.Single(f => f.fileName == "benchmark-it-spending-previous-year-12345678.csv");
        var content = file.content;
        Assert.NotNull(content);
        var csvLines = content.Split(Environment.NewLine);
        Assert.Equal(
            "CompanyNumber,TrustName,Connectivity,OnsiteServers,ItLearningResources,AdministrationSoftwareAndSystems,LaptopsDesktopsAndTablets,OtherHardware,ItSupport",
            csvLines.First());
        Assert.Equal(_trustItSpend.Length, csvLines.Length - 1);
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