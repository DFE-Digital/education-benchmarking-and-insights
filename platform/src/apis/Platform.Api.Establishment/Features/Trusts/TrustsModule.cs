using Carter;
using Carter.ModelBinding;
using Carter.OpenApi;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Platform.Api.Establishment.Search;

namespace Platform.Api.Establishment.Features.Trusts;

public class TrustsModule(ISearchService<TrustResponseModel> search, ITrustsDb db) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/trusts")
            .WithTags("Trusts")
            .IncludeInOpenApi();

        group.MapGet("/{id:int}", GetTrustById)
            .WithName(nameof(GetTrustById));

        group.MapGet("/", QueryTrusts)
            .WithName(nameof(QueryTrusts));

        group.MapPost("/search", SearchTrusts)
            .WithName(nameof(SearchTrusts));

        group.MapPost("/suggest", SuggestTrusts)
            .WithName(nameof(SuggestTrusts));
    }

    private async Task<Results<Ok<TrustResponseModel>, NotFound>> GetTrustById(int id)
    {
        var trust = await db.Get(id);

        return trust == null
            ? TypedResults.NotFound()
            : TypedResults.Ok(trust);
    }

    private Ok<IEnumerable<TrustResponseModel>> QueryTrusts()
    {
        return TypedResults.Ok<IEnumerable<TrustResponseModel>>(Array.Empty<TrustResponseModel>());
    }

    private Ok<SearchResponseModel<TrustResponseModel>> SearchTrusts(PostSearchRequestModel model)
    {
        return TypedResults.Ok(new SearchResponseModel<TrustResponseModel>());
    }

    private async Task<Results<Ok<SuggestResponseModel<TrustResponseModel>>, BadRequest<List<ValidationFailure>>>>
        SuggestTrusts(HttpRequest req, PostSuggestRequestModel model)
    {
        var result = req.Validate(model);
        if (!result.IsValid)
        {
            return TypedResults.BadRequest(result.Errors);
        }

        var suggestions = await search.SuggestAsync(model);
        return TypedResults.Ok(suggestions);
    }
}