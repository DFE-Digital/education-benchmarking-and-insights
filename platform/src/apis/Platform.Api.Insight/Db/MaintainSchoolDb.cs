using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface IMaintainSchoolDb
{
    Task<FinancesResponseModel?> Get(string urn);
}

[ExcludeFromCodeCoverage]
public class MaintainSchoolDb : CosmosDatabase, IMaintainSchoolDb
{
    private readonly string _collectionName;
    private readonly int _latestYear;

    public MaintainSchoolDb(IOptions<FinancialReturnOptions> options) : base(options.Value)
    {
        ArgumentNullException.ThrowIfNull(options.Value.CfrLatestYear);

        _collectionName = options.Value.LatestMaintained;
        _latestYear = options.Value.CfrLatestYear.Value;
    }

    public async Task<FinancesResponseModel?> Get(string urn)
    {
        var finances = await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(
                _collectionName,
                q => q.Where(x => x.Urn == long.Parse(urn)))
            .FirstOrDefaultAsync();

        return finances == null
            ? null
            : FinancesResponseModel.Create(finances, _latestYear);
    }
}