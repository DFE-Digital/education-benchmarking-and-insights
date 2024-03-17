using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Domain.DataObjects;
using Platform.Domain.Responses;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface ISchoolsDb
{
    Task<IEnumerable<SchoolExpenditure>> Expenditure(IEnumerable<string> urns);
    Task<IEnumerable<SchoolWorkforce>> Workforce(IEnumerable<string> urns);
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

    public async Task<IEnumerable<SchoolWorkforce>> Workforce(IEnumerable<string> urns)
    {
        var finances = await Finances(urns);

        return finances.Select(SchoolWorkforce.Create);
    }

    public async Task<IEnumerable<SchoolExpenditure>> Expenditure(IEnumerable<string> urns)
    {

        var areaCollection = _options.FloorAreaCollectionName;
        ArgumentNullException.ThrowIfNull(areaCollection);

        var schools = urns.ToArray();
        var finances = await Finances(schools);

        var floorArea = await ItemEnumerableAsync<FloorAreaDataObject>(areaCollection, q => q.Where(x => schools.Contains(x.Urn.ToString()))).ToListAsync();

        return finances.Select(x => SchoolExpenditure.Create(x, floorArea));
    }

    private async Task<List<SchoolTrustFinancialDataObject>> Finances(IEnumerable<string> urns)
    {
        var collection = await _collectionService.LatestCollection(DataGroups.Edubase);
        var schools = await ItemEnumerableAsync<EdubaseDataObject>(collection.Name, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToListAsync();

        var academies = schools.Where(x => x.FinanceType == EstablishmentTypes.Academies).Select(x => x.Urn.ToString()).ToArray();
        var maintained = schools.Where(x => x.FinanceType is EstablishmentTypes.Maintained or EstablishmentTypes.Federation).Select(x => x.Urn.ToString()).ToArray();

        var tasks = new[]
        {
            FinancesForDataGroup(maintained, DataGroups.Maintained),
            FinancesForDataGroup(academies, DataGroups.MatAllocated)
        };

        var finances = await Task.WhenAll(tasks);

        finances[0].AddRange(finances[1]);
        return finances[0];
    }

    private async Task<List<SchoolTrustFinancialDataObject>> FinancesForDataGroup(IReadOnlyCollection<string> urns, string dataGroup)
    {
        var finances = new List<SchoolTrustFinancialDataObject>();
        if (urns.Count > 0)
        {
            var collection = await _collectionService.LatestCollection(dataGroup);
            finances = await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(collection.Name, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToListAsync();
        }

        return finances;
    }
}