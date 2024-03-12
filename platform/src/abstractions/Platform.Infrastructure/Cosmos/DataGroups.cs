using System.Diagnostics.CodeAnalysis;

namespace Platform.Infrastructure.Cosmos;

[ExcludeFromCodeCoverage]
public static class DataGroups
{
    public const string Edubase = "Edubase";
    public const string Maintained = "Maintained";//Maintained figures
    public const string Academies = "Academies";//Academy's own figures        
    public const string MatAllocated = "MAT-Allocs";//Academy + its MAT's allocated figures        
    public const string MatCentral = "MAT-Central";//MAT only figures
    public const string MatTotals = "MAT-Totals";//Total of Academy only figures of the MAT
    public const string MatOverview = "MAT-Overview";//MAT + all of its Academies' figures
    public const string Unidentified = "Unidentified";
}