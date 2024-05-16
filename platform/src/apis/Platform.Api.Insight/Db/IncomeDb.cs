using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface IIncomeDb
{
    Task<IEnumerable<IncomeResponseModel>> GetSchoolHistory(string urn, string dimension);
    Task<IEnumerable<IncomeResponseModel>> GetTrustHistory(string urn, string dimension);
    Task<IncomeResponseModel?> GetSchool(string urn, string dimension);
}

public class IncomeDb : FinancesDb, IIncomeDb
{
    public IncomeDb(IOptions<FinancesDbOptions> options, ICosmosClientFactory factory) : base(options.Value, factory)
    {
    }

    public async Task<IEnumerable<IncomeResponseModel>> GetSchoolHistory(string urn, string dimension)
    {
        var finances = await GetSchoolFinancesHistory<IncomeDataObject>(urn);

        return finances
            .OfType<(int Term, IncomeDataObject? DataObject)>()
            .Select(x => IncomeResponseModel.Create(x.DataObject, x.Term, dimension));
    }

    public async Task<IncomeResponseModel?> GetSchool(string urn, string dimension)
    {
        var finances = await GetSchoolFinances<IncomeDataObject>(urn);
        return finances
            .Where(d => d.dataObject != null)
            .Select(d => IncomeResponseModel.Create(d.dataObject, d.year, dimension))
            .SingleOrDefault();
    }

    public async Task<IEnumerable<IncomeResponseModel>> GetTrustHistory(string companyNumber, string dimension)
    {
        var finances = await GetTrustFinancesHistory<IncomeDataObject>(companyNumber);

        return finances
            .OfType<(int Term, IncomeDataObject? DataObject)>()
            .Select(x => IncomeResponseModel.Create(x.DataObject, x.Term, dimension));
    }
}