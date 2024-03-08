using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using EducationBenchmarking.Platform.Domain.Requests;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Import;

public interface IComparatorImportService
{
    Task Import();
}

public class ComparatorImportServiceOptions
{
    [Required] public CosmosOptions? Cosmos { get; init; }
    [Required] public SqlOptions? Sql { get; init; }

    public class SqlOptions
    {
        [Required] public string? ConnectionString { get; init; }
        [Required] public string? ComparatorTableName { get; init; }
        [Required] public string? ComparatorTableKey { get; init; }
        [Required] public string? EntityTableName { get; init; }
        [Required] public string? EntityTableKey { get; init; }
    }

    public class CosmosOptions
    {
        [Required] public string? ConnectionString { get; init; }
        [Required] public string? DatabaseId { get; set; }
        [Required] public string? LookupCollectionName { get; set; }
    }
}

public class ComparatorImportService : IComparatorImportService
{
    private readonly ILogger<ComparatorImportService> _logger;
    private readonly ComparatorImportServiceOptions _options;
    private readonly IComparatorSetLookupDb _db;

    public ComparatorImportService(
        IOptions<ComparatorImportServiceOptions> options,
        ILogger<ComparatorImportService> logger,
        IComparatorSetLookupDb db)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task Import()
    {
        try
        {
            if (_options.Sql == null)
                throw new ArgumentNullException(nameof(_options.Sql));
            if (_options.Cosmos == null)
                throw new ArgumentNullException(nameof(_options.Cosmos));
            
            var urns = EntityUrns(_options.Sql).Result;
            await UpsertComparatorSets(urns, _options);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to import comparator sets");
        }
    }
    private async Task<List<string>> EntityUrns(ComparatorImportServiceOptions.SqlOptions options)
    {
        await using var connection = new SqlConnection(options.ConnectionString);
        var sql = $"SELECT URN FROM {options.EntityTableName}";
        var entities = connection.Query<string>(sql).ToList();
        return entities;
    }

