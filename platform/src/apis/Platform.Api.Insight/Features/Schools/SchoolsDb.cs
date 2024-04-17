using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Features.Schools;

public interface ISchoolsDb
{
    Task<IEnumerable<SchoolExpenditureResponseModel>> Expenditure(SchoolsQueryParameters parameters);
    Task<IEnumerable<FinancesResponseModel>> Finances(SchoolsQueryParameters parameters);
}


[ExcludeFromCodeCoverage]
public class SchoolsDb : CosmosDatabase, ISchoolsDb
{
    private const string MaintainedCollectionPrefix = "Maintained";
    private const string MatAllocatedCollectionPrefix = "MAT-Allocations"; //Academy + its MAT's allocated figures
    
    private readonly string _maintainedCollectionName;
    private readonly string _academyCollectionName;
    private readonly string _floorAreaCollectionName;

    public SchoolsDb(IOptions<DbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.FloorAreaCollectionName);
        ArgumentNullException.ThrowIfNull(options.Value.CfrLatestYear);
        ArgumentNullException.ThrowIfNull(options.Value.AarLatestYear);

        _academyCollectionName = BuildCollectionName(MatAllocatedCollectionPrefix, options.Value.AarLatestYear);
        _maintainedCollectionName = BuildCollectionName(MaintainedCollectionPrefix, options.Value.CfrLatestYear);
        _floorAreaCollectionName = options.Value.FloorAreaCollectionName;
    }
    
    public async Task<IEnumerable<SchoolExpenditureResponseModel>> Expenditure(SchoolsQueryParameters parameters)
    {
        var finances = await QueryFinances(parameters.Urns);
        var floorArea = await FloorArea(parameters.Urns);

        return finances.Select(x => SchoolExpenditureResponseModel.Create(x, floorArea));
    }

    public async Task<IEnumerable<FinancesResponseModel>> Finances(SchoolsQueryParameters parameters)
    {
        var finances = await QueryFinances(parameters.Urns);

        return finances.Select(x => FinancesResponseModel.Create(x));
    }

    private async Task<IEnumerable<SchoolTrustFinancialDataObject>> QueryFinances(IReadOnlyCollection<string> urns)
    {
        var tasks = new[]
        {
            FinancesFor(urns, _maintainedCollectionName),
            FinancesFor(urns, _academyCollectionName)
        };

        var finances = await Task.WhenAll(tasks);
        var combined = new List<SchoolTrustFinancialDataObject>();

        combined.AddRange(finances[0]);
        combined.AddRange(finances[1]);

        return combined;
    }

    private async Task<SchoolTrustFinancialDataObject[]> FinancesFor(IReadOnlyCollection<string> urns, string collectionName)
    {
        if (urns.Count <= 0) return Array.Empty<SchoolTrustFinancialDataObject>();

        return await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(collectionName, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToArrayAsync();
    }

    private async Task<FloorAreaDataObject[]> FloorArea(IReadOnlyCollection<string> urns)
    {
        if (urns.Count <= 0) return Array.Empty<FloorAreaDataObject>();

        return await ItemEnumerableAsync<FloorAreaDataObject>(_floorAreaCollectionName, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToArrayAsync();
    }
    private static string BuildCollectionName(string prefix, int? year)
    {
        return $"{prefix}-{year - 1}-{year}";
    }
}