using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Insight.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Insight.Db;

public interface ISchoolExpenditureDb
{
    Task<PagedSchoolExpenditure> GetExpenditure(string[] urns, int page, int pageSize);
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
    
    public async Task<PagedSchoolExpenditure> GetExpenditure(string[] urns, int page, int pageSize)
    {
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);
        var schools = await GetItemEnumerableAsync<Edubase>(collection.Name,q => q.Where(x => urns.Contains(x.URN.ToString()))).ToListAsync();
        
        var academies = schools.Where(x => x.FinanceType == EstablishmentTypes.Academies).Select(x => x.URN.ToString()).ToArray();
        var maintained = schools.Where(x => x.FinanceType is EstablishmentTypes.Maintained or EstablishmentTypes.Federation).Select(x => x.URN.ToString()).ToArray();
        
        var finances = await Task.WhenAll(GetFinances(maintained, DataGroups.Maintained), GetFinances(academies, DataGroups.Academies));

        finances[0].AddRange(finances[1]);
        
        return PagedSchoolExpenditure.Create(finances[0], page, pageSize);
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