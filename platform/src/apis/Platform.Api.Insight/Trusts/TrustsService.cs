using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;
namespace Platform.Api.Insight.Trusts;

public interface ITrustsService
{
    Task<IEnumerable<TrustCharacteristic>> QueryCharacteristicAsync(string[] companyNumbers);
}

public class TrustsService(IDatabaseFactory dbFactory) : ITrustsService
{
    public async Task<IEnumerable<TrustCharacteristic>> QueryCharacteristicAsync(string[] companyNumbers)
    {
        using var conn = await dbFactory.GetConnection();
        const string sql = "SELECT * FROM TrustCharacteristic WHERE CompanyNumber IN @CompanyNumbers";
        var parameters = new
        {
            CompanyNumbers = companyNumbers
        };

        return await conn.QueryAsync<TrustCharacteristic>(sql, parameters);
    }
}