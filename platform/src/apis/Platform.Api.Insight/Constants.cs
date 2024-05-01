using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ApplicationName = "insight-api";

    public const int NumberPreviousYears = 4; //Excludes current years
    public const string MaintainedCollectionPrefix = "Maintained";
    public const string MatAllocatedCollectionPrefix = "MAT-Allocations"; //Academy + its MAT's allocated figures
    public const string AcademiesCollectionPrefix = "Academies"; //Academy's own figures 
    public const string MatCentralCollectionPrefix = "MAT-Central"; //MAT only figures
    public const string MatTotalsCollectionPrefix = "MAT-Totals"; //Total of Academy only figures of the MAT
    public const string MatOverviewCollectionPrefix = "MAT-Overview"; //MAT + all of its Academies' figures
}

public static class CensusCategory
{
    public const string WorkforceFte = "workforce-fte";
    public const string TeachersFte = "teachers-fte";
    public const string SeniorLeadershipFte = "senior-leadership-fte";
    public const string TeachingAssistantsFte = "teaching-assistants-fte";
    public const string NonClassroomSupportStaffFte = "non-classroom-support-staff-fte";
    public const string AuxiliaryStaffFte = "auxiliary-staff-fte";
    public const string WorkforceHeadcount = "workforce-headcount";
    public const string TeachersQualified = "teachers-qualified";
}