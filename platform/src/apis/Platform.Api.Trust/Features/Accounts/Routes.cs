using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.Accounts;

[ExcludeFromCodeCoverage]
public static class Routes
{

    public const string ExpenditureCollection = "trusts/accounts/expenditure";
    public const string ExpenditureSingle = $"trusts/{Constants.CompanyNumberParam}/accounts/expenditure";
    public const string ExpenditureHistory = $"trusts/{Constants.CompanyNumberParam}/accounts/expenditure/history";
    public const string IncomeHistory = $"trusts/{Constants.CompanyNumberParam}/accounts/income/history";
    public const string BalanceCollection = "trusts/accounts/balance";
    public const string BalanceSingle = $"trusts/{Constants.CompanyNumberParam}/accounts/balance";
    public const string BalanceHistory = $"trusts/{Constants.CompanyNumberParam}/accounts/balance/history";
}