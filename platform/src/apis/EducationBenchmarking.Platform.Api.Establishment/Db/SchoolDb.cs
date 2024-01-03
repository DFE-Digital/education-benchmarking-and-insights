using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;
using EducationBenchmarking.Platform.Shared.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace EducationBenchmarking.Platform.Api.Establishment.Db;

public interface ISchoolDb
{
    Task<School?> Get(string urn);
    Task<PagedResults<School>> Query(IEnumerable<KeyValuePair<string, StringValues>> criteria);
}

[ExcludeFromCodeCoverage]
public class SchoolDbOptions
{
    [Required] public string ConnectionString { get; set; }
    [Required] public string DatabaseId { get; set; }
}

[ExcludeFromCodeCoverage]
public class SchoolDb : CosmosDatabase, ISchoolDb
{
    private readonly ICollectionService _collectionService;

    public SchoolDb(IOptions<SchoolDbOptions> options, ICollectionService collectionService) : base(
        options.Value.ConnectionString, options.Value.DatabaseId)
    {
        _collectionService = collectionService;
    }

    public async Task<School?> Get(string urn)
    {
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);

        var school = await GetItemEnumerableAsync<Edubase>(
                collection.Name,
                q => q.Where(x => x.URN == long.Parse(urn)))
            .FirstOrDefaultAsync();

        return school == null ? null : SchoolFactory.Create(school);
    }

    public async Task<PagedResults<School>> Query(IEnumerable<KeyValuePair<string, StringValues>> criteria)
    {
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);
        var pageParams = QueryParameters.GetPagingValues(criteria);


        var establishments =
            await GetPagedItemEnumerableAsync<Edubase>(collection.Name, pageParams.Page, pageParams.PageSize)
                .ToArrayAsync();
        var establishmentsTotalCount = await GetItemCountAsync<Edubase>(collection.Name);

        var schools = establishments.Select(SchoolFactory.Create);
        return PagedResults<School>.Create(schools, pageParams.Page, pageParams.PageSize, establishmentsTotalCount);
    }
}