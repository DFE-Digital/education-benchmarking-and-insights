using System.Net;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Census;

public class WhenRequestingCensusDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly App.Domain.Census[] _censuses;
    private readonly SchoolBenchmarkingWebAppClient _client;

    public WhenRequestingCensusDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _censuses = Fixture.Build<App.Domain.Census>().CreateMany().ToArray();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task CanReturnOkForDefaultComparatorSet(bool ks4ProgressBandingEnabled)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, ["pupil"])
            .Create();

        string[] features = ks4ProgressBandingEnabled ? [] : [FeatureFlags.KS4ProgressBanding];
        var response = await _client
            .SetupDisableFeatureFlags(features)
            .SetupEstablishment(school)
            .SetupComparatorSet(school, comparatorSet)
            .SetupCensus(_censuses)
            .Get(Paths.SchoolCensusDownload(school.URN!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Equal("census-123456.csv", tuple.fileName);

            var csvLines = tuple.content.Split(Environment.NewLine);
            var expectedColumns = "SchoolName,SchoolType,LAName,URN,TotalPupils,Workforce,WorkforceHeadcount,Teachers,SeniorLeadership,TeachingAssistant,NonClassroomSupportStaff,AuxiliaryStaff,PercentTeacherWithQualifiedStatus";
            if (ks4ProgressBandingEnabled)
            {
                expectedColumns += ",ProgressBanding";
            }

            Assert.Equal(expectedColumns, csvLines.First());
            Assert.Equal(_censuses.Length, csvLines.Length - 1);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task CanReturnOkForUserDefinedComparatorSet(bool ks4ProgressBandingEnabled)
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

        string[] features = ks4ProgressBandingEnabled ? [] : [FeatureFlags.KS4ProgressBanding];
        var response = await _client
            .SetupDisableFeatureFlags(features)
            .SetupComparatorSet(school, comparatorSet)
            .SetupCensus(_censuses)
            .SetupUserData(userData)
            .Get(Paths.SchoolCensusDownload(school.URN!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Equal("census-123456.csv", tuple.fileName);

            var csvLines = tuple.content.Split(Environment.NewLine);
            var expectedColumns = "SchoolName,SchoolType,LAName,URN,TotalPupils,Workforce,WorkforceHeadcount,Teachers,SeniorLeadership,TeachingAssistant,NonClassroomSupportStaff,AuxiliaryStaff,PercentTeacherWithQualifiedStatus";
            if (ks4ProgressBandingEnabled)
            {
                expectedColumns += ",ProgressBanding";
            }

            Assert.Equal(expectedColumns, csvLines.First());
            Assert.Equal(_censuses.Length, csvLines.Length - 1);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task CanReturnOkForCustomDataComparatorSet(bool ks4ProgressBandingEnabled)
    {
        const string customDataId = nameof(customDataId);
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var userDefinedComparatorSet = Fixture.Build<UserDefinedSchoolComparatorSet>()
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, ["pupil"])
            .Create();

        var expenditure = Fixture.Build<SchoolExpenditure>().Create();

        string[] features = ks4ProgressBandingEnabled ? [] : [FeatureFlags.KS4ProgressBanding];
        var response = await _client
            .SetupDisableFeatureFlags(features)
            .SetupCustomComparatorSet(school, comparatorSet, userDefinedComparatorSet)
            .SetupCensus(_censuses)
            .SetupExpenditureForCustomData(school, school.URN!, expenditure)
            .Get(Paths.SchoolCensusDownload(school.URN!, customDataId));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Equal("census-123456.csv", tuple.fileName);

            var csvLines = tuple.content.Split(Environment.NewLine);
            var expectedColumns = "SchoolName,SchoolType,LAName,URN,TotalPupils,Workforce,WorkforceHeadcount,Teachers,SeniorLeadership,TeachingAssistant,NonClassroomSupportStaff,AuxiliaryStaff,PercentTeacherWithQualifiedStatus";
            if (ks4ProgressBandingEnabled)
            {
                expectedColumns += ",ProgressBanding";
            }

            Assert.Equal(expectedColumns, csvLines.First());
            Assert.Equal(_censuses.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "123456";
        var response = await _client
            .SetupComparatorSetApiWithException()
            .SetupCensusWithException()
            .Get(Paths.SchoolCensusDownload(urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}