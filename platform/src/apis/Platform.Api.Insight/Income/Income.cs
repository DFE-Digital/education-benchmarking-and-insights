using System.Linq;

namespace Platform.Api.Insight.Income;

public static class IncomeCategories
{
    public const string GrantFunding = nameof(GrantFunding);
    public const string SelfGenerated = nameof(SelfGenerated);
    public const string DirectRevenueFinancing = nameof(DirectRevenueFinancing);

    public static readonly string[] All =
    {
        GrantFunding,
        SelfGenerated,
        DirectRevenueFinancing
    };

    public static bool IsValid(string? category) => All.Any(a => a == category);
}

public static class IncomeDimensions
{
    public const string Actuals = nameof(Actuals);
    public const string PerUnit = nameof(PerUnit);
    public const string PercentIncome = nameof(PercentIncome);
    public const string PercentExpenditure = nameof(PercentExpenditure);

    public static readonly string[] All =
    {
        Actuals,
        PerUnit,
        PercentIncome,
        PercentExpenditure
    };

    public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
}