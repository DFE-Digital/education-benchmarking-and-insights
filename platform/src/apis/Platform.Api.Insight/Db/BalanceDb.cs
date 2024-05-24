using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface IBalanceDb
{
    Task<IEnumerable<BalanceResponseModel>> GetSchoolHistory(string urn, string dimension);
    Task<IEnumerable<BalanceResponseModel>> GetTrustHistory(string companyNumber, string dimension);
    Task<BalanceResponseModel?> GetSchool(string urn, string dimension);
    Task<BalanceResponseModel?> GetTrust(string companyNumber, string dimension);
}

public class BalanceDb : FinancesDb, IBalanceDb
{
    public BalanceDb(IOptions<FinancesDbOptions> options, ICosmosClientFactory factory) : base(options.Value, factory)
    {
    }

    public async Task<IEnumerable<BalanceResponseModel>> GetSchoolHistory(string urn, string dimension)
    {
        var finances = await GetSchoolFinancesHistory<BalanceDataObject>(urn);

        return finances
            .OfType<(int Term, BalanceDataObject? DataObject)>()
            .Select(x => BalanceResponseModel.Create(x.DataObject, x.Term, dimension));
    }

    public async Task<BalanceResponseModel?> GetSchool(string urn, string dimension)
    {
        var finances = await GetSchoolFinances<BalanceDataObject>(urn);
        return finances
            .Where(d => d.dataObject != null)
            .Select(d => BalanceResponseModel.Create(d.dataObject, d.year, dimension))
            .SingleOrDefault();
    }

    public async Task<BalanceResponseModel?> GetTrust(string companyNumber, string dimension)
    {
        var finances = await GetTrustFinances<BalanceDataObject>(companyNumber);
        return BalanceResponseModel.Create(finances.dataObject, finances.year, dimension);
    }

    public async Task<IEnumerable<BalanceResponseModel>> GetTrustHistory(string companyNumber, string dimension)
    {
        var finances = await GetTrustFinancesHistory<BalanceDataObject>(companyNumber);

        return finances
            .OfType<(int Term, BalanceDataObject? DataObject)>()
            .Select(x => BalanceResponseModel.Create(x.DataObject, x.Term, dimension));
    }
}