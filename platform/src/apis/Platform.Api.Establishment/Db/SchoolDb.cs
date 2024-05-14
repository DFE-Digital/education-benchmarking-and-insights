using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Platform.Domain;
using Platform.Infrastructure.Cosmos;

namespace Platform.Api.Establishment.Db;

public interface ISchoolDb
{
    Task<SchoolResponseModel?> Get(string urn);
    Task<IEnumerable<SchoolResponseModel>> Query(string? companyNumber, string? laCode, string? phase);
}

[ExcludeFromCodeCoverage]
public record SchoolDbOptions : CosmosDatabaseOptions
{
    [Required] public string? EstablishmentCollectionName { get; set; }
}

[ExcludeFromCodeCoverage]
public class SchoolDb : CosmosDatabase, ISchoolDb
{
    private readonly string _collectionName;

    public SchoolDb(IOptions<SchoolDbOptions> options, ICosmosClientFactory factory) : base(factory)
    {
        ArgumentNullException.ThrowIfNull(options.Value.EstablishmentCollectionName);
        _collectionName = options.Value.EstablishmentCollectionName;
    }

    public async Task<SchoolResponseModel?> Get(string urn)
    {
        var canParse = long.TryParse(urn, out var parsedUrn);
        if (!canParse)
        {
            return null;
        }

        var school = await ItemEnumerableAsync<EdubaseDataObject>(
                _collectionName,
                q => q.Where(x => x.Urn == parsedUrn))
            .FirstOrDefaultAsync();

        return school == null ? null : SchoolResponseModel.Create(school);
    }

    public async Task<IEnumerable<SchoolResponseModel>> Query(string? companyNumber, string? laCode, string? phase)
    {
        var schools = await ItemEnumerableAsync<EdubaseDataObject>(
                _collectionName,
                q =>
                {
                    if (!string.IsNullOrEmpty(companyNumber) && int.TryParse(companyNumber, out var companyParsed))
                    {
                        q = q.Where(x => x.CompanyNumber == companyParsed && x.FinanceType == EstablishmentTypes.Academies);
                    }

                    if (!string.IsNullOrEmpty(laCode) && int.TryParse(laCode, out var laparsed))
                    {
                        q = q.Where(x => x.LaCode == laparsed && x.FinanceType == EstablishmentTypes.Maintained);
                    }

                    if (!string.IsNullOrEmpty(phase))
                    {
                        q = q.Where(x => x.OverallPhase == phase);
                    }

                    return q;
                }).ToArrayAsync();

        return schools.Select(SchoolResponseModel.Create);
    }
}