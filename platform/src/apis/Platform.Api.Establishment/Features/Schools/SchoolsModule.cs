using Carter;
using Carter.ModelBinding;
using Carter.OpenApi;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Platform.Api.Establishment.Search;
using Platform.Domain;

namespace Platform.Api.Establishment.Features.Schools;

public class SchoolsModule(ISearchService<SchoolResponseModel> search, ISchoolsDb db) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/schools")
            .WithTags("Schools")
            .IncludeInOpenApi();

        group.MapGet("/{id:int}", GetSchoolById)
            .WithName(nameof(GetSchoolById));
        
        group.MapGet("/", QuerySchools)
            .WithName(nameof(QuerySchools));
        
        group.MapPost("/search", SearchSchools)
            .WithName(nameof(SearchSchools));
        
        group.MapPost("/suggest", SuggestSchools)
            .WithName(nameof(SuggestSchools));
    }
    
    private async Task<Results<Ok<SchoolResponseModel>, NotFound>> GetSchoolById(int id)
    {
        var school = await db.Get(id);

        return school == null
            ? TypedResults.NotFound()
            : TypedResults.Ok(school);
    }
    
    private async Task<Ok<PagedResponseModel<SchoolResponseModel>>> QuerySchools([AsParameters] SchoolQueryParameters parameters)
    {
        var result = await db.Query(parameters);
        return TypedResults.Ok(result);
    }
    private Ok<SearchResponseModel<SchoolResponseModel>> SearchSchools(PostSearchRequestModel model)
    { 
        return TypedResults.Ok(new SearchResponseModel<SchoolResponseModel>());
    }

    private async Task<Results<Ok<SuggestResponseModel<SchoolResponseModel>>, BadRequest<List<ValidationFailure>>>>
        SuggestSchools(HttpRequest req, PostSuggestRequestModel model)
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