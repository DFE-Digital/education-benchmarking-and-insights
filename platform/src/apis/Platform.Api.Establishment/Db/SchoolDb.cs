using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Establishment.Db;

public interface ISchoolDb
{
    Task<SchoolResponseModel?> Get(string urn);
    Task<PagedResponseModel<SchoolResponseModel>> Query(IQueryCollection query);
}

[ExcludeFromCodeCoverage]
public record SchoolDbOptions : CosmosDatabaseOptions
{
    [Required] public string? EstablishmentCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class SchoolDb : CosmosDatabase, ISchoolDb
{
    private readonly string _collectionName;

    public SchoolDb(IOptions<SchoolDbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.EstablishmentCollectionName);
        _collectionName = options.Value.EstablishmentCollectionName;
    }

    public async Task<SchoolResponseModel?> Get(string urn)
    {
        var canParse = long.TryParse(urn, out var parsedUrn);
        if (!canParse)
        {
            return null;
        }

        var school = await ItemEnumerableAsync<EdubaseDataObject>(
                _collectionName,
                q => q.Where(x => x.Urn == parsedUrn))
            .FirstOrDefaultAsync();

        return school == null ? null : SchoolResponseModel.Create(school);
    }

    public async Task<PagedResponseModel<SchoolResponseModel>> Query(IQueryCollection query)
    {
        var pageParams = query.GetPagingValues();

        var establishments =
            await PagedItemEnumerableAsync<EdubaseDataObject>(_collectionName, pageParams.Page, pageParams.PageSize)
                .ToArrayAsync();
        var establishmentsTotalCount = await ItemCountAsync<EdubaseDataObject>(_collectionName);

        var schools = establishments.Select(SchoolResponseModel.Create);
        return PagedResponseModel<SchoolResponseModel>.Create(schools, pageParams.Page, pageParams.PageSize,
            establishmentsTotalCount);
    }
}