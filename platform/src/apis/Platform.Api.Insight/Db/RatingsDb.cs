using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Platform.Domain;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Insight.Db;

public class RatingsDb : IRatingsDb
{
    private readonly IDatabaseFactory _dbFactory;

    public RatingsDb(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;

        SetupMappings();
    }

    private static void SetupMappings()
    {
        var columnMap = new ColumnMap();
        columnMap.Add("CostPool", "Cost Pool");
        columnMap.Add("CostPoolID", "Cost Pool ID");
        columnMap.Add("MidDecile", "5thDecile");
        columnMap.Add("ReductionText", "%ReductionText");
        columnMap.Add("Comparators", "%Comparators");
        columnMap.Add("PercentDiffText", "%DiffText");
        columnMap.Add("PoundDiffText", "£DiffText");
        columnMap.Add("YOYchange", "YOY%change");
        columnMap.Add("YOYchangeText", "YOY%changeText");

        SqlMapper.SetTypeMap(typeof(RatingsDataObject), new CustomPropertyTypeMap(typeof(RatingsDataObject), (type, columnName) => type.GetProperty(columnMap[columnName])));
    }

    public async Task<IEnumerable<RatingResponseModel>> Get(string[] urns)
    {
        const string sql = "SELECT * from RAGRatings where URN IN @URNS AND PeerGroup = 'Default'";
        var parameters = new { URNS = urns };

        using var conn = await _dbFactory.GetConnection();
        var results = conn.Query<RatingsDataObject>(sql, parameters);

        return results.Select(RatingResponseModel.Create);
    }
}

public interface IRatingsDb
{
    Task<IEnumerable<RatingResponseModel>> Get(string[] urns);
}