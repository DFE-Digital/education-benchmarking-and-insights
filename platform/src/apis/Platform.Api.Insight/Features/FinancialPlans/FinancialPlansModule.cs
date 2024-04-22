using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Platform.Api.Insight.Features.FinancialPlans;

public class FinancialPlansModule(IFinancialPlansDb db) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/financial-plans")
            .WithTags("Financial Plans")
            .IncludeInOpenApi();

        group.MapGet("/{urn}/{year:int}", GetFinancialPlanByUrnYear)
            .WithName(nameof(GetFinancialPlanByUrnYear));

        group.MapGet("/{urn}", GetFinancialPlansByUrn)
            .WithName(nameof(GetFinancialPlansByUrn));

        group.MapPut("/{urn}/{year:int}", UpdateFinancialPlan)
            .WithName(nameof(UpdateFinancialPlan));

        group.MapDelete("/{urn}/{year:int}", DeleteFinancialPlan)
            .WithName(nameof(DeleteFinancialPlan));
    }

    private async Task<Results<Ok<FinancialPlanResponseModel>, NotFound>> GetFinancialPlanByUrnYear(string urn,
        int year)
    {
        var plan = await db.SingleFinancialPlan(urn, year);

        return plan == null
            ? TypedResults.NotFound()
            : TypedResults.Ok(plan);
    }

    private async Task<Ok<IEnumerable<FinancialPlanResponseModel>>> GetFinancialPlansByUrn(string urn)
    {
        var plans = await db.QueryFinancialPlan(urn);
        return TypedResults.Ok(plans);
    }

    private async Task<Ok> UpdateFinancialPlan(string urn, int year, FinancialPlanRequestModel model)
    {
        await db.UpsertFinancialPlan(urn, year, model);
        return TypedResults.Ok();
    }

    private async Task<Results<Ok, NotFound>> DeleteFinancialPlan(string urn, int year)
    {
        var plan = await db.SingleFinancialPlan(urn, year);
        if (plan == null)
        {
            return TypedResults.NotFound();
        }

        await db.DeleteFinancialPlan(plan);
        return TypedResults.Ok();
    }
}