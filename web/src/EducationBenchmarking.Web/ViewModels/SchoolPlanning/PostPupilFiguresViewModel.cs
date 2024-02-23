namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class PostPupilFiguresViewModel
{
    public string? PupilsYear7 { get; set; }
    public string? PupilsYear8 { get; set; }
    public string? PupilsYear9 { get; set; }
    public string? PupilsYear10 { get; set; }
    public string? PupilsYear11 { get; set; }
    public decimal? PupilsYear12 { get; set; }
    public decimal? PupilsYear13 { get; set; }

    public int? PupilsYear7Parsed =>
        int.TryParse(PupilsYear7, out var val) ? val : null;

    public int? PupilsYear8Parsed =>
        int.TryParse(PupilsYear8, out var val) ? val : null;

    public int? PupilsYear9Parsed =>
        int.TryParse(PupilsYear9, out var val) ? val : null;

    public int? PupilsYear10Parsed =>
        int.TryParse(PupilsYear10, out var val) ? val : null;

    public int? PupilsYear11Parsed =>
        int.TryParse(PupilsYear11, out var val) ? val : null;

    public Dictionary<string, string> Validate(bool isSixthForm)
    {
        var errors = new Dictionary<string, string>();

        if (!HasSingleYearEntered(out var formMsg))
        {
            errors["PupilFigures"] = formMsg;
        }

        if (!IsPupilsYear7Valid(out var pupil7Msg))
        {
            errors[nameof(PupilsYear7)] = pupil7Msg;
        }

        if (!IsPupilsYear8Valid(out var pupil8Msg))
        {
            errors[nameof(PupilsYear8)] = pupil8Msg;
        }

        if (!IsPupilsYear9Valid(out var pupil9Msg))
        {
            errors[nameof(PupilsYear9)] = pupil9Msg;
        }

        if (!IsPupilsYear10Valid(out var pupil10Msg))
        {
            errors[nameof(PupilsYear10)] = pupil10Msg;
        }

        if (!IsPupilsYear11Valid(out var pupil11Msg))
        {
            errors[nameof(PupilsYear11)] = pupil11Msg;
        }

        if (!IsPupilsYear12Valid(isSixthForm, out var pupil12Msg))
        {
            errors[nameof(PupilsYear12)] = pupil12Msg;
        }

        if (!IsPupilsYear13Valid(isSixthForm, out var pupil13Msg))
        {
            errors[nameof(PupilsYear13)] = pupil13Msg;
        }

        return errors;
    }

    private bool HasSingleYearEntered(out string message)
    {
        var isValid = PupilsYear7Parsed is > 0 ||
                      PupilsYear8Parsed is > 0 ||
                      PupilsYear9Parsed is > 0 ||
                      PupilsYear10Parsed is > 0 ||
                      PupilsYear11Parsed is > 0 ||
                      PupilsYear12 is > 0 ||
                      PupilsYear13 is > 0;

        message = isValid ? "" : "Enter pupil figures for at least one year";
        return isValid;
    }

    private bool IsPupilsYear7Valid(out string message)
    {
        var isValid = PupilsYear7Parsed is >= 0 || string.IsNullOrEmpty(PupilsYear7);

        message = isValid
            ? ""
            : PupilsYear7Parsed is null
                ? "Pupil figures for year 7 must be a whole number"
                : "Pupil figures for year 7 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear8Valid(out string message)
    {
        var isValid = PupilsYear8Parsed is >= 0 || string.IsNullOrEmpty(PupilsYear8);

        message = isValid
            ? ""
            : PupilsYear8Parsed is null
                ? "Pupil figures for year 8 must be a whole number"
                : "Pupil figures for year 8 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear9Valid(out string message)
    {
        var isValid = PupilsYear9Parsed is >= 0 || string.IsNullOrEmpty(PupilsYear9);

        message = isValid
            ? ""
            : PupilsYear9Parsed is null
                ? "Pupil figures for year 9 must be a whole number"
                : "Pupil figures for year 9 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear10Valid(out string message)
    {
        var isValid = PupilsYear10Parsed is >= 0 || string.IsNullOrEmpty(PupilsYear10);

        message = isValid
            ? ""
            : PupilsYear10Parsed is null
                ? "Pupil figures for year 10 must be a whole number"
                : "Pupil figures for year 10 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear11Valid(out string message)
    {
        var isValid = PupilsYear11Parsed is >= 0 || string.IsNullOrEmpty(PupilsYear11);

        message = isValid
            ? ""
            : PupilsYear11Parsed is null
                ? "Pupil figures for year 11 must be a whole number"
                : "Pupil figures for year 11 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear12Valid(bool isSixth, out string message)
    {
        var valid = isSixth && PupilsYear12 is null or >= 0;
        message = valid ? "" : "Pupil figures for year 12 must be 0 or more";
        return valid;
    }

    private bool IsPupilsYear13Valid(bool isSixth, out string message)
    {
        var valid = isSixth && PupilsYear13 is null or >= 0;
        message = valid ? "" : "Pupil figures for year 13 must be 0 or more";
        return valid;
    }
}