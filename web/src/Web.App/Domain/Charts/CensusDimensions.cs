using Web.App.Extensions;

namespace Web.App.Domain.Charts;

// TODO: might not all be required
public static class CensusDimensions
{
    public enum ResultAsOptions
    {
        Total = 0,
        PercentWorkforce = 1
    }

    public static string GetQueryParam(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.Total => "Total",
        ResultAsOptions.PercentWorkforce => "PercentWorkforce",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetValueType(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.Total => ValueType.Simple,
        ResultAsOptions.PercentWorkforce => ValueType.Percent,
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetXAxisLabel(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.Total => "total",
        ResultAsOptions.PercentWorkforce => "percentage of workforce",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetFormattedValue(this ResultAsOptions option, decimal? value) => option switch
    {
        ResultAsOptions.Total => value.ToSimpleDisplay(),
        ResultAsOptions.PercentWorkforce => value.ToPercent(),
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetDescription(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.Total => "total",
        ResultAsOptions.PercentWorkforce => "percentage of workforce",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}