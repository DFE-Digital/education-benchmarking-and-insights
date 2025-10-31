using System.Net;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenRequestingLocalAuthoritySchoolsFinanceDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly LocalAuthoritySchoolFinancial[] _localAuthoritySchoolFinancials;

    public WhenRequestingLocalAuthoritySchoolsFinanceDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _localAuthoritySchoolFinancials = Fixture.Build<LocalAuthoritySchoolFinancial>().CreateMany(5).ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var localAuthority = Fixture.Build<LocalAuthority>()
            .With(x => x.Code, "123")
            .Create();

        var response = await _client
            .SetupLocalAuthoritySchools(_localAuthoritySchoolFinancials)
            .Get(Paths.LocalAuthoritySchoolsFinanceDownload(localAuthority.Code!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            $"la-schools-finance-{localAuthority.Code}.csv"
        };

        var files = new List<(string fileName, string content)>();
        await foreach (var file in response.GetFilesFromZip())
        {
            Assert.Contains(file.fileName, expectedFileNames);
            files.Add(file);
        }

        Assert.Single(files);

        var content = files.Single().content;
        Assert.NotNull(content);
        var lines = content.Split(Environment.NewLine);
        Assert.Equal(
            "Urn,SchoolName,PeriodCoveredByReturn,TotalPupils,TotalExpenditure,TotalTeachingSupportStaffCosts,RevenueReserve",
            lines.First());
        Assert.Equal(_localAuthoritySchoolFinancials.Length, lines.Length - 1);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        var localAuthority = Fixture.Build<LocalAuthority>()
            .With(x => x.Code, "123")
            .Create();

        var response = await _client
            .SetupLocalAuthoritiesWithException()
            .Get(Paths.LocalAuthoritySchoolsFinanceDownload(localAuthority.Code!));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}