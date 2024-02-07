using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Domain;
using EducationBenchmarking.Platform.Domain.DataObjects;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Api.Insight.Db;

public interface ISchoolsDb
{
    Task<PagedSchoolExpenditure> Expenditure(IEnumerable<string> urns, int page, int pageSize);
    Task<PagedSchoolWorkforce> Workforce(IEnumerable<string> urns, int page, int pageSize);
    Task<IEnumerable<Rating>> SchoolRatings(string phase, string term, string size, string fsm);
}

[ExcludeFromCodeCoverage]
public class SchoolsDbOptions : CosmosDatabaseOptions
{
    [Required] public string? RatingCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class SchoolsDb : CosmosDatabase, ISchoolsDb
{
    private readonly ICollectionService _collectionService;
    private readonly string _collectionName;
    
    public SchoolsDb(IOptions<SchoolsDbOptions> options, ICollectionService collectionService) : base(options.Value)
    {
        _collectionName = options.Value.RatingCollectionName ?? throw new ArgumentNullException(nameof(options.Value.RatingCollectionName));
        _collectionService = collectionService;
    }
    
    public async Task<PagedSchoolWorkforce> Workforce(IEnumerable<string> urns, int page, int pageSize)
    {
        var finances = await Finances(urns);

        return PagedSchoolWorkforce.Create(finances, page, pageSize);
    }
    
    public async Task<PagedSchoolExpenditure> Expenditure(IEnumerable<string> urns, int page, int pageSize)
    {
        var finances = await Finances(urns);
        
        return PagedSchoolExpenditure.Create(finances, page, pageSize);
    }

    public async Task<IEnumerable<Rating>> SchoolRatings(string phase, string term, string size, string fsm)
    {
        var ratings = await ItemEnumerableAsync<SchoolRatingDataObject>(_collectionName,q => BuildRatingsQueryable(q, phase, term, size, fsm)).ToArrayAsync();

        return ratings.Select(x => new Rating
        {
            AssessmentArea = x.AssessmentArea,
            Divisor = x.Divisor,
            ScoreLow = x.ScoreLow,
            ScoreHigh = x.ScoreHigh,
            RatingText = x.RatingText,
            RatingColour = x.RatingColour
        });
    }
    
    private async Task<List<SchoolTrustFinancialDataObject>> Finances(IEnumerable<string> urns)
    {
        var collection = await _collectionService.LatestCollection(DataGroups.Edubase);
        var schools = await ItemEnumerableAsync<EdubaseDataObject>(collection.Name,q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToListAsync();
        
        var academies = schools.Where(x => x.FinanceType == EstablishmentTypes.Academies).Select(x => x.Urn.ToString()).ToArray();
        var maintained = schools.Where(x => x.FinanceType is EstablishmentTypes.Maintained or EstablishmentTypes.Federation).Select(x => x.Urn.ToString()).ToArray();
        
        var tasks = new[]
        {
            FinancesForDataGroup(maintained, DataGroups.Maintained),
            FinancesForDataGroup(academies, DataGroups.Academies)
        };
        
        var finances = await Task.WhenAll(tasks);

        finances[0].AddRange(finances[1]);
        return finances[0];
    }
    
    private async Task<List<SchoolTrustFinancialDataObject>> FinancesForDataGroup(IReadOnlyCollection<string> urns, string dataGroup)
    {
        var finances = new List<SchoolTrustFinancialDataObject>();
        if (urns.Count > 0)
        {
            var collection = await _collectionService.LatestCollection(dataGroup);
            finances = await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(collection.Name,q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToListAsync();
        }

        return finances;
    }
    
    private static IQueryable<SchoolRatingDataObject> BuildRatingsQueryable(IQueryable<SchoolRatingDataObject> queryable, string phase, string term, string size, string fsm)
    {
        if (!string.IsNullOrEmpty(phase)) queryable = queryable.Where(x => x.OverallPhase == phase);
        if (!string.IsNullOrEmpty(term)) queryable = queryable.Where(x => x.Term == term);
        if (!string.IsNullOrEmpty(size)) queryable = queryable.Where(x => x.Size == size);
        if (!string.IsNullOrEmpty(fsm)) queryable = queryable.Where(x => x.Fsm == fsm);
        
        return queryable;
    }
}