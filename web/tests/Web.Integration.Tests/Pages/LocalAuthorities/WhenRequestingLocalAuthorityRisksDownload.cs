using System.Net;
using AutoFixture;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenRequestingLocalAuthorityRisksDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly LocalAuthorityRiskIndicators[] _risks;

    public WhenRequestingLocalAuthorityRisksDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _risks = Fixture.Build<LocalAuthorityRiskIndicators>().CreateMany(3).ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var authority = Fixture.Build<LocalAuthority>()
            .With(x => x.Code, "123")
            .Create();

        var risksResults = new PagedResults<LocalAuthorityRiskIndicators>
        {
            Results = _risks,
        };

        Assert.NotNull(authority.Code);
        var response = await _client
                .SetupLocalAuthorityEndpoints(authority, risksResults: risksResults)
                .Get(Paths.LocalAuthorityRisksDownload(authority.Code));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "123-schools-risk-overview.csv"
        };
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(
                "SchoolName,Urn,OverallRiskScore,FinancialRiskScore,PupilAndWorkforceRiskScore,EducationalPerformanceRiskScore",
                csvLines.First());
            Assert.Equal(_risks.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = "123";
        var response = await _client
            .SetupEstablishmentWithException()
            .Get(Paths.LocalAuthorityRisksDownload(code));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
