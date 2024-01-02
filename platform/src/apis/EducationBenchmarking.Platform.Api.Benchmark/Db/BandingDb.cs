using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Benchmark.Models;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos.Linq;

namespace EducationBenchmarking.Platform.Api.Benchmark.Db;

public interface IBandingDb
{
    Task<Banding[]> GetFreeSchoolMealBandings();
    Task<IEnumerable<Banding>> GetSchoolSizeBandings(string phase = null, string term = null, decimal? noOfPupils = null, bool? hasSixthForm = null);
}

public class BandingDbOptions
{
    [Required] public string ConnectionString { get; set; }
    [Required] public string DatabaseId { get; set; }
    [Required] public string SizingCollectionName { get; set; }
}

public class BandingDb : CosmosDatabase, IBandingDb
{
    private readonly BandingDbOptions _options;
    
    public BandingDb(IOptions<BandingDbOptions> options)
        : base(options.Value.ConnectionString, options.Value.DatabaseId)
    {
        _options = options.Value;
    }
    
    public Task<Banding[]> GetFreeSchoolMealBandings()
    {
        return Task.FromResult(Array.Empty<Banding>());
    }

    public async Task<IEnumerable<Banding>> GetSchoolSizeBandings(string phase = null, string term = null, decimal? noOfPupils = null, bool? hasSixthForm = null)
    {
        var sizes = await GetItemEnumerableAsync<SizeLookupDataObject>(_options.SizingCollectionName,q => BuildSizeQueryable(q, phase, term, noOfPupils, hasSixthForm)).ToArrayAsync();
        
        return sizes.Select(x => new Banding
        {
            OverallPhase = x.OverallPhase,
            Term = x.Term,
            HasSixthForm = x.HasSixthForm.GetValueOrDefault(),
            Min = x.NoPupilsMin,
            Max = x.NoPupilsMax,
            Scale = x.SizeType
        });
    }

    private static IQueryable<SizeLookupDataObject> BuildSizeQueryable(IQueryable<SizeLookupDataObject> queryable, string phase, string term, decimal? noOfPupils, bool? hasSixthForm)
    {
        
            if (!string.IsNullOrEmpty(phase)) queryable = queryable.Where(x => x.OverallPhase == phase);
            if (!string.IsNullOrEmpty(term)) queryable = queryable.Where(x => x.Term == term);
            if (noOfPupils is > 0) queryable = queryable.Where(x => x.NoPupilsMin <= noOfPupils &&(!x.NoPupilsMax.IsDefined() || x.NoPupilsMax >= noOfPupils));
            if (hasSixthForm != null) queryable = queryable.Where(x => x.HasSixthForm == hasSixthForm || (!hasSixthForm.GetValueOrDefault() && x.NoPupilsMax == null));

            return queryable;
    }
}