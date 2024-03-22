using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain.DataObjects;
using Platform.Domain.Responses;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface ISchoolsDb
{
    Task<IEnumerable<SchoolExpenditure>> Expenditure(string[] urns);
    Task<IEnumerable<SchoolWorkforce>> Workforce(string[] urns);
}

[ExcludeFromCodeCoverage]
public record SchoolsDbOptions : CosmosDatabaseOptions
{
    public string? FloorAreaCollectionName { get; set; }
};

[ExcludeFromCodeCoverage]
public class SchoolsDb : CosmosDatabase, ISchoolsDb
{
    private readonly ICollectionService _collectionService;
    private readonly SchoolsDbOptions _options;

    public SchoolsDb(IOptions<SchoolsDbOptions> options, ICollectionService collectionService) : base(options.Value)
    {
        _options = options.Value;
        _collectionService = collectionService;
    }

    public async Task<IEnumerable<SchoolWorkforce>> Workforce(string[] urns)
    {
        var finances = await Finances(urns);

        return finances.Select(SchoolWorkforce.Create);
    }

    public async Task<IEnumerable<SchoolExpenditure>> Expenditure(string[] urns)
    {
        var finances = await Finances(urns);
        var floorArea = await FloorArea(urns);
        
        return finances.Select(x => SchoolExpenditure.Create(x, floorArea));
    }

    private async Task<IEnumerable<SchoolTrustFinancialDataObject>> Finances(IReadOnlyCollection<string> urns)
    {
        var tasks = new[]
        {
            FinancesForDataGroup(urns, DataGroups.Maintained),
            FinancesForDataGroup(urns, DataGroups.MatAllocated)
        };

        var finances = await Task.WhenAll(tasks);
        var combined = new List<SchoolTrustFinancialDataObject>();
        
        combined.AddRange(finances[0]);
        combined.AddRange(finances[1]);
        
        return combined;
    }

    private async Task<SchoolTrustFinancialDataObject[]> FinancesForDataGroup(IReadOnlyCollection<string> urns, string dataGroup)
    {
        if (urns.Count <= 0) return Array.Empty<SchoolTrustFinancialDataObject>();

        var collection = await _collectionService.LatestCollection(dataGroup);
        return await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(collection.Name, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToArrayAsync();
    }

    private async Task<FloorAreaDataObject[]> FloorArea(IReadOnlyCollection<string> urns)
    {
        if (urns.Count <= 0) return Array.Empty<FloorAreaDataObject>();
        
        var areaCollection = _options.FloorAreaCollectionName;
        ArgumentNullException.ThrowIfNull(areaCollection); 
            
        return await ItemEnumerableAsync<FloorAreaDataObject>(areaCollection, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToArrayAsync();
    }
}