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
public record AcademyDbOptions : CosmosDatabaseOptions;

[ExcludeFromCodeCoverage]
public class AcademyDb : CosmosDatabase, IAcademyDb
{
    private readonly ICollectionService _collectionService;

    public AcademyDb(IOptions<AcademyDbOptions> options, ICollectionService collectionService) : base(options.Value)
    {
        _collectionService = collectionService;
    }

    public async Task<FinancesResponseModel?> Get(string urn)
    {
        var collection = await _collectionService.LatestCollection(DataGroups.MatAllocated);

        var finances = await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(
                collection.Name,
                q => q.Where(x => x.Urn == long.Parse(urn)))
            .FirstOrDefaultAsync();

        return finances == null
            ? null
            : FinancesResponseModel.Create(finances, collection.Term);
    }
}