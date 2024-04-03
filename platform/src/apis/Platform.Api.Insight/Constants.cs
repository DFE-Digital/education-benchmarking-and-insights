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