    private async Task UpsertComparatorSets(List<string> urns, ComparatorImportServiceOptions options)
    {
        try
        {
            foreach (var urn in urns)
            {
                await using var connection = new SqlConnection(options.Sql.ConnectionString);
                var sql = new StringBuilder(
                    "SELECT c.all_comparators_all_Id, a.URN, c.UKPRN_URN1, c.UKPRN_URN2, c.UKPRN_URN_CG, c.PeerGroup, c.CostGroup, c.CompareNum, c.compare, c.RANK2, c.Comparator_code, ");
                sql.Append(
                    $"c.RANK3ReprocessFlag, c.UseAllCompFlag, c.Range_flag, c.DataReleaseId, c.PartYearDataFlag FROM [tabular].[{options.Sql.ComparatorTableName}] c");
                sql.Append($"INNER JOIN [tabular].[{options.Sql.EntityTableName}] a ON c.{options.Sql.ComparatorTableKey} = a.{options.Sql.EntityTableKey}");
                sql.Append($"WHERE a.URN = {urn}");
                
                var comparatorData = connection.Query<ComparatorEntryData>(sql.ToString()).ToList();
                var comparatorSets = BuildComparatorSets(comparatorData);
                foreach (var set in comparatorSets)
                {
                    await _db.UpsertComparatorSetLookup(urn, set);
                }
                
                _logger.LogInformation($"Transferred comparator sets for school URN: {urn}");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Failed to upsert data");
        }
    }

    private List<ComparatorSetLookupRequest> BuildComparatorSets(List<ComparatorEntryData> data)
    {
        // get a list of unique URNs
        var baseSchoolUrns = data.Select(field => field.URN).Distinct().ToList();
        var sets = new List<ComparatorSetLookupRequest>();
        
        foreach (var urn in baseSchoolUrns)
        {
            // Default Pupil
            var set = new ComparatorSetLookupRequest
            {
                Urn = urn,
                PeerGroup = "Default",
                CostGroup = "Pupil",
                Entries = data.Where(dp => dp.URN == urn
                                           && dp is { PeerGroup: "Default", CostGroup: "Pupil" })
                    .Select(row => new EducationBenchmarking.Platform.Domain.Requests.ComparatorSetLookupEntry
                    {
                        ComparatorCode = row.Comparator_code,
                        Compare = row.compare,
                        CompareNum = row.CompareNum,
                        DataReleaseId = row.DataReleaseId,
                        PartYearDataFlag = row.PartYearDataFlag,
                        RangeFlag = row.Range_flag,
                        Rank2 = row.RANK2,
                        Rank3 = row.RANK3,
                        ReprocessFlag = row.ReprocessFlag,
                        UkPrnUrn1 = row.UKPRN_URN1,
                        UkPrnUrn2 = row.UKPRN_URN2,
                        UkPrnUrnCg = row.UKPRN_URN_CG,
                        UseAllCompFlag = row.UseAllCompFlag
                    }).ToArray()
            };
            sets.Add(set);

            // Default Area
            set = new ComparatorSetLookupRequest
            {
                Urn = urn,
                PeerGroup = "Default",
                CostGroup = "Area",
                Entries = data.Where(dp => dp.URN == urn
                                           && dp is { PeerGroup: "Default", CostGroup: "Area" })
                    .Select(row => new EducationBenchmarking.Platform.Domain.Requests.ComparatorSetLookupEntry
                    {
                        ComparatorCode = row.Comparator_code,
                        Compare = row.compare,
                        CompareNum = row.CompareNum,
                        DataReleaseId = row.DataReleaseId,
                        PartYearDataFlag = row.PartYearDataFlag,
                        RangeFlag = row.Range_flag,
                        Rank2 = row.RANK2,
                        Rank3 = row.RANK3,
                        ReprocessFlag = row.ReprocessFlag,
                        UkPrnUrn1 = row.UKPRN_URN1,
                        UkPrnUrn2 = row.UKPRN_URN2,
                        UkPrnUrnCg = row.UKPRN_URN_CG,
                        UseAllCompFlag = row.UseAllCompFlag
                    }).ToArray()
            };
            sets.Add(set);

            // Mixed Pupil
            set = new ComparatorSetLookupRequest
            {
                Urn = urn,
                PeerGroup = "Mixed",
                CostGroup = "Pupil",
                Entries = data.Where(dp => dp.URN == urn
                                           && dp is { PeerGroup: "Mixed", CostGroup: "Pupil" })
                    .Select(row => new EducationBenchmarking.Platform.Domain.Requests.ComparatorSetLookupEntry
                    {
                        ComparatorCode = row.Comparator_code,
                        Compare = row.compare,
                        CompareNum = row.CompareNum,
                        DataReleaseId = row.DataReleaseId,
                        PartYearDataFlag = row.PartYearDataFlag,
                        RangeFlag = row.Range_flag,
                        Rank2 = row.RANK2,
                        Rank3 = row.RANK3,
                        ReprocessFlag = row.ReprocessFlag,
                        UkPrnUrn1 = row.UKPRN_URN1,
                        UkPrnUrn2 = row.UKPRN_URN2,
                        UkPrnUrnCg = row.UKPRN_URN_CG,
                        UseAllCompFlag = row.UseAllCompFlag
                    }).ToArray()
            };
            sets.Add(set);

            // Mixed Area
            set = new ComparatorSetLookupRequest
            {
                Urn = urn,
                PeerGroup = "Mixed",
                CostGroup = "Area",
                Entries = data.Where(dp => dp.URN == urn
                                           && dp is { PeerGroup: "Mixed", CostGroup: "Area" })
                    .Select(row => new EducationBenchmarking.Platform.Domain.Requests.ComparatorSetLookupEntry
                    {
                        ComparatorCode = row.Comparator_code,
                        Compare = row.compare,
                        CompareNum = row.CompareNum,
                        DataReleaseId = row.DataReleaseId,
                        PartYearDataFlag = row.PartYearDataFlag,
                        RangeFlag = row.Range_flag,
                        Rank2 = row.RANK2,
                        Rank3 = row.RANK3,
                        ReprocessFlag = row.ReprocessFlag,
                        UkPrnUrn1 = row.UKPRN_URN1,
                        UkPrnUrn2 = row.UKPRN_URN2,
                        UkPrnUrnCg = row.UKPRN_URN_CG,
                        UseAllCompFlag = row.UseAllCompFlag
                    }).ToArray()
            };
            sets.Add(set);
        }

        return sets;
    }

    
}