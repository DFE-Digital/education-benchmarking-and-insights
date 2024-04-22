using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

public class LocalAuthoritiesModule : CarterModule
{
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        
        var group = app.MapGroup("/local-authorities")
            .WithTags("Local Authorities")
            .IncludeInOpenApi();

        group.MapGet("/{id:int}", GetLocalAuthorityById)
            .WithName(nameof(GetLocalAuthorityById));
        
        group.MapGet("/", QueryLocalAuthorities)
            .WithName(nameof(QueryLocalAuthorities));
        
        group.MapPost("/search", SearchLocalAuthorities)
            .WithName(nameof(SearchLocalAuthorities));
        
        group.MapPost("/suggest", SuggestLocalAuthorities)
            .WithName(nameof(SuggestLocalAuthorities));
    }

    private static Results<Ok,NotFound> GetLocalAuthorityById(int id) => TypedResults.Ok();
    private static Ok QueryLocalAuthorities () => TypedResults.Ok();
    private static Ok SearchLocalAuthorities () => TypedResults.Ok();
    private static Ok SuggestLocalAuthorities () => TypedResults.Ok();
}