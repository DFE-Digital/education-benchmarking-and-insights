using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.ViewModels.SchoolPlanning;

public class PostPrimaryPupilFiguresViewModel
{
    public decimal? PupilsNursery { get; set; }

    public string? PupilsMixedReceptionYear1 { get; set; }
    public string? PupilsMixedYear1Year2 { get; set; }
    public string? PupilsMixedYear2Year3 { get; set; }
    public string? PupilsMixedYear3Year4 { get; set; }
    public string? PupilsMixedYear4Year5 { get; set; }
    public string? PupilsMixedYear5Year6 { get; set; }
    public string? PupilsReception { get; set; }
    public string? PupilsYear1 { get; set; }
    public string? PupilsYear2 { get; set; }
    public string? PupilsYear3 { get; set; }
    public string? PupilsYear4 { get; set; }
    public string? PupilsYear5 { get; set; }
    public string? PupilsYear6 { get; set; }


    public int? PupilsMixedReceptionYear1Parsed => PupilsMixedReceptionYear1.ToInt();
    public int? PupilsMixedYear1Year2Parsed => PupilsMixedYear1Year2.ToInt();
    public int? PupilsMixedYear2Year3Parsed => PupilsMixedYear2Year3.ToInt();
    public int? PupilsMixedYear3Year4Parsed => PupilsMixedYear3Year4.ToInt();
    public int? PupilsMixedYear4Year5Parsed => PupilsMixedYear4Year5.ToInt();
    public int? PupilsMixedYear5Year6Parsed => PupilsMixedYear5Year6.ToInt();
    public int? PupilsReceptionParsed => PupilsReception.ToInt();
    public int? PupilsYear1Parsed => PupilsYear1.ToInt();
    public int? PupilsYear2Parsed => PupilsYear2.ToInt();
    public int? PupilsYear3Parsed => PupilsYear3.ToInt();
    public int? PupilsYear4Parsed => PupilsYear4.ToInt();
    public int? PupilsYear5Parsed => PupilsYear5.ToInt();
    public int? PupilsYear6Parsed => PupilsYear6.ToInt();

    public Dictionary<string, string> Validate(bool mixedReceptionYear1, bool mixedYear1Year2, bool mixedYear2Year3,
        bool mixedYear3Year4, bool mixedYear4Year5, bool mixedYear5Year6)
    {
        var errors = new Dictionary<string, string>();

        if (!HasSingleYearEntered(out var formMsg))
        {
            errors["PupilFigures"] = formMsg;
        }

        if (!IsPupilsNurseryValid(out var pupilNurseryMsg))
        {
            errors[nameof(PupilsNursery)] = pupilNurseryMsg;
        }

        if (!IsPupilsMixedReceptionYear1Valid(mixedReceptionYear1, out var pupilMixedReceptionYear1Msg))
        {
            errors[nameof(PupilsMixedReceptionYear1)] = pupilMixedReceptionYear1Msg;
        }

        if (!IsPupilsMixedYear1Year2Valid(mixedYear1Year2, out var pupilMixedYear1Year2Msg))
        {
            errors[nameof(PupilsMixedYear1Year2)] = pupilMixedYear1Year2Msg;
        }

        if (!IsPupilsMixedYear2Year3Valid(mixedYear2Year3, out var pupilMixedYear2Year3Msg))
        {
            errors[nameof(PupilsMixedYear2Year3)] = pupilMixedYear2Year3Msg;
        }

        if (!IsPupilsMixedYear3Year4Valid(mixedYear3Year4, out var pupilMixedYear3Year4Msg))
        {
            errors[nameof(PupilsMixedYear3Year4)] = pupilMixedYear3Year4Msg;
        }

        if (!IsPupilsMixedYear4Year5Valid(mixedYear4Year5, out var pupilMixedYear4Year5Msg))
        {
            errors[nameof(PupilsMixedYear4Year5)] = pupilMixedYear4Year5Msg;
        }

        if (!IsPupilsMixedYear5Year6Valid(mixedYear5Year6, out var pupilMixedYear5Year6Msg))
        {
            errors[nameof(PupilsMixedYear5Year6)] = pupilMixedYear5Year6Msg;
        }

        if (!IsPupilsReceptionValid(mixedReceptionYear1, out var pupilReceptionMsg))
        {
            errors[nameof(PupilsReception)] = pupilReceptionMsg;
        }

        if (!IsPupilsYear1Valid(mixedReceptionYear1 || mixedYear1Year2, out var pupilYear1Msg))
        {
            errors[nameof(PupilsYear1)] = pupilYear1Msg;
        }

        if (!IsPupilsYear2Valid(mixedYear1Year2 || mixedYear2Year3, out var pupilYear2Msg))
        {
            errors[nameof(PupilsYear2)] = pupilYear2Msg;
        }

        if (!IsPupilsYear3Valid(mixedYear2Year3 || mixedYear3Year4, out var pupilYear3Msg))
        {
            errors[nameof(PupilsYear3)] = pupilYear3Msg;
        }

        if (!IsPupilsYear4Valid(mixedYear3Year4 || mixedYear4Year5, out var pupilYear4Msg))
        {
            errors[nameof(PupilsYear4)] = pupilYear4Msg;
        }

        if (!IsPupilsYear5Valid(mixedYear4Year5 || mixedYear5Year6, out var pupilYear5Msg))
        {
            errors[nameof(PupilsYear5)] = pupilYear5Msg;
        }

        if (!IsPupilsYear6Valid(mixedYear5Year6, out var pupilYear6Msg))
        {
            errors[nameof(PupilsYear6)] = pupilYear6Msg;
        }

        return errors;
    }

