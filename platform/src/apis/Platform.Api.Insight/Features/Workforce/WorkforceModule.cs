using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Platform.Api.Insight.Features.Workforce;

public class WorkforceModule(IWorkforceDb db) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/workforce")
            .WithTags("Workforce")
            .IncludeInOpenApi();

        group.MapGet("{urn}/history", GetWorkForceHistory)
            .WithName(nameof(GetWorkForceHistory));

        group.MapGet("/", QueryWorkForce)
            .WithName(nameof(QueryWorkForce));
    }

    public async Task<Ok<IEnumerable<WorkforceResponseModel>>> GetWorkForceHistory(string urn, [AsParameters] WorkforceHistoryQueryParameters parameters)
    {
        //TODO: Add validation for dimension
        var workforce = await db.GetHistory(urn, parameters);
        return TypedResults.Ok(workforce);
    }

    public async Task<Ok<IEnumerable<WorkforceResponseModel>>> QueryWorkForce([AsParameters] WorkforceQueryParameters parameters)
    {
        //TODO: Add validation for urns, category and dimension
        var workforce = await db.Get(parameters);
        return TypedResults.Ok(workforce);
    }
}