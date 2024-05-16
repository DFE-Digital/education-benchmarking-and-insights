using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface ICensusDb
{
    Task<IEnumerable<CensusResponseModel>> GetHistory(string urn, string dimension);
    Task<IEnumerable<CensusResponseModel>> Get(string[] urns, string category, string dimension);
    Task<CensusResponseModel?> Get(string urn, string? category, string dimension);
}

public class CensusDb : FinancesDb, ICensusDb
{
    public CensusDb(IOptions<FinancesDbOptions> options, ICosmosClientFactory factory) : base(options.Value, factory)
    {
    }

    public async Task<IEnumerable<CensusResponseModel>> GetHistory(string urn, string dimension)
    {
        var finances = await GetSchoolFinancesHistory<CensusDataObject>(urn);

        return finances
            .OfType<(int Term, CensusDataObject? DataObject)>()
            .Select(x => CensusResponseModel.Create(x.DataObject, x.Term, dimension));
    }

    public async Task<IEnumerable<CensusResponseModel>> Get(string[] urns, string category, string dimension)
    {
        var finances = await GetSchoolFinances<CensusDataObject>(urns);
        return finances
                .SelectMany(x => x.dataObject
                    .Select(d => CensusResponseModel.Create(d, x.year, dimension, category)));
    }

    public async Task<CensusResponseModel?> Get(string urn, string? category, string dimension)
    {
        var finances = await GetSchoolFinances<CensusDataObject>(urn);
        return finances
            .Where(d => d.dataObject != null)
            .Select(d => CensusResponseModel.Create(d.dataObject, d.year, dimension, category))
            .SingleOrDefault();
    }
}