namespace Web.App.Domain;

public static class SchoolsSummaryWorkforceDimensions
{
    public enum ResultAsOptions
    {
        PercentPupil = 0,
        Actuals = 1,
    }

    public static string GetQueryParam(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.PercentPupil => "PercentPupil",
        ResultAsOptions.Actuals => "Actuals",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetTableHeaderPrefix(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.PercentPupil => "% with ",
        ResultAsOptions.Actuals => string.Empty,
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetDescription(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.PercentPupil => "Percentage of pupils",
        ResultAsOptions.Actuals => "Pupil numbers",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}