using System.Diagnostics.CodeAnalysis;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

[ExcludeFromCodeCoverage]
public record FinancialReturnOptions : CosmosDatabaseOptions
{
    public int? CfrLatestYear { get; set; }
    public int? AarLatestYear { get; set; }

    public string LatestMaintained => BuildCollectionName("Maintained", CfrLatestYear);
    public string LatestAcademies => BuildCollectionName("Academies", AarLatestYear); //Academy's own figures        
    public string LatestMatAllocated => BuildCollectionName("MAT-Allocations", AarLatestYear); //Academy + its MAT's allocated figures        
    public string LatestMatCentral => BuildCollectionName("MAT-Central", AarLatestYear); //MAT only figures
    public string LatestMatTotals => BuildCollectionName("MAT-Totals", AarLatestYear); //Total of Academy only figures of the MAT
    public string LatestMatOverview => BuildCollectionName("MAT-Overview", AarLatestYear); //MAT + all of its Academies' figures

    private static string BuildCollectionName(string prefix, int? year)
    {
        return $"{prefix}-{year - 1}-{year}";
    }
}