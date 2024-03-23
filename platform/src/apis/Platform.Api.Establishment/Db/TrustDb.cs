using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Establishment.Db;

public interface ITrustDb
{
    Task<TrustResponseModel?> Get(string identifier);
    Task<IEnumerable<SchoolResponseModel>> Schools(string identifier);
}

[ExcludeFromCodeCoverage]
public record TrustDbOptions : CosmosDatabaseOptions;

[ExcludeFromCodeCoverage]
public class TrustDb : CosmosDatabase, ITrustDb
{
    private readonly ICollectionService _collectionService;

    public TrustDb(IOptions<TrustDbOptions> options, ICollectionService collectionService) : base(options.Value)
    {
        _collectionService = collectionService;
    }

    public async Task<TrustResponseModel?> Get(string identifier)
    {
        var canParse = long.TryParse(identifier, out var parsedIdentifier);
        if (!canParse)
        {
            return null;
        }

        var collection = await _collectionService.LatestCollection(DataGroups.Edubase);

        var trust = await ItemEnumerableAsync<EdubaseDataObject>(
                collection.Name,
                q => q.Where(x => x.CompanyNumber == parsedIdentifier))
            .FirstOrDefaultAsync();

        return trust == null ? null : TrustResponseModel.Create(trust);
    }

    public async Task<IEnumerable<SchoolResponseModel>> Schools(string identifier)
    {
        var canParse = long.TryParse(identifier, out var parsedIdentifier);
        if (!canParse)
        {
            return Array.Empty<SchoolResponseModel>();
        }

        var collection = await _collectionService.LatestCollection(DataGroups.Edubase);

        var schools = await ItemEnumerableAsync<EdubaseDataObject>(
                collection.Name,
                q => q.Where(x => x.CompanyNumber == parsedIdentifier)).ToListAsync();

        return schools.Select(SchoolResponseModel.Create);
    }
}