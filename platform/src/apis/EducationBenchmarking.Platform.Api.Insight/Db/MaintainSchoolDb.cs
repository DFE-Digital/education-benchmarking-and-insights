using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.School.Db;

public interface IMaintainSchoolDb
{
    Task<Finances> Get(string urn);
}

public class MaintainSchoolDbOptions
{
    [Required] public string ConnectionString { get; set; }
    [Required] public string DatabaseId { get; set; }
}

public class MaintainSchoolDb : CosmosDatabase, IMaintainSchoolDb
{
    private readonly ICollectionService _collectionService;

    public MaintainSchoolDb(IOptions<MaintainSchoolDbOptions> options, ICollectionService collectionService)
        : base(options.Value.ConnectionString, options.Value.DatabaseId)
    {
        _collectionService = collectionService;
    }

    public async Task<Finances> Get(string urn)
    {
        var collection = await _collectionService.GetLatestCollection(DataGroups.Maintained);

        var finances = await GetItemEnumerableAsync<SchoolTrustFinancialDataObject>(
                collection.Name,
                q => q.Where(x => x.URN == long.Parse(urn)))
            .FirstOrDefaultAsync();

        return finances == null
            ? null
            : new Finances
            {
                YearEnd = collection.Term,
                URN = finances.URN.ToString(),
                SchoolName = finances.SchoolName
            };
    }
}