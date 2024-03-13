using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using EducationBenchmarking.Platform.Import.Abstractions;
using EducationBenchmarking.Platform.Import.Db;
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
        [Required] public string? TableName { get; init; }
        [Required] public string? TableKey { get; init; }
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
        var sql = $"SELECT DISTINCT (URN) FROM {options.TableName}";
        var entities = connection.Query<string>(sql).ToList();
        return entities;
    }

    private async Task UpsertComparatorSets(List<string> urns, ComparatorImportServiceOptions options)
    {
        await using var connection = new SqlConnection(options.Sql.ConnectionString);
        connection.Open();
        foreach (var urn in urns)
        {
            _logger.LogInformation($"Transferring comparator sets for school URN: {urn} at {DateTime.Now}");
            var sql = new StringBuilder(
                "SELECT c.all_comparators_all_Id, c.URN, c.UKPRN_URN1, c.UKPRN_URN2, c.UKPRN_URN_CG, c.PeerGroup, c.CostGroup, c.CompareNum, c.compare, c.RANK2, c.Comparator_code, ");
            sql.Append(
                $"c.RANK3, c.ReprocessFlag, c.UseAllCompFlag, c.Range_flag, c.DataReleaseId, c.PartYearDataFlag FROM [{options.Sql.TableName}] c ");
            sql.Append($"WHERE c.URN = {urn}");

            var comparatorData = connection.Query<ComparatorEntryData>(sql.ToString()).ToList();
            _logger.LogInformation($"Query run at {DateTime.Now}");
            var comparatorSets = BuildComparatorSets(comparatorData);
            foreach (var set in comparatorSets)
            {
                await _db.UpsertComparatorSetLookup(urn, set);
            }

            _logger.LogInformation($"Transferred comparator sets for school URN: {urn} at {DateTime.Now}");
        }
        connection.Close();
    }

    private List<ComparatorSetLookupRequest> BuildComparatorSets(List<ComparatorEntryData> data)
    {
        // get a list of unique URNs
        var baseSchoolUrns = data.Select(field => field.URN).Distinct().OrderBy(o => o).ToList();
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
                    .Select(row => new Abstractions.ComparatorSetLookupEntry
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
                    .Select(row => new Abstractions.ComparatorSetLookupEntry
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
                    .Select(row => new Abstractions.ComparatorSetLookupEntry
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
                    .Select(row => new Abstractions.ComparatorSetLookupEntry
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