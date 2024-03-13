using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Import.Abstractions;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Import.Db;

public interface IComparatorSetLookupDb
{
    Task UpsertComparatorSetLookup(string urn, ComparatorSetLookupRequest request);
}

[ExcludeFromCodeCoverage]
public record ComparatorSetLookupDbOptions : CosmosDatabaseOptions
{
    [Required] public string? ComparatorSetLookupCollectionName { get; set; }
}

public class ComparatorSetLookupDb : CosmosDatabase, IComparatorSetLookupDb
{
    private readonly ComparatorSetLookupDbOptions _options;
    
    public ComparatorSetLookupDb(IOptions<ComparatorSetLookupDbOptions> options) : base(options.Value)
    {
        _options = options.Value;
    }
    
    public async Task UpsertComparatorSetLookup(string urn, ComparatorSetLookupRequest request)
    {
        ArgumentNullException.ThrowIfNull(_options.ComparatorSetLookupCollectionName);

        var item = CreateDataObject(request);
        await UpsertItemAsync(_options.ComparatorSetLookupCollectionName, item, new PartitionKey(item.PartitionKey));
    }

    private ComparatorSetLookupDataObject CreateDataObject(ComparatorSetLookupRequest request)
    {
        return new ComparatorSetLookupDataObject
        {
            Id = request.Urn,
            PartitionKey = request.PeerGroup + "-" + request.CostGroup,
            PeerGroup = request.PeerGroup,
            CostGroup = request.CostGroup,
            Entries = request.Entries.Select(e =>
                new ComparatorSetLookupEntryDataObject
                {
                    ComparatorCode = e.ComparatorCode,
                    Compare = e.Compare.ToLower().Trim() == "true",
                    CompareNum = e.CompareNum,
                    DataReleaseId = e.DataReleaseId,
                    PartYearDataFlag = e.PartYearDataFlag,
                    RangeFlag = e.RangeFlag,
                    Rank2 = e.Rank2,
                    Rank3 = e.Rank3,
                    ReprocessFlag = e.ReprocessFlag,
                    UkPrnUrn1 = e.UkPrnUrn1,
                    UkPrnUrn2 = e.UkPrnUrn2,
                    UkPrnUrnCg = e.UkPrnUrnCg,
                    UseAllCompFlag = e.UseAllCompFlag
                }).ToArray()
        };
    }
}