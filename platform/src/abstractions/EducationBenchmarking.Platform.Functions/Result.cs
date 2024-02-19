using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Platform.Functions;

public enum ResultStatusCode
{
    Created,
    Updated,
    Conflict
}

public abstract class Result(ResultStatusCode status)
{
    public ResultStatusCode Status => status;

    public abstract IActionResult CreateResponse();
}

public class CreatedResult<T>(T content, string location) : Result(ResultStatusCode.Created)
{
    public object? Content { get; } = content;
    public string Location { get; } = location;

    public override CreatedResult CreateResponse()
    {
        return new CreatedResult(Location, Content);
    }
}

public class UpdatedResult() : Result(ResultStatusCode.Updated)
{
    public override NoContentResult CreateResponse()
    {
        return new NoContentResult();
    }
}

public class DataConflictResult() : Result(ResultStatusCode.Conflict)
{
    public enum Reason
    {
        Timestamp
    }

    public string? Id { get; set; }
    public string? Type { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Reason ConflictReason { get; set; }

    public override ConflictObjectResult CreateResponse()
    {
        return new ConflictObjectResult(this);
    }
}