namespace EducationBenchmarking.Platform.Domain;

public class DbResult
{
    public enum ResultStatus
    {
        Created,
        Updated
    }
    
    public ResultStatus Status { get; set; }
    public object? Content { get; set; }
}