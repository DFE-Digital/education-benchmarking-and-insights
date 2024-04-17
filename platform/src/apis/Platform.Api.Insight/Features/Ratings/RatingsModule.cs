using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Platform.Api.Insight.Features.Ratings;

public class RatingsModule(IRatingsDb db) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/ratings")
            .WithTags("Ratings")
            .IncludeInOpenApi();

        group.MapGet("/", QueryRatings)
            .WithName(nameof(QueryRatings));
    }
    
    private async Task<Ok<IEnumerable<RatingResponseModel>>> QueryRatings([AsParameters] RatingsQueryParameters parameters)
    {
        var ratings = await db.Get(parameters);

        return TypedResults.Ok(ratings);
    }
}