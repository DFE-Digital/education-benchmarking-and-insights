using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface ISchoolMetricsDb
{
    Task<FloorAreaResponseModel?> FloorArea(string urn);
}

[ExcludeFromCodeCoverage]
public record SchoolMetricsDbOptions : FinancialReturnOptions
{
    public string? FloorAreaCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class SchoolMetricsDb : CosmosDatabase, ISchoolMetricsDb
{
    private readonly string _floorAreaCollectionName;

    public SchoolMetricsDb(IOptions<SchoolMetricsDbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.FloorAreaCollectionName);

        _floorAreaCollectionName = options.Value.FloorAreaCollectionName;
    }

    public async Task<FloorAreaResponseModel?> FloorArea(string urn)
    {
        return await ItemEnumerableAsync<FloorAreaDataObject>(_floorAreaCollectionName,
                q => q.Where(x => x.Urn.ToString() == urn))
            .Select(FloorAreaResponseModel.Create)
            .FirstOrDefaultAsync();
    }
}