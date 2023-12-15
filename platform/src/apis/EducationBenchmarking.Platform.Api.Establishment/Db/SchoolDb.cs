using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Db;

public interface ISchoolDb
{
    Task<School?> GetSchool(string urn);
}

public class SchoolDbOptions
{
    public string ConnectionString { get; set; }
    public string DatabaseId { get; set; }
}

public class SchoolDb : CosmosDatabase, ISchoolDb
{
    private readonly ICollectionService _collectionService;

    public SchoolDb(IOptions<SchoolDbOptions> options, ICollectionService collectionService) : base(
        options.Value.ConnectionString, options.Value.DatabaseId)
    {
        _collectionService = collectionService;
    }

    public async Task<School?> GetSchool(string urn)
    {
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);
        
        var school = await GetItemEnumerableAsync<Edubase>(
                collection.Name,
                q => q.Where(x => x.URN == long.Parse(urn)))
            .FirstOrDefaultAsync();

        return school == null
            ? null
            : new School
            {
                Urn = school.URN.ToString(),
                Kind = school.TypeOfEstablishment,
                FinanceType = school.FinanceType,
                Name = school.EstablishmentName
            };
    }
}