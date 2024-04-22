using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Insight.Features.SchoolFinances;
public interface ISchoolFinancesDb
{
    Task<FinancesResponseModel?> Get(string urn);
    Task<IEnumerable<BalanceResponseModel>> GetBalanceHistory(string urn, SchoolHistoryQueryParameters parameters);
    Task<IEnumerable<IncomeResponseModel>> GetIncomeHistory(string urn, SchoolHistoryQueryParameters parameters);
    Task<IEnumerable<ExpenditureResponseModel>> GetExpenditureHistory(string urn, SchoolHistoryQueryParameters parameters);
}

[ExcludeFromCodeCoverage]
public class SchoolFinancesDb : CosmosDatabase, ISchoolFinancesDb
{
    private readonly int _cfrLatestYear;
    private readonly int _aarLatestYear;
    private readonly string _establishmentCollectionName;

    public SchoolFinancesDb(IOptions<DbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.CfrLatestYear);
        ArgumentNullException.ThrowIfNull(options.Value.AarLatestYear);
        ArgumentNullException.ThrowIfNull(options.Value.EstablishmentCollectionName);

        _cfrLatestYear = options.Value.CfrLatestYear.Value;
        _aarLatestYear = options.Value.AarLatestYear.Value;
        _establishmentCollectionName = options.Value.EstablishmentCollectionName;
    }

    public async Task<FinancesResponseModel?> Get(string urn)
    {
        var (prefix, year) = await GetLatestCollectionForSchool(urn);
        var collection = BuildFinanceCollectionName(prefix, year);
        var finances = await GetFinances(year, urn, collection);

        return finances.dataObject == null
            ? null
            : FinancesResponseModel.Create(finances.dataObject, finances.year);
    }
    public async Task<IEnumerable<BalanceResponseModel>> GetBalanceHistory(string urn, SchoolHistoryQueryParameters parameters)
    {
        var finances = await GetHistoryFinances(urn);

        return finances
            .OfType<(int, SchoolTrustFinancialDataObject)>()
            .Select(x => BalanceResponseModel.Create(x.Item2, x.Item1, parameters.Dimension));
    }

    public async Task<IEnumerable<ExpenditureResponseModel>> GetExpenditureHistory(string urn, SchoolHistoryQueryParameters parameters)
    {
        var finances = await GetHistoryFinances(urn);

        return finances
            .OfType<(int, SchoolTrustFinancialDataObject)>()
            .Select(x => ExpenditureResponseModel.Create(x.Item2, x.Item1, parameters.Dimension));
    }

    public async Task<IEnumerable<IncomeResponseModel>> GetIncomeHistory(string urn,SchoolHistoryQueryParameters parameters)
    {
        var finances = await GetHistoryFinances(urn);

        return finances
            .OfType<(int, SchoolTrustFinancialDataObject?)>()
            .Select(x => IncomeResponseModel.Create(x.Item2, x.Item1, parameters.Dimension));
    }

    private async Task<(int year, SchoolTrustFinancialDataObject? dataObject)[]> GetHistoryFinances(string urn)
    {
        var (prefix, years) = await GetHistoryCollectionForSchool(urn);
        var tasks = years.Select(year =>
        {
            var collection = BuildFinanceCollectionName(prefix, year);

            return GetFinances(year, urn, collection);
        }).ToArray();

        var finances = await Task.WhenAll(tasks);
        return finances;
    }

    private async Task<(int year, SchoolTrustFinancialDataObject? dataObject)> GetFinances(int year, string urn, string collection)
    {
        return (year, await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(
                collection,
                q => q.Where(x => x.Urn == long.Parse(urn)))
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

    private async Task<(string Prefix, int Year)> GetLatestCollectionForSchool(string urn)
    {
        var school = await GetEstablishment(urn);
        switch (school?.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return (Constants.MatAllocatedCollectionPrefix, _aarLatestYear);
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return (Constants.MaintainedCollectionPrefix, _cfrLatestYear);
            default:
                throw new ArgumentOutOfRangeException(nameof(school.FinanceType));
        }
    }

    private async Task<(string Prefix, IEnumerable<int> Years)> GetHistoryCollectionForSchool(string urn)
    {
        var school = await GetEstablishment(urn);
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

    private async Task<EdubaseDataObject?> GetEstablishment(string urn)
    {
        var school = await ItemEnumerableAsync<EdubaseDataObject>(
                _establishmentCollectionName,
                q => q.Where(x => x.Urn == int.Parse(urn)))
            .FirstOrDefaultAsync();
        return school;
    }
}