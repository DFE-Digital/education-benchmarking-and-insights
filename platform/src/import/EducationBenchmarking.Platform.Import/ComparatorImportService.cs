using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Import;

public interface IComparatorImportService
{
    Task Import();
}

public class ComparatorImportServiceOptions
{
    [Required] public CosmosOptions? Cosmos { get; set; }
    [Required] public SqlOptions? Sql { get; set; }

    public class SqlOptions
    {
        [Required] public string? ConnectionString { get; set; }
        [Required] public string? TableName { get; set; }
    }

    public class CosmosOptions
    {
        [Required] public string? ConnectionString { get; set; }
        [Required] public string? DatabaseId { get; set; }
    }
}

public class ComparatorImportService : IComparatorImportService
{
    private readonly ILogger<ComparatorImportService> _logger;
    private readonly ComparatorImportServiceOptions _options;
    private ICollectionService _collectionService;

    public ComparatorImportService(
        IOptions<ComparatorImportServiceOptions> options, 
        ILogger<ComparatorImportService> logger, 
        ICollectionService collectionService)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _collectionService = collectionService ?? throw new ArgumentNullException(nameof(collectionService));
    }
    
    public Task Import()
    {
        try
        {
            if (_options.Sql != null)
            {
                var data = ComparatorData(_options.Sql).Result;
                var sets = BuildComparatorSets(data.Tables[0]).Result;
                // TODO: var result = await _db.UpsertComparatorSets(sets);
                // TODO: return result.CreateResponse();
            }
            else
            {
                throw new ArgumentNullException(nameof(_options.Sql));
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to import comparator sets");
        }

        return Task.CompletedTask;
    }
    
    private async Task<DataSet> ComparatorData(ComparatorImportServiceOptions.SqlOptions options)
    { 
        await using var connection = new SqlConnection(options.ConnectionString);
        var sql = "SELECT trust_academy_Id, UKPRN_URN1, UKPRN_URN2, UKPRN_URN_CG, PeerGroup, CostGroup, CompareNum, compare, RANK2, Comparator_code, " +
                  $"RANK3ReprocessFlag, UseAllCompFlag, Range_flag, DataReleaseId, PartYearDataFlag FROM {options.TableName}";

        using var adapter = new SqlDataAdapter(sql, connection);
        var dataset = new DataSet();
        adapter.Fill(dataset);
        
        return dataset;
    }

    private Task<List<ComparatorSet>> BuildComparatorSets(DataTable table)
    {
        // get a list of unique URNs
        var baseSchoolUrns = table.AsEnumerable().Select(row => row.Field<int>("URN")).Distinct().ToList();
        var sets = new List<ComparatorSet>();
        
        foreach (var urn in baseSchoolUrns)
        {
            var set = new ComparatorSet
            {
                Urn = urn
            };

            // Default Pupil
            set.DefaultPupil = table.AsEnumerable()
                .Where(row => row.Field<int>("URN") == urn
                              && row.Field<string>("PeerGroup") == "Default"
                              && row.Field<string>("CostGroup") == "Pupil")
                .Select(row => new ComparatorEntry
                {
                    ComparatorCode = row.Field<int>("Comparator_code"),
                    Compare = row.Field<bool>("compare"),
                    CompareNum = row.Field<int>("CompareNum"),
                    CostGroup = row.Field<string>("CostGroup"),
                    DataReleaseId = row.Field<int>("DataReleaseId"),
                    PartYearDataFlag = row.Field<int>("PartYearDataFlag"),
                    PeerGroup = row.Field<string>("PeerGroup"),
                    RangeFlag = row.Field<int>("Range_flag"),
                    Rank2 = row.Field<int>("RANK2"),
                    Rank3 = row.Field<int>("RANK3"),
                    ReprocessFlag = row.Field<int>("ReprocessFlag"),
                    UkPrnUrn1 = row.Field<string>("UKPRN_URN1"),
                    UkPrnUrn2 = row.Field<string>("UKPRN_URN2"),
                    UkPrnUrnCg = row.Field<string>("UKPRN_URN_CG"),
                    UseAllCompFlag = row.Field<int>("UseAllCompFlag")
                    
                }).ToList();
            
            // Default Area
            set.DefaultArea = table.AsEnumerable()
                .Where(row => row.Field<int>("URN") == urn
                              && row.Field<string>("PeerGroup") == "Default"
                              && row.Field<string>("CostGroup") == "Area")
                .Select(row => new ComparatorEntry
                {
                    ComparatorCode = row.Field<int>("Comparator_code"),
                    Compare = row.Field<bool>("compare"),
                    CompareNum = row.Field<int>("CompareNum"),
                    CostGroup = row.Field<string>("CostGroup"),
                    DataReleaseId = row.Field<int>("DataReleaseId"),
                    PartYearDataFlag = row.Field<int>("PartYearDataFlag"),
                    PeerGroup = row.Field<string>("PeerGroup"),
                    RangeFlag = row.Field<int>("Range_flag"),
                    Rank2 = row.Field<int>("RANK2"),
                    Rank3 = row.Field<int>("RANK3"),
                    ReprocessFlag = row.Field<int>("ReprocessFlag"),
                    UkPrnUrn1 = row.Field<string>("UKPRN_URN1"),
                    UkPrnUrn2 = row.Field<string>("UKPRN_URN2"),
                    UkPrnUrnCg = row.Field<string>("UKPRN_URN_CG"),
                    UseAllCompFlag = row.Field<int>("UseAllCompFlag")
                    
                }).ToList();

            // Mixed Pupil
            set.DefaultArea = table.AsEnumerable()
                .Where(row => row.Field<int>("URN") == urn
                              && row.Field<string>("PeerGroup") == "Mixed"
                              && row.Field<string>("CostGroup") == "Pupil")
                .Select(row => new ComparatorEntry
                {
                    ComparatorCode = row.Field<int>("Comparator_code"),
                    Compare = row.Field<bool>("compare"),
                    CompareNum = row.Field<int>("CompareNum"),
                    CostGroup = row.Field<string>("CostGroup"),
                    DataReleaseId = row.Field<int>("DataReleaseId"),
                    PartYearDataFlag = row.Field<int>("PartYearDataFlag"),
                    PeerGroup = row.Field<string>("PeerGroup"),
                    RangeFlag = row.Field<int>("Range_flag"),
                    Rank2 = row.Field<int>("RANK2"),
                    Rank3 = row.Field<int>("RANK3"),
                    ReprocessFlag = row.Field<int>("ReprocessFlag"),
                    UkPrnUrn1 = row.Field<string>("UKPRN_URN1"),
                    UkPrnUrn2 = row.Field<string>("UKPRN_URN2"),
                    UkPrnUrnCg = row.Field<string>("UKPRN_URN_CG"),
                    UseAllCompFlag = row.Field<int>("UseAllCompFlag")
                    
                }).ToList();
            
            // Mixed Area
            set.DefaultArea = table.AsEnumerable()
                .Where(row => row.Field<int>("URN") == urn
                              && row.Field<string>("PeerGroup") == "Mixed"
                              && row.Field<string>("CostGroup") == "Area")
                .Select(row => new ComparatorEntry
                {
                    ComparatorCode = row.Field<int>("Comparator_code"),
                    Compare = row.Field<bool>("compare"),
                    CompareNum = row.Field<int>("CompareNum"),
                    CostGroup = row.Field<string>("CostGroup"),
                    DataReleaseId = row.Field<int>("DataReleaseId"),
                    PartYearDataFlag = row.Field<int>("PartYearDataFlag"),
                    PeerGroup = row.Field<string>("PeerGroup"),
                    RangeFlag = row.Field<int>("Range_flag"),
                    Rank2 = row.Field<int>("RANK2"),
                    Rank3 = row.Field<int>("RANK3"),
                    ReprocessFlag = row.Field<int>("ReprocessFlag"),
                    UkPrnUrn1 = row.Field<string>("UKPRN_URN1"),
                    UkPrnUrn2 = row.Field<string>("UKPRN_URN2"),
                    UkPrnUrnCg = row.Field<string>("UKPRN_URN_CG"),
                    UseAllCompFlag = row.Field<int>("UseAllCompFlag")
                    
                }).ToList();
            
            sets.Add(set);
        }

        return Task.FromResult(sets);
    }
}