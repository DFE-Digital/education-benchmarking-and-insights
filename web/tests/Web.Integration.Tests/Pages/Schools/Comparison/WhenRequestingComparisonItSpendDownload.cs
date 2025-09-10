using System.Net;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Comparison;

public class WhenRequestingComparisonItSpendDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly SchoolItSpend[] _schoolItSpend;

    public WhenRequestingComparisonItSpendDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _schoolItSpend = Fixture.Build<SchoolItSpend>().CreateMany().ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .With(x => x.FinanceType, EstablishmentTypes.Maintained)
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        var response = await _client
            .SetupEstablishment(school)
            .SetupComparatorSet(school, comparatorSet)
            .SetupItSpend(_schoolItSpend)
            .Get(Paths.SchoolComparisonItSpendDownload(school.URN!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "benchmark-it-spending-123456.csv"
        };
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(
                "URN,SchoolName,SchoolType,LAName,PeriodCoveredByReturn,TotalPupils,Connectivity,OnsiteServers,ItLearningResources,AdministrationSoftwareAndSystems,LaptopsDesktopsAndTablets,OtherHardware,ItSupport",
                csvLines.First());
            Assert.Equal(_schoolItSpend.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "123456";
        var response = await _client
            .SetupEstablishmentWithException()
            .Get(Paths.SchoolComparisonItSpendDownload(urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}