    private bool HasSingleYearEntered(out string message)
    {
        var isValid = PupilsNursery is > 0 ||
                      PupilsMixedReceptionYear1Parsed is > 0 ||
                      PupilsMixedYear1Year2Parsed is > 0 ||
                      PupilsMixedYear2Year3Parsed is > 0 ||
                      PupilsMixedYear3Year4Parsed is > 0 ||
                      PupilsMixedYear4Year5Parsed is > 0 ||
                      PupilsMixedYear5Year6Parsed is > 0 ||
                      PupilsReceptionParsed is > 0 ||
                      PupilsYear1Parsed is > 0 ||
                      PupilsYear2Parsed is > 0 ||
                      PupilsYear3Parsed is > 0 ||
                      PupilsYear4Parsed is > 0 ||
                      PupilsYear5Parsed is > 0 ||
                      PupilsYear6Parsed is > 0;

        message = isValid ? "" : "Enter pupil figures for at least one year";
        return isValid;
    }

    private bool IsPupilsNurseryValid(out string message)
    {
        var valid = PupilsNursery is null or >= 0;
        message = valid ? "" : "Pupil figures for nursery must be 0 or more";
        return valid;
    }

    private bool IsPupilsMixedReceptionYear1Valid(bool isMixed, out string message)
    {
        var isValid = !isMixed ||
                      PupilsMixedReceptionYear1Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsMixedReceptionYear1);

        message = isValid
            ? ""
            : PupilsMixedReceptionYear1Parsed is null
                ? "Pupil figures for reception and year 1 must be a whole number"
                : "Pupil figures for reception and year 1 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsMixedYear1Year2Valid(bool isMixed, out string message)
    {
        var isValid = !isMixed ||
                      PupilsMixedYear1Year2Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsMixedYear1Year2);

        message = isValid
            ? ""
            : PupilsMixedYear1Year2Parsed is null
                ? "Pupil figures for year 1 and year 2 must be a whole number"
                : "Pupil figures for year 1 and year 2 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsMixedYear2Year3Valid(bool isMixed, out string message)
    {
        var isValid = !isMixed ||
                      PupilsMixedYear2Year3Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsMixedYear2Year3);

        message = isValid
            ? ""
            : PupilsMixedYear2Year3Parsed is null
                ? "Pupil figures for year 2 and year 3 must be a whole number"
                : "Pupil figures for year 2 and year 3 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsMixedYear3Year4Valid(bool isMixed, out string message)
    {
        var isValid = !isMixed ||
                      PupilsMixedYear3Year4Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsMixedYear3Year4);

        message = isValid
            ? ""
            : PupilsMixedYear3Year4Parsed is null
                ? "Pupil figures for year 3 and year 4 must be a whole number"
                : "Pupil figures for year 3 and year 4 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsMixedYear4Year5Valid(bool isMixed, out string message)
    {
        var isValid = !isMixed ||
                      PupilsMixedYear4Year5Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsMixedYear4Year5);

        message = isValid
            ? ""
            : PupilsMixedYear4Year5Parsed is null
                ? "Pupil figures for year 4 and year 5 must be a whole number"
                : "Pupil figures for year 4 and year 5 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsMixedYear5Year6Valid(bool isMixed, out string message)
    {
        var isValid = !isMixed ||
                      PupilsMixedYear5Year6Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsMixedYear5Year6);

        message = isValid
            ? ""
            : PupilsMixedYear5Year6Parsed is null
                ? "Pupil figures for year 5 and year 6 must be a whole number"
                : "Pupil figures for year 5 and year 6 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsReceptionValid(bool isMixed, out string message)
    {
        var isValid = isMixed ||
                      PupilsReceptionParsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsReception);

        message = isValid
            ? ""
            : PupilsReceptionParsed is null
                ? "Pupil figures for reception must be a whole number"
                : "Pupil figures for reception must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear1Valid(bool isMixed, out string message)
    {
        var isValid = isMixed ||
                      PupilsYear1Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsYear1);

        message = isValid
            ? ""
            : PupilsYear1Parsed is null
                ? "Pupil figures for year 1 must be a whole number"
                : "Pupil figures for year 1 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear2Valid(bool isMixed, out string message)
    {
        var isValid = isMixed ||
                      PupilsYear2Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsYear2);

        message = isValid
            ? ""
            : PupilsYear2Parsed is null
                ? "Pupil figures for year 2 must be a whole number"
                : "Pupil figures for year 2 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear3Valid(bool isMixed, out string message)
    {
        var isValid = isMixed ||
                      PupilsYear3Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsYear3);

        message = isValid
            ? ""
            : PupilsYear3Parsed is null
                ? "Pupil figures for year 3 must be a whole number"
                : "Pupil figures for year 3 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear4Valid(bool isMixed, out string message)
    {
        var isValid = isMixed ||
                      PupilsYear4Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsYear4);

        message = isValid
            ? ""
            : PupilsYear4Parsed is null
                ? "Pupil figures for year 4 must be a whole number"
                : "Pupil figures for year 4 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear5Valid(bool isMixed, out string message)
    {
        var isValid = isMixed ||
                      PupilsYear5Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsYear5);

        message = isValid
            ? ""
            : PupilsYear5Parsed is null
                ? "Pupil figures for year 5 must be a whole number"
                : "Pupil figures for year 5 must be 0 or more";

        return isValid;
    }

    private bool IsPupilsYear6Valid(bool isMixed, out string message)
    {
        var isValid = isMixed ||
                      PupilsYear6Parsed is >= 0 ||
                      string.IsNullOrEmpty(PupilsYear6);

        message = isValid
            ? ""
            : PupilsYear6Parsed is null
                ? "Pupil figures for year 6 must be a whole number"
                : "Pupil figures for year 6 must be 0 or more";

        return isValid;
    }
}