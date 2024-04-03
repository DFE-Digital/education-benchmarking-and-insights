using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

[ExcludeFromCodeCoverage]
public record FinancialReturnOptions : CosmosDatabaseOptions
{
    private const int NumberPreviousYears = 4; //Excludes current years
    private const string MaintainedCollectionPrefix = "Maintained";
    private const string MatAllocatedCollectionPrefix = "MAT-Allocations"; //Academy + its MAT's allocated figures
    private const string AcademiesCollectionPrefix = "Academies"; //Academy's own figures 
    private const string MatCentralCollectionPrefix = "MAT-Central"; //MAT only figures
    private const string MatTotalsCollectionPrefix = "MAT-Totals"; //Total of Academy only figures of the MAT
    private const string MatOverviewCollectionPrefix = "MAT-Overview"; //MAT + all of its Academies' figures

    private IEnumerable<int> AarYears => AarLatestYear != null ? Enumerable.Range(AarLatestYear.Value - NumberPreviousYears, NumberPreviousYears + 1) : Array.Empty<int>();
    private IEnumerable<int> CfrYears => CfrLatestYear != null ? Enumerable.Range(CfrLatestYear.Value - NumberPreviousYears, NumberPreviousYears + 1) : Array.Empty<int>();

    public int? CfrLatestYear { get; set; }
    public int? AarLatestYear { get; set; }

    public string LatestMaintained => BuildCollectionName(MaintainedCollectionPrefix, CfrLatestYear);
    public string LatestMatAllocated => BuildCollectionName(MatAllocatedCollectionPrefix, AarLatestYear);

    public IEnumerable<string> HistoryMaintained => CfrYears.Select(year => BuildCollectionName(MaintainedCollectionPrefix, year));
    public IEnumerable<string> HistoryMatAllocated => AarYears.Select(year => BuildCollectionName(MatAllocatedCollectionPrefix, year));

    private static string BuildCollectionName(string prefix, int? year)
    {
        return $"{prefix}-{year - 1}-{year}";
    }
}