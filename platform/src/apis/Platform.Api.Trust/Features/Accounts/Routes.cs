namespace Platform.Api.Trust.Features.Accounts;

public static class Routes
{
    public const string ExpenditureCollection = "trusts/accounts/expenditure";
    public const string ExpenditureSingle = "trusts/{companyNumber:regex(^\\d{{8}}$)}/accounts/expenditure";
    public const string ExpenditureHistory = "rusts/{companyNumber:regex(^\\d{{8}}$)}/accounts/expenditure/history";
    public const string IncomeHistory = "trusts/{companyNumber:regex(^\\d{{8}}$)}/accounts/income/history";
    public const string BalanceCollection = "trusts/accounts/balance";
    public const string BalanceSingle = "trusts/{companyNumber:regex(^\\d{{8}}$)}/accounts/balance";
    public const string BalanceHistory = "trusts/{companyNumber:regex(^\\d{{8}}$)}/accounts/balance/history";
}