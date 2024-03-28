using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

[ExcludeFromCodeCoverage]
public record MaintainSchoolFinancesDbOptions : SchoolFinancesDbOptions
{
    private const string MaintainedCollectionPrefix = "Maintained";

    public override int LatestYear => CfrLatestYear;
    public int CfrLatestYear { get; set; }
    public override string CollectionPrefix => MaintainedCollectionPrefix;
}

[ExcludeFromCodeCoverage]
public class MaintainSchoolFinancesDb : SchoolFinancesDb<Maintained>
{
    public MaintainSchoolFinancesDb(IOptions<MaintainSchoolFinancesDbOptions> options, ICosmosClientFactory factory)
        : base(options, factory)
    {
    }
}