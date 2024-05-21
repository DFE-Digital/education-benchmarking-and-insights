using System.Linq;

namespace Platform.Domain;

public static class BalanceDimensions
{
    public const string Actuals = nameof(Actuals);
    public const string PoundPerPupil = nameof(PoundPerPupil);
    public const string PercentIncome = nameof(PercentIncome);
    public const string PercentExpenditure = nameof(PercentExpenditure);

    public static readonly string[] All =
    [
        Actuals,
        PoundPerPupil,
        PercentIncome,
        PercentExpenditure
    ];

    public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
}