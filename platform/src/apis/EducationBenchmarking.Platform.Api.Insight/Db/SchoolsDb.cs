using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Insight.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Insight.Db;

public interface ISchoolsDb
{
    Task<PagedSchoolExpenditure> GetExpenditure(IEnumerable<string> urns, int page, int pageSize);
    Task<PagedSchoolWorkforce> GetWorkforce(IEnumerable<string> urns, int page, int pageSize);
}

public class SchoolsDbOptions
{
    [Required] public string ConnectionString { get; set; }
    [Required] public string DatabaseId { get; set; }
}

public class SchoolsDb : CosmosDatabase, ISchoolsDb
{
    private readonly ICollectionService _collectionService;
    
    public SchoolsDb(IOptions<SchoolsDbOptions> options, ICollectionService collectionService)
        : base(options.Value.ConnectionString, options.Value.DatabaseId)
    {
        _collectionService = collectionService;
    }
    
    public async Task<PagedSchoolWorkforce> GetWorkforce(IEnumerable<string> urns, int page, int pageSize)
    {
        var finances = await GetFinances(urns);

        return PagedSchoolWorkforce.Create(finances, page, pageSize);
    }
    
    public async Task<PagedSchoolExpenditure> GetExpenditure(IEnumerable<string> urns, int page, int pageSize)
    {
        var finances = await GetFinances(urns);
        
        return PagedSchoolExpenditure.Create(finances, page, pageSize);
    }

    private async Task<List<SchoolTrustFinancialDataObject>> GetFinances(IEnumerable<string> urns)
    {
        var collection = await _collectionService.GetLatestCollection(DataGroups.Edubase);
        var schools = await GetItemEnumerableAsync<Edubase>(collection.Name,q => q.Where(x => urns.Contains(x.URN.ToString()))).ToListAsync();
        
        var academies = schools.Where(x => x.FinanceType == EstablishmentTypes.Academies).Select(x => x.URN.ToString()).ToArray();
        var maintained = schools.Where(x => x.FinanceType is EstablishmentTypes.Maintained or EstablishmentTypes.Federation).Select(x => x.URN.ToString()).ToArray();
        
        var tasks = new[]
        {
            GetFinancesForDataGroup(maintained, DataGroups.Maintained),
            GetFinancesForDataGroup(academies, DataGroups.Academies)
        };
        
        var finances = await Task.WhenAll(tasks);

        finances[0].AddRange(finances[1]);
        return finances[0];
    }
    
    private async Task<List<SchoolTrustFinancialDataObject>> GetFinancesForDataGroup(IReadOnlyCollection<string> urns, string dataGroup)
    {
        var finances = new List<SchoolTrustFinancialDataObject>();
        if (urns.Count > 0)
        {
            var collection = await _collectionService.GetLatestCollection(dataGroup);
            finances = await GetItemEnumerableAsync<SchoolTrustFinancialDataObject>(collection.Name,q => q.Where(x => urns.Contains(x.URN.ToString()))).ToListAsync();
        }

        return finances;
    }
}