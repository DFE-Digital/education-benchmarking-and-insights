using System;
using System.Diagnostics.CodeAnalysis;
using JsonSubTypes;
using Newtonsoft.Json;

namespace Platform.Domain.Messages;

[ExcludeFromCodeCoverage]
public record PipelineFinishMessage
{
    public Guid JobId { get; set; } = Guid.NewGuid();
}

[ExcludeFromCodeCoverage]
public record PipelineStartMessage
{
    public Guid JobId { get; set; } = Guid.NewGuid();
    public string? Type { get; set; } // Pipeline job type : default / comparator-set / custom-data
    public string? RunType { get; set; }  // Data context : default / custom
    public string? RunId { get; set; } // year or id for comparator-set / custom-data
    public int? Year { get; set; } // Needed for when custom data or comparator set
    public string? URN { get; set; }
    public Payload? Payload { get; set; } // null for default
}

[JsonConverter(typeof(JsonSubtypes), "Kind")]
public abstract record Payload
{
    [JsonProperty("Kind")]
    public virtual string? Kind { get; }
};

public record ComparatorSetPayload : Payload
{
    public override string Kind => nameof(ComparatorSetPayload);
    public string[] Set { get; set; } = [];
}

public record CustomDataPayload : Payload
{
    public override string Kind => nameof(CustomDataPayload);
    //TBC
}