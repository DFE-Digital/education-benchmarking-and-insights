using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EducationBenchmarking.Platform.Domain.DataObjects;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos.Linq;

namespace EducationBenchmarking.Platform.Api.Benchmark.Db;

public interface IBandingDb
{
    Task<Banding[]> FreeSchoolMealBandings();
    Task<IEnumerable<Banding>> SchoolSizeBandings(string? phase = null, string? term = null, decimal? noOfPupils = null, bool? hasSixthForm = null);
}


[ExcludeFromCodeCoverage]
public class BandingDbOptions : CosmosDatabaseOptions
{
    [Required] public string? SizingCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class BandingDb : CosmosDatabase, IBandingDb
{
    private readonly string _collectionName;

    public BandingDb(IOptions<BandingDbOptions> options)
        : base(options.Value)
    {
        _collectionName = options.Value.SizingCollectionName ?? throw new ArgumentNullException(nameof(options.Value.SizingCollectionName));
    }

    public Task<Banding[]> FreeSchoolMealBandings()
    {
        return Task.FromResult(Array.Empty<Banding>());
    }

    public async Task<IEnumerable<Banding>> SchoolSizeBandings(string? phase = null, string? term = null, decimal? noOfPupils = null, bool? hasSixthForm = null)
    {
        var sizes = await ItemEnumerableAsync<SizeLookupDataObject>(_collectionName, q => BuildSizeQueryable(q, phase, term, noOfPupils, hasSixthForm)).ToArrayAsync();

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

    private static IQueryable<SizeLookupDataObject> BuildSizeQueryable(IQueryable<SizeLookupDataObject> queryable, string? phase, string? term, decimal? noOfPupils, bool? hasSixthForm)
    {

        if (!string.IsNullOrEmpty(phase)) queryable = queryable.Where(x => x.OverallPhase == phase);
        if (!string.IsNullOrEmpty(term)) queryable = queryable.Where(x => x.Term == term);
        if (noOfPupils is > 0) queryable = queryable.Where(x => x.NoPupilsMin <= noOfPupils && (!x.NoPupilsMax.IsDefined() || x.NoPupilsMax >= noOfPupils));
        if (hasSixthForm != null) queryable = queryable.Where(x => x.HasSixthForm == hasSixthForm || (!hasSixthForm.GetValueOrDefault() && x.NoPupilsMax == null));

        return queryable;
    }
}