using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Platform.Domain;

namespace Platform.Api.Insight.Features.Schools;

public class SchoolsModule(ISchoolsDb db) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/schools")
            .WithTags("Schools finances")
            .IncludeInOpenApi();

        group.MapGet("/", QuerySchools)
            .WithName(nameof(QuerySchools));

        group.MapGet("/expenditure", QuerySchoolsExpenditure)
            .WithName(nameof(QuerySchoolsExpenditure));
    }

    public async Task<Ok<IEnumerable<FinancesResponseModel>>> QuerySchools([AsParameters] SchoolsQueryParameters parameters)
    {
        var finances = await db.Finances(parameters);

        return TypedResults.Ok(finances);
    }

    public async Task<Ok<IEnumerable<SchoolExpenditureResponseModel>>> QuerySchoolsExpenditure([AsParameters] SchoolsQueryParameters parameters)
    {
        var expenditure = await db.Expenditure(parameters);
        return TypedResults.Ok(expenditure);
    }
}