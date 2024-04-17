using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Establishment.Features.Trusts;

public interface ITrustsDb
{
    Task<TrustResponseModel?> Get(int identifier);
}

[ExcludeFromCodeCoverage]
public class TrustsDb : CosmosDatabase, ITrustsDb
{
    private readonly string _collectionName;

    public TrustsDb(IOptions<DbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.EstablishmentCollectionName);
        _collectionName = options.Value.EstablishmentCollectionName;
    }

    public async Task<TrustResponseModel?> Get(int identifier)
    {
        var trust = await ItemEnumerableAsync<EdubaseDataObject>(
                _collectionName,
                q => q.Where(x => x.CompanyNumber == identifier))
            .FirstOrDefaultAsync();

        return trust == null ? null : TrustResponseModel.Create(trust);
    }
}