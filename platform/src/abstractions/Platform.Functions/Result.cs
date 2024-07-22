using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Functions.Extensions;
namespace Platform.Functions;

public enum ResultStatusCode
{
    Created,
    Updated,
    Conflict
}

[ExcludeFromCodeCoverage]
public abstract class Result(ResultStatusCode status)
{
    public ResultStatusCode Status => status;

    public abstract IActionResult CreateResponse();

    public abstract Task<HttpResponseData> CreateResponse(HttpRequestData req);
}

[ExcludeFromCodeCoverage]
public class CreatedResult<T>(T content, string location) : Result(ResultStatusCode.Created)
{
    public object? Content { get; } = content;
    public string Location { get; } = location;

    public override CreatedResult CreateResponse() => new(Location, Content);

    public override async Task<HttpResponseData> CreateResponse(HttpRequestData req)
    {
        var response = Content == null ? req.CreateResponse(HttpStatusCode.Created) : await req.CreateJsonResponseAsync(Content, HttpStatusCode.Created);
        if (!string.IsNullOrWhiteSpace(Location))
        {
            response.Headers.Add(nameof(Location), Location);
        }

        return response;
    }
}

[ExcludeFromCodeCoverage]
public class UpdatedResult() : Result(ResultStatusCode.Updated)
{
    public override NoContentResult CreateResponse() => new();

    public override Task<HttpResponseData> CreateResponse(HttpRequestData req) => Task.FromResult(req.CreateResponse(HttpStatusCode.NoContent));
}

[ExcludeFromCodeCoverage]
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

    public override ConflictObjectResult CreateResponse() => new(this);

    public override async Task<HttpResponseData> CreateResponse(HttpRequestData req)
    {
        var response = await req.CreateJsonResponseAsync(this, HttpStatusCode.Conflict);
        return response;
    }
}