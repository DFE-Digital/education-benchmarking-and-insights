using System.Net;
using System.Text;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingCensusDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly Census[] _censuses;
    private readonly SchoolBenchmarkingWebAppClient _client;

    public WhenRequestingCensusDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _censuses = Fixture.Build<Census>().CreateMany().ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, ["pupil"])
            .Create();

        var response = await _client
            .SetupEstablishment(school)
            .SetupComparatorSet(school, comparatorSet)
            .SetupCensus(_censuses)
            .Get(Paths.ApiCensusDownload(school.URN!, "school"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var bytes = await response.Content.ReadAsByteArrayAsync();
        var csvLines = Encoding.UTF8.GetString(bytes).Split(Environment.NewLine);
        Assert.Equal(
            "SchoolName,SchoolType,LAName,URN,TotalPupils,Workforce,WorkforceHeadcount,Teachers,SeniorLeadership,TeachingAssistant,NonClassroomSupportStaff,AuxiliaryStaff,PercentTeacherWithQualifiedStatus",
            csvLines.First());
        Assert.Equal(_censuses.Length, csvLines.Length - 1);
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "12345";
        var response = await _client
            .SetupComparatorSetApiWithException()
            .SetupCensusWithException()
            .Get(Paths.ApiCensusDownload(urn, "school"));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}