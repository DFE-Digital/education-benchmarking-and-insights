namespace Platform.Api.School.Features.Accounts;

public static class Routes
{
    public const string ItSpending = "schools/accounts/it-spending";
    public const string IncomeHistory = $"schools/{Constants.UrnParam}/accounts/income/history";
    public const string Income = $"schools/{Constants.UrnParam}/accounts/income";
    public const string IncomeComparatorSetAverageHistory = $"schools/{Constants.UrnParam}/comparator-set-average/accounts/income/history";
    public const string IncomeNationalAverageHistory = "schools/national-average/accounts/income/history/";
    public const string BalanceHistory = $"schools/{Constants.UrnParam}/accounts/balance/history";
    public const string Balance = $"schools/{Constants.UrnParam}/accounts/balance";
    public const string ExpenditureUserDefined = $"schools/{Constants.UrnParam}/user-defined/{{identifier}}/accounts/expenditure";
    public const string ExpenditureSingle = $"schools/{Constants.UrnParam}/accounts/expenditure";
    public const string ExpenditureComparatorSetAverageHistory = $"schools/{Constants.UrnParam}/comparator-set-average/accounts/expenditure/history";
    public const string ExpenditureHistory = $"schools/{Constants.UrnParam}/accounts/expenditure/history";
    public const string ExpenditureNationalAverageHistory = "schools/national-average/accounts/expenditure/history/";
    public const string ExpenditureCollection = "schools/accounts/expenditure";
}