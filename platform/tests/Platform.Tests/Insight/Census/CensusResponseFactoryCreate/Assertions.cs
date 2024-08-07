using Platform.Api.Insight.Census;
using Xunit;
namespace Platform.Tests.Insight.Census.CensusResponseFactoryCreate;

public static class Assertions
{
    internal static void AssertWorkforce(CensusBaseResponse expected, CensusBaseResponse response)
    {
        AssertEqual(nameof(CensusBaseResponse.Workforce), expected.Workforce, response.Workforce);
    }

    internal static void AssertTeachers(CensusBaseResponse expected, CensusBaseResponse response)
    {
        AssertEqual(nameof(CensusBaseResponse.Teachers), expected.Teachers, response.Teachers);
    }

    internal static void AssertSeniorLeadership(CensusBaseResponse expected, CensusBaseResponse response)
    {
        AssertEqual(nameof(CensusBaseResponse.SeniorLeadership), expected.SeniorLeadership, response.SeniorLeadership);
    }

    internal static void AssertTeachingAssistant(CensusBaseResponse expected, CensusBaseResponse response)
    {
        AssertEqual(nameof(CensusBaseResponse.TeachingAssistant), expected.TeachingAssistant, response.TeachingAssistant);
    }

    internal static void AssertNonClassroomSupportStaff(CensusBaseResponse expected, CensusBaseResponse response)
    {
        AssertEqual(nameof(CensusBaseResponse.NonClassroomSupportStaff), expected.NonClassroomSupportStaff, response.NonClassroomSupportStaff);
    }

    internal static void AssertAuxiliaryStaff(CensusBaseResponse expected, CensusBaseResponse response)
    {
        AssertEqual(nameof(CensusBaseResponse.AuxiliaryStaff), expected.AuxiliaryStaff, response.AuxiliaryStaff);
    }

    private static void AssertEqual(string field, decimal? expected, decimal? actual) =>
        Assert.True(
            Math.Abs(expected.GetValueOrDefault() - actual.GetValueOrDefault()) < 0.02m,
            $"Expected `{expected}` for {field} but got `{actual}`");
}