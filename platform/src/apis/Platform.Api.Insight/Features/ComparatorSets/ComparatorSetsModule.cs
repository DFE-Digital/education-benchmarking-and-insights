using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Platform.Api.Insight.Features.ComparatorSets;

public class ComparatorSetsModule(IComparatorSetsDb db) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/comparator-sets")
            .WithTags("Comparator Sets")
            .IncludeInOpenApi();

        group.MapGet("/{urn:int}", GetComparatorSetById)
            .WithName(nameof(GetComparatorSetById));
    }

    private async Task<Results<Ok<ComparatorSetResponseModel>, NotFound>> GetComparatorSetById(int urn)
    {
        var set = await db.Get(urn);

        return set == null
            ? TypedResults.NotFound()
            : TypedResults.Ok(set);
    }
}