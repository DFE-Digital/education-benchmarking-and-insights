using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Db;

public record FinancesDbOptions : CosmosDatabaseOptions
{
    public int? CfrLatestYear { get; set; }
    public int? AarLatestYear { get; set; }
    public string? EstablishmentCollectionName { get; set; }
}

public abstract class FinancesDb : CosmosDatabase
{
    private readonly int _cfrLatestYear;
    private readonly int _aarLatestYear;
    private readonly string _establishmentCollectionName;

    protected FinancesDb(FinancesDbOptions options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.CfrLatestYear);
        ArgumentNullException.ThrowIfNull(options.AarLatestYear);
        ArgumentNullException.ThrowIfNull(options.EstablishmentCollectionName);

        _cfrLatestYear = options.CfrLatestYear.Value;
        _aarLatestYear = options.AarLatestYear.Value;
        _establishmentCollectionName = options.EstablishmentCollectionName;
    }

    protected async Task<IEnumerable<(int year, T? dataObject)>> GetSchoolFinancesHistory<T>(string urn) where T : QueryableFinancesDataObject
    {
        var (prefix, years) = await GetHistoryCollectionForSchool(urn);
        var tasks = years.Select(year =>
        {
            var collection = BuildFinanceCollectionName(prefix, year);

            return GetSchoolFinances<T>(year, urn, collection);
        }).ToArray();

        return await Task.WhenAll(tasks);
    }

    protected async Task<IEnumerable<(int year, T[] dataObject)>> GetSchoolFinances<T>(IReadOnlyCollection<string> urns) where T : QueryableFinancesDataObject
    {
        var tasks = new[]
        {
            GetSchoolFinances<T>(_aarLatestYear, urns, BuildFinanceCollectionName(Constants.MatAllocatedCollectionPrefix, _aarLatestYear)),
            GetSchoolFinances<T>(_cfrLatestYear, urns, BuildFinanceCollectionName(Constants.MaintainedCollectionPrefix, _cfrLatestYear))
        };

        return await Task.WhenAll(tasks);
    }

    protected async Task<IEnumerable<(int year, T? dataObject)>> GetSchoolFinances<T>(string urn) where T : QueryableFinancesDataObject
    {
        var tasks = new[]
        {
            GetSchoolFinances<T>(_aarLatestYear, urn, BuildFinanceCollectionName(Constants.MatAllocatedCollectionPrefix, _aarLatestYear)),
            GetSchoolFinances<T>(_cfrLatestYear, urn, BuildFinanceCollectionName(Constants.MaintainedCollectionPrefix, _cfrLatestYear))
        };

        return await Task.WhenAll(tasks);
    }

    protected async Task<(int year, T? dataObject)[]> GetTrustFinancesHistory<T>(string companyNumber) where T : QueryableFinancesDataObject
    {
        var tasks = HistoricYears(_aarLatestYear).Select(year =>
        {
            var collection = BuildFinanceCollectionName(Constants.MatOverviewCollectionPrefix, year);

            return GetTrustFinances<T>(year, companyNumber, collection);
        }).ToArray();

        var finances = await Task.WhenAll(tasks);
        return finances;
    }

    private async Task<(int year, T? dataObject)> GetTrustFinances<T>(int year, string companyNumber, string collection) where T : QueryableFinancesDataObject
    {
        return (year, await ItemEnumerableAsync<T>(
                collection,
                q => q.Where(x => x.CompanyNumber == int.Parse(companyNumber)))
            .FirstOrDefaultAsync());
    }


    private async Task<(int, T[])> GetSchoolFinances<T>(int year, IReadOnlyCollection<string> urns, string collectionName) where T : QueryableFinancesDataObject
    {
        if (urns.Count <= 0) return (year, Array.Empty<T>());

        return (year, await ItemEnumerableAsync<T>(collectionName, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToArrayAsync());
    }

    private async Task<(int year, T? dataObject)> GetSchoolFinances<T>(int year, string urn, string collection) where T : QueryableFinancesDataObject
    {
        return (year, await ItemEnumerableAsync<T>(collection, q => q.Where(x => x.Urn == long.Parse(urn))).FirstOrDefaultAsync());
    }

    private async Task<(string Prefix, IEnumerable<int> Years)> GetHistoryCollectionForSchool(string urn)
    {
        var school = await GetSchool(urn);
        switch (school?.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return (Constants.MatAllocatedCollectionPrefix, HistoricYears(_aarLatestYear));
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return (Constants.MaintainedCollectionPrefix, HistoricYears(_cfrLatestYear));
            default:
                throw new ArgumentOutOfRangeException(nameof(school.FinanceType));
        }
    }

    private async Task<EdubaseDataObject?> GetSchool(string urn)
    {
        var school = await ItemEnumerableAsync<EdubaseDataObject>(
                _establishmentCollectionName,
                q => q.Where(x => x.Urn == int.Parse(urn)))
            .FirstOrDefaultAsync();
        return school;
    }

    private static string BuildFinanceCollectionName(string prefix, int year) => $"{prefix}-{year - 1}-{year}";
    private static IEnumerable<int> HistoricYears(int currentYear) => Enumerable.Range(currentYear - Constants.NumberPreviousYears, Constants.NumberPreviousYears + 1);

}