using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Trusts.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.Trusts.Services;

public interface ITrustsService
{
    Task<IEnumerable<TrustCharacteristic>> QueryAsync(string[] companyNumbers);
}

[ExcludeFromCodeCoverage]
public class TrustsService(IDatabaseFactory dbFactory) : ITrustsService
{
    public async Task<IEnumerable<TrustCharacteristic>> QueryAsync(string[] companyNumbers)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new TrustCharacteristicsQuery()
            .WhereCompanyNumberIn(companyNumbers);

        return await conn.QueryAsync<TrustCharacteristic>(builder);
    }
}