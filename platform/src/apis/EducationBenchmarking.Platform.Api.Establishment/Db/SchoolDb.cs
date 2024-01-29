using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Domain.DataObjects;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Establishment.Db;

public interface ISchoolDb
{
    Task<School?> Get(string urn);
    Task<PagedResults<School>> Query(IQueryCollection query);
}

[ExcludeFromCodeCoverage]
public class SchoolDbOptions
{
    [Required] public string ConnectionString { get; set; }
    [Required] public string DatabaseId { get; set; }
    public bool IsDirect { get; set; } = true;
}

[ExcludeFromCodeCoverage]
public class SchoolDb : CosmosDatabase, ISchoolDb
{
    private readonly ICollectionService _collectionService;

    public SchoolDb(IOptions<SchoolDbOptions> options, ICollectionService collectionService) : base(
        options.Value.ConnectionString, options.Value.DatabaseId, options.Value.IsDirect)
    {
        _collectionService = collectionService;
    }

    public async Task<School?> Get(string urn)
    {
        var canParse = long.TryParse(urn, out var parsedUrn);
        if (!canParse)
        {
            return null;
        }
        
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);
        
        var school = await GetItemEnumerableAsync<EdubaseDataObject>(
                collection.Name,
                q => q.Where(x => x.URN == parsedUrn))
            .FirstOrDefaultAsync();

        return school == null ? null : School.Create(school);
    }

    public async Task<PagedResults<School>> Query(IQueryCollection query)
    {
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);
        var pageParams = query.GetPagingValues();
        
        var establishments =
            await GetPagedItemEnumerableAsync<EdubaseDataObject>(collection.Name, pageParams.Page, pageParams.PageSize)
                .ToArrayAsync();
        var establishmentsTotalCount = await GetItemCountAsync<EdubaseDataObject>(collection.Name);

        var schools = establishments.Select(School.Create);
        return PagedResults<School>.Create(schools, pageParams.Page, pageParams.PageSize, establishmentsTotalCount);
    }
}