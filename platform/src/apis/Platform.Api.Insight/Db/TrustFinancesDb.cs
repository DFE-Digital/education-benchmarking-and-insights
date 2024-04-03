using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

[ExcludeFromCodeCoverage]
public record TrustFinancesDbOptions : CosmosDatabaseOptions
{
    public int? AarLatestYear { get; set; }
}

public interface ITrustFinancesDb
{
    Task<IEnumerable<BalanceResponseModel>> GetBalanceHistory(string companyNumber, Dimension dimension);
    Task<IEnumerable<IncomeResponseModel>> GetIncomeHistory(string companyNumber, Dimension dimension);
}

[ExcludeFromCodeCoverage]
public class TrustFinancesDb : CosmosDatabase, ITrustFinancesDb
{
    private readonly int _latestYear;

    public TrustFinancesDb(IOptions<TrustFinancesDbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.AarLatestYear);

        _latestYear = options.Value.AarLatestYear.Value;
    }

    public async Task<IEnumerable<BalanceResponseModel>> GetBalanceHistory(string companyNumber, Dimension dimension)
    {
        var finances = await GetHistoryFinances(companyNumber);

        return finances
            .OfType<(int, SchoolTrustFinancialDataObject)>()
            .Select(x => BalanceResponseModel.Create(x.Item2, x.Item1, dimension));
    }

    public async Task<IEnumerable<IncomeResponseModel>> GetIncomeHistory(string companyNumber, Dimension dimension)
    {
        var finances = await GetHistoryFinances(companyNumber);

        return finances
            .OfType<(int, SchoolTrustFinancialDataObject?)>()
            .Select(x => IncomeResponseModel.Create(x.Item2, x.Item1, dimension));
    }

    private async Task<(int year, SchoolTrustFinancialDataObject? dataObject)[]> GetHistoryFinances(string companyNumber)
    {
        var tasks = HistoricYears(_latestYear).Select(year =>
        {
            var collection = BuildFinanceCollectionName(Constants.MatOverviewCollectionPrefix, year);

            return GetFinances(year, companyNumber, collection);
        }).ToArray();

        var finances = await Task.WhenAll(tasks);
        return finances;
    }

    private async Task<(int year, SchoolTrustFinancialDataObject? dataObject)> GetFinances(int year, string companyNumber, string collection)
    {
        return (year, await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(
                collection,
                q => q.Where(x => x.CompanyNumber == int.Parse(companyNumber)))
            .FirstOrDefaultAsync());
    }

    private static IEnumerable<int> HistoricYears(int currentYear)
    {
        return Enumerable.Range(currentYear - Constants.NumberPreviousYears, Constants.NumberPreviousYears + 1);
    }

    private static string BuildFinanceCollectionName(string prefix, int year)
    {
        return $"{prefix}-{year - 1}-{year}";
    }
}