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
public record SchoolFinancesDbOptions : CosmosDatabaseOptions
{
    public int? CfrLatestYear { get; set; }
    public int? AarLatestYear { get; set; }
    public string? EstablishmentCollectionName { get; set; }
}

public interface ISchoolFinancesDb
{
    Task<FinancesResponseModel?> Get(string urn);
    Task<IEnumerable<BalanceResponseModel>> GetBalanceHistory(string urn, Dimension dimension);
    Task<IEnumerable<WorkforceResponseModel>> GetWorkforceHistory(string urn, Dimension dimension);
    Task<IEnumerable<IncomeResponseModel>> GetIncomeHistory(string urn, Dimension dimension);
}

[ExcludeFromCodeCoverage]
public class SchoolFinancesDb : CosmosDatabase, ISchoolFinancesDb
{
    private const int NumberPreviousYears = 4; //Excludes current years
    private const string MaintainedCollectionPrefix = "Maintained";
    private const string MatAllocatedCollectionPrefix = "MAT-Allocations"; //Academy + its MAT's allocated figures
    private const string AcademiesCollectionPrefix = "Academies"; //Academy's own figures 
    private const string MatCentralCollectionPrefix = "MAT-Central"; //MAT only figures
    private const string MatTotalsCollectionPrefix = "MAT-Totals"; //Total of Academy only figures of the MAT
    private const string MatOverviewCollectionPrefix = "MAT-Overview"; //MAT + all of its Academies' figures
    
    private readonly int _cfrLatestYear;
    private readonly int _aarLatestYear;
    private readonly string _establishmentCollectionName;
    
    public SchoolFinancesDb(IOptions<SchoolFinancesDbOptions> options, ICosmosClientFactory factory) : base(factory)
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
    public async Task<IEnumerable<BalanceResponseModel>> GetBalanceHistory(string urn, Dimension dimension)
    {
        var schools = await GetHistoryFinances(urn);

        return schools
            .OfType<(int, SchoolTrustFinancialDataObject)>()
            .Select(school => BalanceResponseModel.Create(school.Item2, school.Item1, dimension));
    }

    public async Task<IEnumerable<WorkforceResponseModel>> GetWorkforceHistory(string urn, Dimension dimension)
    {
        var schools = await GetHistoryFinances(urn);

        return schools
            .OfType<(int, SchoolTrustFinancialDataObject?)>()
            .Select(school => WorkforceResponseModel.Create(school.Item2, school.Item1, dimension));
    }

    public async Task<IEnumerable<IncomeResponseModel>> GetIncomeHistory(string urn, Dimension dimension)
    {
        var schools = await GetHistoryFinances(urn);

        return schools
            .OfType<(int, SchoolTrustFinancialDataObject?)>()
            .Select(school => IncomeResponseModel.Create(school.Item2, school.Item1, dimension));
    }
    
    private async Task<(int year, SchoolTrustFinancialDataObject? dataObject)[]> GetHistoryFinances(string urn)
    {
        var (prefix, years) = await GetHistoryCollectionForSchool(urn);
        var tasks = years.Select(year =>
        {
            var collection = BuildFinanceCollectionName(prefix,year);

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
    
    private static IEnumerable<int> HistoricYears(int currentYear)
    {
        return Enumerable.Range(currentYear - NumberPreviousYears, NumberPreviousYears + 1); 
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
                return (MatAllocatedCollectionPrefix, _aarLatestYear);
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return (MaintainedCollectionPrefix, _cfrLatestYear);
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
                return (MatAllocatedCollectionPrefix, HistoricYears(_aarLatestYear));
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return (MaintainedCollectionPrefix, HistoricYears(_cfrLatestYear));
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