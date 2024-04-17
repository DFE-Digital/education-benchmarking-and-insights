using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Platform.Domain;

namespace Platform.Api.Insight.Features.SchoolFinances;

public class SchoolFinancesModule(ISchoolFinancesDb db) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/schools")
            .WithTags("School finances")
            .IncludeInOpenApi();

        group.MapGet("/{urn}", GetSchoolFiancesById)
            .WithName(nameof(GetSchoolFiancesById));

        group.MapGet("/{urn}/balance/history", GetSchoolBalanceHistory)
            .WithName(nameof(GetSchoolBalanceHistory));

        group.MapGet("/{urn}/income/history", GetSchoolIncomeHistory)
            .WithName(nameof(GetSchoolIncomeHistory));

        group.MapGet("/{urn}/expenditure/history", GetSchoolExpenditureHistory)
            .WithName(nameof(GetSchoolExpenditureHistory));
    }

    public async Task<Results<Ok<FinancesResponseModel>, NotFound>> GetSchoolFiancesById(string urn)
    {
        var finances = await db.Get(urn);

        return finances == null
            ? TypedResults.NotFound()
            : TypedResults.Ok(finances);
    }

    private async Task<Ok<IEnumerable<BalanceResponseModel>>> GetSchoolBalanceHistory(string urn,
        [AsParameters] SchoolHistoryQueryParameters parameters)
    {
        var balanceHistory = await db.GetBalanceHistory(urn, parameters);
        return TypedResults.Ok(balanceHistory);
    }

    public async Task<Ok<IEnumerable<IncomeResponseModel>>> GetSchoolIncomeHistory(string urn,
        [AsParameters] SchoolHistoryQueryParameters parameters)
    {
        var incomeHistory = await db.GetIncomeHistory(urn, parameters);
        return TypedResults.Ok(incomeHistory);
    }

    public async Task<Ok<IEnumerable<ExpenditureResponseModel>>> GetSchoolExpenditureHistory(string urn,
        [AsParameters] SchoolHistoryQueryParameters parameters)
    {
        var expenditureHistory = await db.GetExpenditureHistory(urn, parameters);
        return TypedResults.Ok(expenditureHistory);
    }
}