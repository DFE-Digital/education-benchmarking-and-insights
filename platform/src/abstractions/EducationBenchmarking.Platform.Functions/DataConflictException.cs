using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Functions;

[ExcludeFromCodeCoverage]
public sealed class DataConflictException : Exception
{
    public ConflictData Details { get; }
    
    public DataConflictException(string id, string type, string createdBy, in DateTimeOffset createdAt, string updatedBy, DateTimeOffset? updatedAt)
        : this(new ConflictData
        {
            Id = id,
            Type = type,
            CreatedBy = createdBy,
            CreatedAt = createdAt,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            ConflictReason = ConflictData.Reason.Timestamp,
        })
    {
            
    }
    

    public DataConflictException(ConflictData data)
        : base($"Unable to create {data.Type} with {data.Id}. {data.Type}:{data.Id} already exists, originally created at {data.CreatedAt:G}")
    {
        data.Message = Message;
        Details = data;
    }
}

[ExcludeFromCodeCoverage]
public class ConflictData
{
    public enum Reason
    {
        Timestamp
    }
        
    public string Id { get; set; }
    public string Type { get; set; }
    public string CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Reason ConflictReason { get; set; }
    public string Message { get; set; }
}
