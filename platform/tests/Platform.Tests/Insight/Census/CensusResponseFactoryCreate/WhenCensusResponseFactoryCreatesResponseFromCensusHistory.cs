using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census.CensusResponseFactoryCreate;

public class WhenCensusResponseFactoryCreatesResponseFromCensusHistory
{
    private readonly CensusHistoryModel _model;

    public WhenCensusResponseFactoryCreatesResponseFromCensusHistory()
    {
        _model = TestDataReader.ReadTestDataFromFile<CensusHistoryModel>("CensusTestData.json", GetType());
    }

    [Theory]
    [ClassData(typeof(WorkforceTestData<CensusHistoryResponse>))]
    public void ShouldBuildResponseModelWithWorkforce(string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, dimension);

        Assertions.AssertWorkforce(expected, response);
    }

    [Theory]
    [ClassData(typeof(TeachersTestData<CensusHistoryResponse>))]
    public void ShouldBuildResponseModelWithTeachers(string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, dimension);

        Assertions.AssertTeachers(expected, response);
    }

    [Theory]
    [ClassData(typeof(SeniorLeadershipTestData<CensusHistoryResponse>))]
    public void ShouldBuildResponseModelWithSeniorLeadership(string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, dimension);

        Assertions.AssertSeniorLeadership(expected, response);
    }

    [Theory]
    [ClassData(typeof(TeachingAssistantTestData<CensusHistoryResponse>))]
    public void ShouldBuildResponseModelWithTeachingAssistant(string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, dimension);

        Assertions.AssertTeachingAssistant(expected, response);
    }

    [Theory]
    [ClassData(typeof(NonClassroomSupportStaffTestData<CensusHistoryResponse>))]
    public void ShouldBuildResponseModelWithNonClassroomSupportStaff(string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, dimension);

        Assertions.AssertNonClassroomSupportStaff(expected, response);
    }

    [Theory]
    [ClassData(typeof(AuxiliaryStaffTestData<CensusHistoryResponse>))]
    public void ShouldBuildResponseModelWithAuxiliaryStaff(string dimension, CensusBaseResponse expected)
    {
        var response = CensusResponseFactory.Create(_model, dimension);

        Assertions.AssertAuxiliaryStaff(expected, response);
    }
}