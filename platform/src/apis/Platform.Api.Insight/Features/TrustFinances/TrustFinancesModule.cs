using Carter;
using Carter.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Platform.Domain;

namespace Platform.Api.Insight.Features.TrustFinances;

public class TrustFinancesModule(ITrustFinancesDb db): CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/trusts")
            .WithTags("Trust finances")
            .IncludeInOpenApi();

        group.MapGet("{companyNumber}/balance/history", GetTrustBalanceHistory)
            .WithName(nameof(GetTrustBalanceHistory));
        
        group.MapGet("{companyNumber}/income/history", GetTrustIncomeHistory)
            .WithName(nameof(GetTrustIncomeHistory));
        
        group.MapGet("{companyNumber}/expenditure/history", GetTrustExpenditureHistory)
            .WithName(nameof(GetTrustExpenditureHistory));
    }
    
    private async Task<Ok<IEnumerable<BalanceResponseModel>>> GetTrustBalanceHistory(string companyNumber, [AsParameters] TrustHistoryQueryParameters parameters)
    {
                var balanceHistory = await db.GetBalanceHistory(companyNumber, parameters);
                return TypedResults.Ok(balanceHistory);
    }
    
    public async Task<Ok<IEnumerable<IncomeResponseModel>>> GetTrustIncomeHistory(string companyNumber, [AsParameters] TrustHistoryQueryParameters parameters)
    {
        var incomeHistory = await db.GetIncomeHistory(companyNumber, parameters);
        return TypedResults.Ok(incomeHistory);
    }
    public async Task<Ok<IEnumerable<ExpenditureResponseModel>>> GetTrustExpenditureHistory(string companyNumber, [AsParameters] TrustHistoryQueryParameters parameters)
    {
        var expenditureHistory = await db.GetExpenditureHistory(companyNumber, parameters);
        return TypedResults.Ok(expenditureHistory);
    }
}