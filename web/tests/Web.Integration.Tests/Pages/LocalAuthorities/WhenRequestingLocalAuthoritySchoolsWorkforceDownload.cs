using System.Net;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenRequestingLocalAuthoritySchoolsWorkforceDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly LocalAuthoritySchoolWorkforce[] _localAuthoritySchoolWorkforces;

    public WhenRequestingLocalAuthoritySchoolsWorkforceDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _localAuthoritySchoolWorkforces = Fixture.Build<LocalAuthoritySchoolWorkforce>().CreateMany(5).ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var localAuthority = Fixture.Build<LocalAuthority>()
            .With(x => x.Code, "123")
            .Create();

        var response = await _client
            .SetupLocalAuthoritySchools(null, _localAuthoritySchoolWorkforces)
            .Get(Paths.LocalAuthoritySchoolsWorkforceDownload(localAuthority.Code!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            $"la-schools-workforce-{localAuthority.Code}.csv"
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
        Assert.Equal("Urn,SchoolName,TotalPupils,PupilTeacherRatio,EhcPlan,SenSupport", lines.First());
        Assert.Equal(_localAuthoritySchoolWorkforces.Length, lines.Length - 1);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        var localAuthority = Fixture.Build<LocalAuthority>()
            .With(x => x.Code, "123")
            .Create();

        var response = await _client
            .SetupLocalAuthoritiesWithException()
            .Get(Paths.LocalAuthoritySchoolsWorkforceDownload(localAuthority.Code!));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}