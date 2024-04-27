using System;

namespace Platform.Domain;

public enum JobKind
{
    Default,
    Custom
}

public class PipelineJobRequestModel
{
    public string JobId => Kind == JobKind.Custom ? $"{Kind.ToString().ToLower()}-{Year}-{RequestId}" : $"{Kind.ToString().ToLower()}-{Year}-";
    public JobKind? Kind { get; set; }
    public int Year { get; set; }
    public Guid? RequestId { get; set; }
}