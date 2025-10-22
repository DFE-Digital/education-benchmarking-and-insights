namespace Platform.Api.Trust.Features.Accounts;

public static class Routes
{
    public const string ExpenditureCollection = "trusts/accounts/expenditure";
    public const string ExpenditureSingle = "trusts/{companyNumber}/accounts/expenditure";
    public const string ExpenditureHistory = "rusts/{companyNumber}/accounts/expenditure/history";
    public const string IncomeHistory = "trusts/{companyNumber}/accounts/income/history";
    public const string BalanceCollection = "trusts/accounts/balance";
    public const string BalanceSingle = "trusts/{companyNumber}/accounts/balance";
    public const string BalanceHistory = "trusts/{companyNumber}/accounts/balance/history";
}