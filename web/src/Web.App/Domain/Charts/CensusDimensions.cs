using Web.App.Extensions;

namespace Web.App.Domain.Charts;

public static class CensusDimensions
{
    public enum ResultAsOptions
    {
        Total = 0,
        PercentWorkforce = 1
    }

    public static readonly ResultAsOptions[] All =
    [
        ResultAsOptions.Total,
        ResultAsOptions.PercentWorkforce,
    ];

    public static string GetQueryParam(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.Total => "Total",
        ResultAsOptions.PercentWorkforce => "PercentWorkforce",
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