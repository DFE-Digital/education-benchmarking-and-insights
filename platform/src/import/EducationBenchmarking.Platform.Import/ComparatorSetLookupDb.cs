using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Domain.DataObjects;
using EducationBenchmarking.Platform.Domain.Requests;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace EducationBenchmarking.Platform.Import;

public interface IComparatorSetLookupDb
{
    Task<EducationBenchmarking.Platform.Domain.Responses.ComparatorSetLookup?> ComparatorSetLookup(string urn, string peerGroup, string costGroup);
    Task<Result> UpsertComparatorSetLookup(string urn, ComparatorSetLookupRequest request);
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

    public async Task<EducationBenchmarking.Platform.Domain.Responses.ComparatorSetLookup?> ComparatorSetLookup(string urn, string peerGroup, string costGroup)
    {
        ArgumentNullException.ThrowIfNull(_options.ComparatorSetLookupCollectionName);

        var response =
            await ReadItemStreamAsync(_options.ComparatorSetLookupCollectionName, urn, peerGroup + "-" + costGroup);
        return response.IsSuccessStatusCode ? Domain.Responses.ComparatorSetLookup.Create(response.Content.FromJson<ComparatorSetLookupDataObject>()) : null;
    }
    
    public async Task<Result> UpsertComparatorSetLookup(string urn, ComparatorSetLookupRequest request)
    {
        ArgumentNullException.ThrowIfNull(_options.ComparatorSetLookupCollectionName);
        var response = await ReadItemStreamAsync(_options.ComparatorSetLookupCollectionName, urn, request.PeerGroup + "-" + request.CostGroup);
        if (!response.IsSuccessStatusCode)
        {
            return await Create(urn, request);
        }

        var existing = response.Content.FromJson<ComparatorSetLookupDataObject>();
        return await Update(request, existing);
    }

    private async Task<Result> Create(string urn, ComparatorSetLookupRequest request)
    {
        ArgumentNullException.ThrowIfNull(_options.ComparatorSetLookupCollectionName);

        var lookupSet = new ComparatorSetLookupDataObject
        {
            Id = urn,
            Urn = urn,
            PartitionKey = request.PeerGroup + "-" + request.CostGroup,
            PeerGroup = request.PeerGroup,
            CostGroup = request.CostGroup,
            Entries = request.Entries.Select(e =>
                new EducationBenchmarking.Platform.Domain.DataObjects.ComparatorSetLookupEntry
                {
                    ComparatorCode = e.ComparatorCode,
                    Compare = e.Compare,
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
        await UpsertItemAsync(_options.ComparatorSetLookupCollectionName, lookupSet, new PartitionKey(urn));

        return new CreatedResult<ComparatorSetLookupDataObject>(lookupSet, string.Empty);
    }

    private async Task<Result> Update(ComparatorSetLookupRequest request, ComparatorSetLookupDataObject existing)
    {
        ArgumentNullException.ThrowIfNull(_options.ComparatorSetLookupCollectionName);

        existing.Entries = request.Entries.Select(e =>
            new EducationBenchmarking.Platform.Domain.DataObjects.ComparatorSetLookupEntry
            {
                ComparatorCode = e.ComparatorCode,
                Compare = e.Compare,
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
            }).ToArray();
        
        await UpsertItemAsync(_options.ComparatorSetLookupCollectionName, existing, new PartitionKey(existing.PartitionKey));

        return new UpdatedResult();
    }
}