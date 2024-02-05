using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Domain.DataObjects;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Insight.Db;

public interface IAcademyDb
{
    Task<Finances> Get(string urn);
}

[ExcludeFromCodeCoverage]
public class AcademyDbOptions : CosmosDatabaseOptions
{
}

[ExcludeFromCodeCoverage]
public class AcademyDb : CosmosDatabase, IAcademyDb
{
    private readonly ICollectionService _collectionService;

    public AcademyDb(IOptions<AcademyDbOptions> options, ICollectionService collectionService) : base(options.Value)
    {
        _collectionService = collectionService;
    }

    public async Task<Finances> Get(string urn)
    {
        var collection = await _collectionService.GetLatestCollection(DataGroups.Academies);

        var finances = await GetItemEnumerableAsync<SchoolTrustFinancialDataObject>(
                collection.Name,
                q => q.Where(x => x.URN == long.Parse(urn)))
            .FirstOrDefaultAsync();

        return finances == null
            ? null
            : Finances.Create(finances, collection.Term);
    }
}