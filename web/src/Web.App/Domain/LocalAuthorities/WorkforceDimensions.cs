namespace Web.App.Domain.LocalAuthorities;

public static class WorkforceDimensions
{
    public enum ResultAsOptions
    {
        PercentPerPupil = 0,
        Actuals = 1,
    }

    public static string GetQueryParam(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.PercentPerPupil => "PercentPerPupil",
        ResultAsOptions.Actuals => "Actuals",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetTableHeaderPrefix(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.PercentPerPupil => "% with ",
        ResultAsOptions.Actuals => string.Empty,
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetDescription(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.PercentPerPupil => "Percentage of pupils",
        ResultAsOptions.Actuals => "Pupil numbers",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}