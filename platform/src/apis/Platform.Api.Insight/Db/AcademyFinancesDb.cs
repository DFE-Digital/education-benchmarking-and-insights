using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

[ExcludeFromCodeCoverage]
public record AcademyFinancesDbOptions : SchoolFinancesDbOptions
{
    private const string MatAllocatedCollectionPrefix = "MAT-Allocations"; //Academy + its MAT's allocated figures
    private const string AcademiesCollectionPrefix = "Academies"; //Academy's own figures 
    private const string MatCentralCollectionPrefix = "MAT-Central"; //MAT only figures
    private const string MatTotalsCollectionPrefix = "MAT-Totals"; //Total of Academy only figures of the MAT
    private const string MatOverviewCollectionPrefix = "MAT-Overview"; //MAT + all of its Academies' figures

    public override int LatestYear => AarLatestYear;
    public int AarLatestYear { get; set; }
    public override string CollectionPrefix => MatAllocatedCollectionPrefix;
}

[ExcludeFromCodeCoverage]
public class AcademyFinancesDb : SchoolFinancesDb<Academy>
{
    public AcademyFinancesDb(IOptions<AcademyFinancesDbOptions> options, ICosmosClientFactory factory)
        : base(options, factory)
    {
    }
}