using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
public record TrustDbOptions : CosmosDatabaseOptions
{
    [Required] public string? EstablishmentCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class TrustDb : CosmosDatabase, ITrustDb
{
    private readonly string _collectionName;

    public TrustDb(IOptions<TrustDbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.EstablishmentCollectionName);
        _collectionName = options.Value.EstablishmentCollectionName;
    }

    public async Task<TrustResponseModel?> Get(string identifier)
    {
        var canParse = long.TryParse(identifier, out var parsedIdentifier);
        if (!canParse)
        {
            return null;
        }

        var trust = await ItemEnumerableAsync<EdubaseDataObject>(
                _collectionName,
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

        var schools = await ItemEnumerableAsync<EdubaseDataObject>(
            _collectionName,
                q => q.Where(x => x.CompanyNumber == parsedIdentifier)).ToListAsync();

        return schools.Select(SchoolResponseModel.Create);
    }
}