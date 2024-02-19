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
}

[ExcludeFromCodeCoverage]
public record SchoolsDbOptions : CosmosDatabaseOptions;

[ExcludeFromCodeCoverage]
public class SchoolsDb : CosmosDatabase, ISchoolsDb
{
    private readonly ICollectionService _collectionService;

    public SchoolsDb(IOptions<SchoolsDbOptions> options, ICollectionService collectionService) : base(options.Value)
    {
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

    private async Task<List<SchoolTrustFinancialDataObject>> Finances(IEnumerable<string> urns)
    {
        var collection = await _collectionService.LatestCollection(DataGroups.Edubase);
        var schools = await ItemEnumerableAsync<EdubaseDataObject>(collection.Name, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToListAsync();

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
            finances = await ItemEnumerableAsync<SchoolTrustFinancialDataObject>(collection.Name, q => q.Where(x => urns.Contains(x.Urn.ToString()))).ToListAsync();
        }

        return finances;
    }
}