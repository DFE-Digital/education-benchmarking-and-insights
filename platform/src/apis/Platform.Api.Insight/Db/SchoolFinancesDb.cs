using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

[ExcludeFromCodeCoverage]
public abstract record SchoolFinancesDbOptions : CosmosDatabaseOptions
{
    private const int NumberPreviousYears = 4; //Excludes current years

    public IEnumerable<int> HistoricYears => Enumerable.Range(LatestYear - NumberPreviousYears, NumberPreviousYears + 1);
    public abstract int LatestYear { get; }
    public abstract string CollectionPrefix { get; }

    public string BuildCollectionName(int? year)
    {
        return $"{CollectionPrefix}-{year - 1}-{year}";
    }
}

//TODO: consider keyed services with .NET 8
// ReSharper disable once UnusedTypeParameter
public interface ISchoolFinancesDb<T>
{
    Task<FinancesResponseModel?> Get(string urn);
    Task<IEnumerable<FinanceBalanceResponseModel>> GetBalanceHistory(string urn, Dimension dimension);
    Task<IEnumerable<FinanceWorkforceResponseModel>> GetWorkforceHistory(string urn, Dimension dimension);
}

[ExcludeFromCodeCoverage]
public abstract class SchoolFinancesDb<T> : CosmosDatabase, ISchoolFinancesDb<T>
{
    private readonly SchoolFinancesDbOptions _options;
    protected SchoolFinancesDb(IOptions<SchoolFinancesDbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        _options = options.Value;
    }

    public async Task<FinancesResponseModel?> Get(string urn)
    {
        var year = _options.LatestYear;
        var collection = _options.BuildCollectionName(year);
        var finances = await GetFinances(year, urn, collection);

        return finances.dataObject == null
            ? null
            : FinancesResponseModel.Create(finances.dataObject, finances.year);
    }
    public async Task<IEnumerable<FinanceBalanceResponseModel>> GetBalanceHistory(string urn, Dimension dimension)
    {
        var schools = await GetHistoryFinances(urn);

        return schools
            .OfType<(int, SchoolTrustFinancialDataObject)>()
            .Select(school => FinanceBalanceResponseModel.Create(school.Item2, school.Item1, dimension));
    }

    public async Task<IEnumerable<FinanceWorkforceResponseModel>> GetWorkforceHistory(string urn, Dimension dimension)
    {
        var schools = await GetHistoryFinances(urn);

        return schools
            .OfType<(int, SchoolTrustFinancialDataObject)>()
            .Select(school => FinanceWorkforceResponseModel.Create(school.Item2, school.Item1, dimension));
    }

    private async Task<(int year, SchoolTrustFinancialDataObject? dataObject)[]> GetHistoryFinances(string urn)
    {
        var tasks = _options.HistoricYears.Select(year =>
        {
            var collection = _options.BuildCollectionName(year);

            return GetFinances(year, urn, collection);
        }).ToArray();

        var schools = await Task.WhenAll(tasks);
        return schools;
    }

    private async Task<(int year, SchoolTrustFinancialDataObject? dataObject)> GetFinances(int year, string urn, string collection)
    {
        return (year, await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(
                collection,
                q => q.Where(x => x.Urn == long.Parse(urn)))
            .FirstOrDefaultAsync());
    }
}

public record Maintained;
public record Academy;
