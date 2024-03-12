using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain.DataObjects;
using Platform.Domain.Responses;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface IMaintainSchoolDb
{
    Task<Finances?> Get(string urn);
}

[ExcludeFromCodeCoverage]
public record MaintainSchoolDbOptions : CosmosDatabaseOptions;

[ExcludeFromCodeCoverage]
public class MaintainSchoolDb : CosmosDatabase, IMaintainSchoolDb
{
    private readonly ICollectionService _collectionService;

    public MaintainSchoolDb(IOptions<MaintainSchoolDbOptions> options, ICollectionService collectionService) : base(options.Value)
    {
        _collectionService = collectionService;
    }

    public async Task<Finances?> Get(string urn)
    {
        var collection = await _collectionService.LatestCollection(DataGroups.Maintained);

        var finances = await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(
                collection.Name,
                q => q.Where(x => x.Urn == long.Parse(urn)))
            .FirstOrDefaultAsync();

        return finances == null
            ? null
            : Finances.Create(finances, collection.Term);
    }
}