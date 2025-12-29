namespace Platform.Api.School.Features.Accounts;

public static class Routes
{
    public const string ItSpending = "schools/accounts/it-spending";
    public const string IncomeHistory = $"schools/{Constants.UrnParam}/accounts/income/history";
    public const string Income = $"schools/{Constants.UrnParam}/accounts/income";
    public const string BalanceHistory = $"schools/{Constants.UrnParam}/accounts/balance/history";
    public const string Balance = $"schools/{Constants.UrnParam}/accounts/balance";
}