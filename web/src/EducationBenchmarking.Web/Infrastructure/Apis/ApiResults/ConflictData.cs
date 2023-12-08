namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class ConflictData
{
    public enum Reason
    {
        Timestamp,
        AlreadyExists,
        UpnAlreadyExists,
        DuplicateFormType
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