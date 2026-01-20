using System.Net;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Census;

public class WhenRequestingSeniorLeadershipDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SeniorLeadershipGroup[] _group;
    private readonly SchoolBenchmarkingWebAppClient _client;

    public WhenRequestingSeniorLeadershipDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _group = Fixture.Build<SeniorLeadershipGroup>().CreateMany().ToArray();
    }

    [Fact]
    public async Task CanReturnOkForDefaultComparatorSet()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, ["pupil"])
            .Create();

        var response = await _client
            .SetupSchool(school, _group)
            .SetupComparatorSet(school, comparatorSet)
            .Get(Paths.SchoolSeniorLeadershipDownload(school.URN));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Equal("benchmark-senior-leadership-group-123456.csv", tuple.fileName);

            var csvLines = tuple.content.Split(Environment.NewLine);
            var expectedColumns = "URN,SchoolName,LAName,TotalPupils,SeniorLeadership,HeadTeacher,DeputyHeadTeacher,AssistantHeadTeacher,LeadershipNonTeacher";

            Assert.Equal(expectedColumns, csvLines.First());
            Assert.Equal(_group.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnOkForUserDefinedComparatorSet()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var comparatorSet = Fixture.Build<UserDefinedSchoolComparatorSet>()
            .Create();

        var userData = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = "Id"
            }
        };

        var response = await _client
            .SetupSchool(school, _group)
            .SetupComparatorSet(school, comparatorSet)
            .SetupUserData(userData)
            .Get(Paths.SchoolSeniorLeadershipDownload(school.URN));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Equal("benchmark-senior-leadership-group-123456.csv", tuple.fileName);

            var csvLines = tuple.content.Split(Environment.NewLine);
            var expectedColumns = "URN,SchoolName,LAName,TotalPupils,SeniorLeadership,HeadTeacher,DeputyHeadTeacher,AssistantHeadTeacher,LeadershipNonTeacher";

            Assert.Equal(expectedColumns, csvLines.First());
            Assert.Equal(_group.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "123456";
        var response = await _client
            .SetupSchoolWithException()
            .Get(Paths.SchoolSeniorLeadershipDownload(urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}