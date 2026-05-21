using Web.App.Extensions;
// ReSharper disable ConvertToExtensionBlock

namespace Web.App.Domain.Charts;

public static class HighNeedsDimensions
{
    public enum ResultAsOptions
    {
        PerPupil = 0,
        PerEhcp = 1,
        PerSenSupport = 2,
        PerTotalSupport = 3
    }

    public static readonly ResultAsOptions[] AllResultsAsOptions =
    [
        ResultAsOptions.PerPupil,
        ResultAsOptions.PerEhcp,
        ResultAsOptions.PerSenSupport,
        ResultAsOptions.PerTotalSupport,
    ];

    public enum SubmissionTypeOptions
    {
        Outturn = 0,
        Budget = 1
    }

    public static readonly SubmissionTypeOptions[] AllSubmissionTypeOptions =
    [
        SubmissionTypeOptions.Outturn,
        SubmissionTypeOptions.Budget
    ];

    public static string GetSubmissionTypeQueryParam(this SubmissionTypeOptions option) => option switch
    {
        SubmissionTypeOptions.Budget => nameof(SubmissionTypeOptions.Budget),
        SubmissionTypeOptions.Outturn => nameof(SubmissionTypeOptions.Outturn),
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetResultAsQueryParam(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.PerPupil => nameof(ResultAsOptions.PerPupil),
        ResultAsOptions.PerEhcp => nameof(ResultAsOptions.PerEhcp),
        ResultAsOptions.PerSenSupport => nameof(ResultAsOptions.PerSenSupport),
        ResultAsOptions.PerTotalSupport => nameof(ResultAsOptions.PerTotalSupport),
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetSubmissionTypeDescription(this SubmissionTypeOptions option) => option switch
    {
        SubmissionTypeOptions.Budget => "Planned expenditure",
        SubmissionTypeOptions.Outturn => "Outturn",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetSubmissionTypeFileName(this SubmissionTypeOptions option) => option switch
    {
        SubmissionTypeOptions.Budget => "planned-expenditure",
        SubmissionTypeOptions.Outturn => "outturn",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetResultAsDescription(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.PerPupil => "£ per pupil",
        ResultAsOptions.PerEhcp => "£ per EHCP",
        ResultAsOptions.PerSenSupport => "£ per SEN support",
        ResultAsOptions.PerTotalSupport => "£ per EHCP and SEN support",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}
