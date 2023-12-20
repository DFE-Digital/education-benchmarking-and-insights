using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;
using EducationBenchmarking.Platform.Shared.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace EducationBenchmarking.Platform.Api.Insight.Db;

public interface ISchoolExpenditureDb
{
    Task<PagedResults<SchoolExpenditure>> Query(IEnumerable<KeyValuePair<string, StringValues>> criteria);
}

public class SchoolExpenditureDbOptions
{
    [Required] public string ConnectionString { get; set; }
    [Required] public string DatabaseId { get; set; }
}

public class SchoolExpenditureDb : CosmosDatabase, ISchoolExpenditureDb
{
    private readonly ICollectionService _collectionService;
    
    public SchoolExpenditureDb(IOptions<SchoolExpenditureDbOptions> options, ICollectionService collectionService)
        : base(options.Value.ConnectionString, options.Value.DatabaseId)
    {
        _collectionService = collectionService;
    }
    
    public async Task<PagedResults<SchoolExpenditure>> Query(IEnumerable<KeyValuePair<string, StringValues>> criteria)
    {
        var values = criteria as KeyValuePair<string, StringValues>[] ?? criteria.ToArray();
        var (page, pageSize) = QueryParameters.GetPagingValues(values);
        var urns = values.FirstOrDefault(x => x.Key.ToLower() == "urns").Value.ToString().Split(",");

        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);
        var schools = await GetItemEnumerableAsync<Edubase>(collection.Name,q => q.Where(x => urns.Contains(x.URN.ToString()))).ToListAsync();
        
        var academies = schools.Where(x => x.FinanceType == EstablishmentTypes.Academies).Select(x => x.URN.ToString()).ToArray();
        var maintained = schools.Where(x => x.FinanceType is EstablishmentTypes.Maintained or EstablishmentTypes.Federation).Select(x => x.URN.ToString()).ToArray();
        
        var finances = await Task.WhenAll(GetFinances(maintained, DataGroups.Maintained), GetFinances(academies, DataGroups.Academies));
        
        var schoolsExpenditure = finances.SelectMany(x => x.Select(SchoolFactory.Create));
        
        return PagedResults<SchoolExpenditure>.Create(schoolsExpenditure);
    }

    private async Task<List<SchoolTrustFinancialDataObject>> GetFinances(string[] urns, string dataGroup)
    {
        var finances = new List<SchoolTrustFinancialDataObject>();
        if (urns.Length > 0)
        {
            var collection = await _collectionService.GetLatestCollection(dataGroup);
            finances = await GetItemEnumerableAsync<SchoolTrustFinancialDataObject>(collection.Name,q => q.Where(x => urns.Contains(x.URN.ToString()))).ToListAsync();
        }

        return finances;
    }
}