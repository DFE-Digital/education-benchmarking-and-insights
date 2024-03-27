using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface IAcademyDb
{
    Task<FinancesResponseModel?> Get(string urn);
}

[ExcludeFromCodeCoverage]
public class AcademyDb : CosmosDatabase, IAcademyDb
{
    private readonly string _collectionName;
    private readonly int _latestYear;

    public AcademyDb(IOptions<FinancialReturnOptions> options) : base(options.Value)
    {
        ArgumentNullException.ThrowIfNull(options.Value.AarLatestYear);

        _collectionName = options.Value.LatestMatAllocated;
        _latestYear = options.Value.AarLatestYear.Value;
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