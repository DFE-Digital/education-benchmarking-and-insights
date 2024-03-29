using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public interface ISchoolsDb
{
    Task<IEnumerable<SchoolExpenditureResponseModel>> Expenditure(string[] urns);
    Task<IEnumerable<SchoolWorkforceResponseModel>> Workforce(string[] urns);
}

[ExcludeFromCodeCoverage]
public record SchoolsDbOptions : FinancialReturnOptions
{
    public string? FloorAreaCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class SchoolsDb : CosmosDatabase, ISchoolsDb
{
    private readonly string _maintainedCollectionName;
    private readonly string _academyCollectionName;
    private readonly string _floorAreaCollectionName;

    public SchoolsDb(IOptions<SchoolsDbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.FloorAreaCollectionName);
        ArgumentNullException.ThrowIfNull(options.Value.CfrLatestYear);
        ArgumentNullException.ThrowIfNull(options.Value.AarLatestYear);

        _academyCollectionName = options.Value.LatestMatAllocated;
        _maintainedCollectionName = options.Value.LatestMaintained;
        _floorAreaCollectionName = options.Value.FloorAreaCollectionName;
    }

    public async Task<IEnumerable<SchoolWorkforceResponseModel>> Workforce(string[] urns)
    {
        var finances = await Finances(urns);

        return finances.Select(SchoolWorkforceResponseModel.Create);
    }

    public async Task<IEnumerable<SchoolExpenditureResponseModel>> Expenditure(string[] urns)
    {
        var finances = await Finances(urns);
        var floorArea = await FloorArea(urns);

        return finances.Select(x => SchoolExpenditureResponseModel.Create(x, floorArea));
    }

    private async Task<IEnumerable<SchoolTrustFinancialDataObject>> Finances(IReadOnlyCollection<string> urns)
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
}