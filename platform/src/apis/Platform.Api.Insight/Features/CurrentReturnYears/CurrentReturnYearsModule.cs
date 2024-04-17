using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace Platform.Api.Insight.Features.CurrentReturnYears;

public class CurrentReturnYearsModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/current-return-years")
            .WithTags("Current Return Years")
            .IncludeInOpenApi();

        group.MapGet("/", GetCurrentReturnYears)
            .WithName(nameof(GetCurrentReturnYears));
    }

    private Ok<CurrentReturnYearsResponseModel> GetCurrentReturnYears(IOptions<DbOptions> options)
    {
        return TypedResults.Ok(new CurrentReturnYearsResponseModel
        {
            Aar = options.Value.AarLatestYear,
            Cfr = options.Value.CfrLatestYear
        });
    }
}