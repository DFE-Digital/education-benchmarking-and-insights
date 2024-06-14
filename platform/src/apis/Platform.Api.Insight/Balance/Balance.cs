using System.Linq;

namespace Platform.Api.Insight.Balance;

public static class BalanceDimensions
{
    public const string Actuals = nameof(Actuals);
    public const string PerUnit = nameof(PerUnit);
    public const string PercentIncome = nameof(PercentIncome);
    public const string PercentExpenditure = nameof(PercentExpenditure);

    public static readonly string[] All = new[]
    {
        Actuals,
        PerUnit,
        PercentIncome,
        PercentExpenditure
    };

    public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
}