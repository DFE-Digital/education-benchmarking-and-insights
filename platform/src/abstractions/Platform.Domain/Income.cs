using System.Linq;

namespace Platform.Domain;

public static class IncomeCategories
{
    public const string GrantFunding = nameof(GrantFunding);
    public const string SelfGenerated = nameof(SelfGenerated);
    public const string DirectRevenueFinancing = nameof(DirectRevenueFinancing);

    public static readonly string[] All =
    [
        GrantFunding,
        SelfGenerated,
        DirectRevenueFinancing
    ];

    public static bool IsValid(string? category) => All.Any(a => a == category);
}

public static class IncomeDimensions
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