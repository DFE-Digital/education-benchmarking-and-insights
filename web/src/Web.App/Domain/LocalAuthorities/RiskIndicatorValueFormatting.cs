namespace Web.App.Domain.LocalAuthorities;


/// <summary>
/// Domain format discriminators set upstream in the data pipeline.
/// Aligns with <see cref="RisksMetrics.RiskIndicatorValueFormatting"/> to dictate
/// how <see cref="RisksMetrics.RiskIndicatorValue"/> is formatted in the UI.
/// </summary>
/// <remarks>
/// Example usage in <see cref="ViewModels.RiskMetricViewModel.FormatMetricValue"/>
/// to determine string formatting rules.
/// </remarks>
public static class RiskIndicatorValueFormatting
{
    public const string Percentage = nameof(Percentage);
    public const string Boolean = nameof(Boolean);
    public const string Decimal = nameof(Decimal);
    public const string String = nameof(String);
}
