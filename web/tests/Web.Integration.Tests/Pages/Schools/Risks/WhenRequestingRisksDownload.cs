using System.Net;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Risks;

public class WhenRequestingRisksDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly LocalAuthorityRiskIndicatorsHistoryRows _risksHistoryRows;

    public WhenRequestingRisksDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _risksHistoryRows = Fixture.Build<LocalAuthorityRiskIndicatorsHistoryRows>().Create();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        const string code = "123";
        const string urn = "123456";

        var authority = Fixture.Build<Web.App.Domain.LocalAuthorities.LocalAuthority>()
            .With(x => x.Code, code)
            .Create();

        var school = Fixture.Build<School>()
            .With(x => x.URN, urn)
            .With(x => x.SchoolName, "foo")
            .With(x => x.LACode, code)
            .Create();

        Assert.NotNull(authority.Code);
        var response = await _client
                .SetupLocalAuthorityEndpoints(authority)
                .SetupSchool(school, riskIndicatorsHistory: _risksHistoryRows)
                .Get(Paths.LocalAuthoritySchoolRisksHistoryDownload(authority.Code, school.URN));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "foo-trend-in-risk-scores.csv"
        };
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(
                "Year,Urn,SchoolName,OverallGrade,Overall,OverallMax,OverallGradeColour,Financial,FinancialMax,SchoolAndPupil,SchoolAndPupilMax,EducationalPerformance,EducationalPerformanceMax",
                csvLines.First());
            Assert.Equal(_risksHistoryRows.Rows.Count(), csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnNotFound()
    {
        const string code = "123";
        const string otherCode = "321";
        const string urn = "123456";

        var authority = Fixture.Build<Web.App.Domain.LocalAuthorities.LocalAuthority>()
            .With(x => x.Code, code)
            .Create();

        var school = Fixture.Build<School>()
            .With(x => x.URN, urn)
            .With(x => x.LACode, otherCode)
            .Create();

        var response = await _client
            .SetupLocalAuthorityEndpoints(authority)
            .SetupSchool(school)
            .Get(Paths.LocalAuthoritySchoolRisksHistoryDownload(code, urn));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = "123";
        const string urn = "123456";

        var response = await _client
            .SetupSchoolWithException()
            .Get(Paths.LocalAuthoritySchoolRisksHistoryDownload(code, urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
