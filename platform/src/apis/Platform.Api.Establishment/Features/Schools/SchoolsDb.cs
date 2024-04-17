using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Establishment.Features.Schools;

public interface ISchoolsDb
{
    Task<SchoolResponseModel?> Get(int identifier);
    Task<PagedResponseModel<SchoolResponseModel>> Query(SchoolQueryParameters parameters);
}

[ExcludeFromCodeCoverage]
public class SchoolsDb : CosmosDatabase, ISchoolsDb
{
    private readonly string _collectionName;

    public SchoolsDb(IOptions<DbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.EstablishmentCollectionName);
        _collectionName = options.Value.EstablishmentCollectionName;
    }

    public async Task<SchoolResponseModel?> Get(int identifier)
    {
        var school = await ItemEnumerableAsync<EdubaseDataObject>(
                _collectionName,
                q => q.Where(x => x.Urn == identifier))
            .FirstOrDefaultAsync();

        return school == null ? null : SchoolResponseModel.Create(school);
    }

    public async Task<PagedResponseModel<SchoolResponseModel>> Query(SchoolQueryParameters parameters)
    {
        var establishments =
            await PagedItemEnumerableAsync<EdubaseDataObject>(_collectionName, parameters.Page, parameters.PageSize)
                .ToArrayAsync();
        var establishmentsTotalCount = await ItemCountAsync<EdubaseDataObject>(_collectionName);

        var schools = establishments.Select(SchoolResponseModel.Create);
        return PagedResponseModel<SchoolResponseModel>.Create(schools, parameters.Page, parameters.PageSize,
            establishmentsTotalCount);
    }
}