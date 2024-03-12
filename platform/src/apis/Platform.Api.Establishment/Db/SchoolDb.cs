using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Platform.Domain.DataObjects;
using Platform.Domain.Responses;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Establishment.Db;

public interface ISchoolDb
{
    Task<School?> Get(string urn);
    Task<PagedResults<School>> Query(IQueryCollection query);
}

[ExcludeFromCodeCoverage]
public record SchoolDbOptions : CosmosDatabaseOptions;

[ExcludeFromCodeCoverage]
public class SchoolDb : CosmosDatabase, ISchoolDb
{
    private readonly ICollectionService _collectionService;

    public SchoolDb(IOptions<SchoolDbOptions> options, ICollectionService collectionService) : base(options.Value)
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

        var collection = await _collectionService.LatestCollection(DataGroups.Edubase);

        var school = await ItemEnumerableAsync<EdubaseDataObject>(
                collection.Name,
                q => q.Where(x => x.Urn == parsedUrn))
            .FirstOrDefaultAsync();

        return school == null ? null : School.Create(school);
    }

    public async Task<PagedResults<School>> Query(IQueryCollection query)
    {
        var collection = await _collectionService.LatestCollection(DataGroups.Edubase);
        var pageParams = query.GetPagingValues();

        var establishments =
            await PagedItemEnumerableAsync<EdubaseDataObject>(collection.Name, pageParams.Page, pageParams.PageSize)
                .ToArrayAsync();
        var establishmentsTotalCount = await ItemCountAsync<EdubaseDataObject>(collection.Name);

        var schools = establishments.Select(School.Create);
        return PagedResults<School>.Create(schools, pageParams.Page, pageParams.PageSize, establishmentsTotalCount);
    }
}