using AutoFixture;
using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census;

public class WhenCensusResponseFactoryCreatesResponseFromCensusHistory
{
    private readonly Fixture _fixture = new();
    private const decimal TotalPupils = 1000;
    private const decimal WorkforceFTE = 90;
    private const decimal WorkforceHeadcount = 100;
    private const decimal TeachersFTE = 80;
    private const decimal TeachersHeadcount = 85;
    private const decimal SeniorLeadershipFTE = 70;
    private const decimal SeniorLeadershipHeadcount = 75;
    private const decimal TeachingAssistantFTE = 60;
    private const decimal TeachingAssistantHeadcount = 65;
    private const decimal NonClassroomSupportStaffFTE = 40;
    private const decimal NonClassroomSupportStaffHeadcount = 50;
    private const decimal AuxiliaryStaffFTE = 20;
    private const decimal AuxiliaryStaffHeadcount = 30;

    [Theory]
    [InlineData(CensusDimensions.HeadcountPerFte, "0.90")]
    [InlineData(CensusDimensions.PercentWorkforce, "100.00")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "11.11")]
    [InlineData(CensusDimensions.Total, "90.00")]
    public void ShouldBuildResponseModelWithWorkforce(string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusHistoryModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.Workforce:N2}");
    }

    [Theory]
    [InlineData(CensusDimensions.HeadcountPerFte, "0.94")]
    [InlineData(CensusDimensions.PercentWorkforce, "88.89")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "12.50")]
    [InlineData(CensusDimensions.Total, "80.00")]
    public void ShouldBuildResponseModelWithTeachers(string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusHistoryModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.TeachersFTE, TeachersFTE)
            .With(c => c.TeachersHeadcount, TeachersHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.Teachers:N2}");
    }

    [Theory]
    [InlineData(CensusDimensions.HeadcountPerFte, "0.93")]
    [InlineData(CensusDimensions.PercentWorkforce, "77.78")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "14.29")]
    [InlineData(CensusDimensions.Total, "70.00")]
    public void ShouldBuildResponseModelWithSeniorLeadership(string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusHistoryModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.SeniorLeadershipFTE, SeniorLeadershipFTE)
            .With(c => c.SeniorLeadershipHeadcount, SeniorLeadershipHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.SeniorLeadership:N2}");
    }

    [Theory]
    [InlineData(CensusDimensions.HeadcountPerFte, "0.92")]
    [InlineData(CensusDimensions.PercentWorkforce, "66.67")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "16.67")]
    [InlineData(CensusDimensions.Total, "60.00")]
    public void ShouldBuildResponseModelWithTeachingAssistant(string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusHistoryModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.TeachingAssistantFTE, TeachingAssistantFTE)
            .With(c => c.TeachingAssistantHeadcount, TeachingAssistantHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.TeachingAssistant:N2}");
    }

    [Theory]
    [InlineData(CensusDimensions.HeadcountPerFte, "0.80")]
    [InlineData(CensusDimensions.PercentWorkforce, "44.44")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "25.00")]
    [InlineData(CensusDimensions.Total, "40.00")]
    public void ShouldBuildResponseModelWithNonClassroomSupportStaff(string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusHistoryModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.NonClassroomSupportStaffFTE, NonClassroomSupportStaffFTE)
            .With(c => c.NonClassroomSupportStaffHeadcount, NonClassroomSupportStaffHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.NonClassroomSupportStaff:N2}");
    }

    [Theory]
    [InlineData(CensusDimensions.HeadcountPerFte, "0.67")]
    [InlineData(CensusDimensions.PercentWorkforce, "22.22")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "50.00")]
    [InlineData(CensusDimensions.Total, "20.00")]
    public void ShouldBuildResponseModelWithAuxiliaryStaff(string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusHistoryModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.AuxiliaryStaffFTE, AuxiliaryStaffFTE)
            .With(c => c.AuxiliaryStaffHeadcount, AuxiliaryStaffHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.AuxiliaryStaff:N2}");
    }
}