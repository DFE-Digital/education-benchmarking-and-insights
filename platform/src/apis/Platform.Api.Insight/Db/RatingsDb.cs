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

    public async Task<IEnumerable<RatingResponseModel>> Get(string[] urns, int[] costPools, string[] ragWords)
    {
        // To get around the error `An expression of non-boolean type specified in a context where a condition is expected`
        // use STRING_SPLIT to support the optional additional filtering of RAGs in an additional JOIN
        var costPoolJoin = costPools.Any()
            ? " JOIN (SELECT * FROM STRING_SPLIT(@CostPoolIds, ',', 1)) c ON c.[value] = r.[Cost Pool ID]"
            : string.Empty;
        var ragWordJoin = ragWords.Any()
            ? " JOIN (SELECT * FROM STRING_SPLIT(@RagWords, ',', 1)) w ON w.[value] = r.[RAGWord]"
            : string.Empty;
        var sql = $"SELECT * FROM [RAGRatings] r{costPoolJoin}{ragWordJoin} WHERE r.[URN] IN @URNS AND r.[PeerGroup] = 'Default'";

        var parameters = new
        {
            URNS = urns,
            CostPoolIds = string.Join(",", costPools),
            RagWords = string.Join(",", ragWords),
        };

        using var conn = await _dbFactory.GetConnection();
        var results = conn.Query<RatingsDataObject>(sql, parameters);

        return results.Select(RatingResponseModel.Create);
    }
}

public interface IRatingsDb
{
    Task<IEnumerable<RatingResponseModel>> Get(string[] urns, int[] costPools, string[] ragWords);
}