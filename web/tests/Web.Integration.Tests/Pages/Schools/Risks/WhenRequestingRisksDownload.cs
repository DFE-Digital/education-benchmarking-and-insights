using System.Net;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Risks;

public class WhenRequestingRisksDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly LocalAuthorityRiskIndicators _indicators;
    private readonly RisksMetrics[] _metrics;

    public WhenRequestingRisksDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _indicators = Fixture.Build<LocalAuthorityRiskIndicators>().Create();
        _metrics = Fixture.Build<RisksMetrics>().CreateMany(3).ToArray();
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
            .SetupSchool(school, riskIndicators: _indicators, riskMetrics: _metrics)
            .Get(Paths.LocalAuthoritySchoolRisksDownload(authority.Code, school.URN));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var expectedFileNames = new[]
        {
            "foo-risk-score.csv",
            "foo-risk-score-metrics.csv"
        };

        var extractedFiles = new List<(string fileName, string content)>();
        await foreach (var tuple in response.GetFilesFromZip())
        {
            extractedFiles.Add(tuple);
        }

        Assert.Equal(expectedFileNames.Length, extractedFiles.Count);

        foreach (var expectedName in expectedFileNames)
        {
            var file = extractedFiles.FirstOrDefault(f => f.fileName == expectedName);
            Assert.NotNull(file.fileName);

            var csvLines = file.content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            switch (expectedName)
            {
                case "foo-risk-score.csv":
                    Assert.Equal(
                        "Urn,SchoolName,OverallGrade,Overall,OverallMax,OverallGradeColour,Financial,FinancialMax,SchoolAndPupil,SchoolAndPupilMax,EducationalPerformance,EducationalPerformanceMax",
                        csvLines.First());
                    Assert.Equal(2, csvLines.Length);
                    break;
                case "foo-risk-score-metrics.csv":
                    Assert.Equal(
                        "Urn,RiskGroup,RiskIndicator,RiskIndicatorValue,RiskIndicatorValueFormatting,RiskIndicatorFlag,RiskIndicatorContribution,RiskIndicatorContributionMax",
                        csvLines.First());
                    Assert.Equal(_metrics.Length + 1, csvLines.Length);
                    break;
            }
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
            .Get(Paths.LocalAuthoritySchoolRisksDownload(code, urn));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = "123";
        const string urn = "123456";

        var response = await _client
            .SetupSchoolWithException()
            .Get(Paths.LocalAuthoritySchoolRisksDownload(code, urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
