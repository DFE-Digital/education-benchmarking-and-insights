using System.Net;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenRequestingEducationHealthCarePlansDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly EducationHealthCarePlans[] _plans;

    public WhenRequestingEducationHealthCarePlansDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _plans = Fixture.Build<EducationHealthCarePlans>().CreateMany().ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var authority = Fixture.Build<Web.App.Domain.LocalAuthorities.LocalAuthority>()
            .With(x => x.Code, "123")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        Assert.NotNull(authority.Code);
        var response = await _client
                .SetupLocalAuthorityEndpoints(authority, _plans)
                .SetupLocalAuthoritiesComparators(authority.Code, ["123", "124"])
                .Get(Paths.LocalAuthorityEducationHealthCarePlansDownload(authority.Code));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "benchmark-education-health-care-plans-123.csv"
        };
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(
                "Code,Name,TotalPupils,Total,Mainstream,Resourced,Special,Independent,Hospital,Post16,Other",
                csvLines.First());
            Assert.Equal(_plans.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = "123";
        var response = await _client
            .SetupEstablishmentWithException()
            .Get(Paths.LocalAuthorityEducationHealthCarePlansDownload(code));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}