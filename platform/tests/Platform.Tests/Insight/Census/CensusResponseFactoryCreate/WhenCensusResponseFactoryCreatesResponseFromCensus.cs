using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census.CensusResponseFactoryCreate;

public class WhenCensusResponseFactoryCreatesResponseFromCensus
{
    private readonly CensusModel _model;

    public WhenCensusResponseFactoryCreatesResponseFromCensus()
    {
        _model = TestDataReader.ReadTestDataFromFile<CensusModel>("CensusTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(WorkforceTestData<CensusResponse>))]
    public void ShouldBuildResponseModelWithWorkforce(string? category, string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, category, dimension);

        Assertions.AssertWorkforce(expected, response);
    }

    [Theory]
    [ClassData(typeof(TeachersTestData<CensusResponse>))]
    public void ShouldBuildResponseModelWithTeachers(string? category, string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, category, dimension);

        Assertions.AssertTeachers(expected, response);
    }

    [Theory]
    [ClassData(typeof(SeniorLeadershipTestData<CensusResponse>))]
    public void ShouldBuildResponseModelWithSeniorLeadership(string? category, string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, category, dimension);

        Assertions.AssertSeniorLeadership(expected, response);
    }

    [Theory]
    [ClassData(typeof(TeachingAssistantTestData<CensusResponse>))]
    public void ShouldBuildResponseModelWithTeachingAssistant(string? category, string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, category, dimension);

        Assertions.AssertTeachingAssistant(expected, response);
    }

    [Theory]
    [ClassData(typeof(NonClassroomSupportStaffTestData<CensusResponse>))]
    public void ShouldBuildResponseModelWithNonClassroomSupportStaff(string? category, string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, category, dimension);

        Assertions.AssertNonClassroomSupportStaff(expected, response);
    }

    [Theory]
    [ClassData(typeof(AuxiliaryStaffTestData<CensusResponse>))]
    public void ShouldBuildResponseModelWithAuxiliaryStaff(string? category, string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, category, dimension);

        Assertions.AssertAuxiliaryStaff(expected, response);
    }
}