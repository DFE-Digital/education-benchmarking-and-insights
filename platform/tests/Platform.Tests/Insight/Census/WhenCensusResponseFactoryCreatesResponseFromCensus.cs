using AutoFixture;
using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census;

public class WhenCensusResponseFactoryCreatesResponseFromCensus
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
    [InlineData(CensusCategories.WorkforceFte, CensusDimensions.HeadcountPerFte, "0.90")]
    [InlineData(CensusCategories.WorkforceFte, CensusDimensions.PercentWorkforce, "100.00")]
    [InlineData(CensusCategories.WorkforceFte, CensusDimensions.PupilsPerStaffRole, "11.11")]
    [InlineData(CensusCategories.WorkforceFte, CensusDimensions.Total, "90.00")]
    [InlineData(null, CensusDimensions.HeadcountPerFte, "0.90")]
    [InlineData(null, CensusDimensions.PercentWorkforce, "100.00")]
    [InlineData(null, CensusDimensions.PupilsPerStaffRole, "11.11")]
    [InlineData(null, CensusDimensions.Total, "90.00")]
    [InlineData("Category", "Dimension", null)]
    public void ShouldBuildResponseModelWithWorkforce(string? category, string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, category, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.Workforce:N2}");
    }

    [Theory]
    [InlineData(CensusCategories.TeachersFte, CensusDimensions.HeadcountPerFte, "0.94")]
    [InlineData(CensusCategories.TeachersFte, CensusDimensions.PercentWorkforce, "88.89")]
    [InlineData(CensusCategories.TeachersFte, CensusDimensions.PupilsPerStaffRole, "12.50")]
    [InlineData(CensusCategories.TeachersFte, CensusDimensions.Total, "80.00")]
    [InlineData(null, CensusDimensions.HeadcountPerFte, "0.94")]
    [InlineData(null, CensusDimensions.PercentWorkforce, "88.89")]
    [InlineData(null, CensusDimensions.PupilsPerStaffRole, "12.50")]
    [InlineData(null, CensusDimensions.Total, "80.00")]
    [InlineData("Category", "Dimension", null)]
    public void ShouldBuildResponseModelWithTeachers(string? category, string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.TeachersFTE, TeachersFTE)
            .With(c => c.TeachersHeadcount, TeachersHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, category, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.Teachers:N2}");
    }

    [Theory]
    [InlineData(CensusCategories.SeniorLeadershipFte, CensusDimensions.HeadcountPerFte, "0.93")]
    [InlineData(CensusCategories.SeniorLeadershipFte, CensusDimensions.PercentWorkforce, "77.78")]
    [InlineData(CensusCategories.SeniorLeadershipFte, CensusDimensions.PupilsPerStaffRole, "14.29")]
    [InlineData(CensusCategories.SeniorLeadershipFte, CensusDimensions.Total, "70.00")]
    [InlineData(null, CensusDimensions.HeadcountPerFte, "0.93")]
    [InlineData(null, CensusDimensions.PercentWorkforce, "77.78")]
    [InlineData(null, CensusDimensions.PupilsPerStaffRole, "14.29")]
    [InlineData(null, CensusDimensions.Total, "70.00")]
    [InlineData("Category", "Dimension", null)]
    public void ShouldBuildResponseModelWithSeniorLeadership(string? category, string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.SeniorLeadershipFTE, SeniorLeadershipFTE)
            .With(c => c.SeniorLeadershipHeadcount, SeniorLeadershipHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, category, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.SeniorLeadership:N2}");
    }

    [Theory]
    [InlineData(CensusCategories.TeachingAssistantsFte, CensusDimensions.HeadcountPerFte, "0.92")]
    [InlineData(CensusCategories.TeachingAssistantsFte, CensusDimensions.PercentWorkforce, "66.67")]
    [InlineData(CensusCategories.TeachingAssistantsFte, CensusDimensions.PupilsPerStaffRole, "16.67")]
    [InlineData(CensusCategories.TeachingAssistantsFte, CensusDimensions.Total, "60.00")]
    [InlineData(null, CensusDimensions.HeadcountPerFte, "0.92")]
    [InlineData(null, CensusDimensions.PercentWorkforce, "66.67")]
    [InlineData(null, CensusDimensions.PupilsPerStaffRole, "16.67")]
    [InlineData(null, CensusDimensions.Total, "60.00")]
    [InlineData("Category", "Dimension", null)]
    public void ShouldBuildResponseModelWithTeachingAssistant(string? category, string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.TeachingAssistantFTE, TeachingAssistantFTE)
            .With(c => c.TeachingAssistantHeadcount, TeachingAssistantHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, category, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.TeachingAssistant:N2}");
    }

    [Theory]
    [InlineData(CensusCategories.NonClassroomSupportStaffFte, CensusDimensions.HeadcountPerFte, "0.80")]
    [InlineData(CensusCategories.NonClassroomSupportStaffFte, CensusDimensions.PercentWorkforce, "44.44")]
    [InlineData(CensusCategories.NonClassroomSupportStaffFte, CensusDimensions.PupilsPerStaffRole, "25.00")]
    [InlineData(CensusCategories.NonClassroomSupportStaffFte, CensusDimensions.Total, "40.00")]
    [InlineData(null, CensusDimensions.HeadcountPerFte, "0.80")]
    [InlineData(null, CensusDimensions.PercentWorkforce, "44.44")]
    [InlineData(null, CensusDimensions.PupilsPerStaffRole, "25.00")]
    [InlineData(null, CensusDimensions.Total, "40.00")]
    [InlineData("Category", "Dimension", null)]
    public void ShouldBuildResponseModelWithNonClassroomSupportStaff(string? category, string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.NonClassroomSupportStaffFTE, NonClassroomSupportStaffFTE)
            .With(c => c.NonClassroomSupportStaffHeadcount, NonClassroomSupportStaffHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, category, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.NonClassroomSupportStaff:N2}");
    }

    [Theory]
    [InlineData(CensusCategories.AuxiliaryStaffFte, CensusDimensions.HeadcountPerFte, "0.67")]
    [InlineData(CensusCategories.AuxiliaryStaffFte, CensusDimensions.PercentWorkforce, "22.22")]
    [InlineData(CensusCategories.AuxiliaryStaffFte, CensusDimensions.PupilsPerStaffRole, "50.00")]
    [InlineData(CensusCategories.AuxiliaryStaffFte, CensusDimensions.Total, "20.00")]
    [InlineData(null, CensusDimensions.HeadcountPerFte, "0.67")]
    [InlineData(null, CensusDimensions.PercentWorkforce, "22.22")]
    [InlineData(null, CensusDimensions.PupilsPerStaffRole, "50.00")]
    [InlineData(null, CensusDimensions.Total, "20.00")]
    [InlineData("Category", "Dimension", null)]
    public void ShouldBuildResponseModelWithAuxiliaryStaff(string? category, string dimension, string? expected)
    {
        // arrange
        var model = _fixture.Build<CensusModel>()
            .With(c => c.TotalPupils, TotalPupils)
            .With(c => c.WorkforceFTE, WorkforceFTE)
            .With(c => c.WorkforceHeadcount, WorkforceHeadcount)
            .With(c => c.AuxiliaryStaffFTE, AuxiliaryStaffFTE)
            .With(c => c.AuxiliaryStaffHeadcount, AuxiliaryStaffHeadcount)
            .Create();

        // act
        var response = CensusResponseFactory.Create(model, category, dimension);

        // assert
        Assert.Equal(expected ?? string.Empty, $"{response.AuxiliaryStaff:N2}");
    }
}