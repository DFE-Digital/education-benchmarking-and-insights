using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Domain.Requests;

[ExcludeFromCodeCoverage]
public record FinancialPlanRequest
{
    public string? User { get; set; }
    public bool? UseFigures { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public int? TimetablePeriods { get; set; }
    public bool? HasMixedAgeClasses { get; set; }
    public bool MixedAgeReceptionYear1 { get; set; }
    public bool MixedAgeYear1Year2 { get; set; }
    public bool MixedAgeYear2Year3 { get; set; }
    public bool MixedAgeYear3Year4 { get; set; }
    public bool MixedAgeYear4Year5 { get; set; }
    public bool MixedAgeYear5Year6 { get; set; }
    public int? PupilsYear7 { get; set; }
    public int? PupilsYear8 { get; set; }
    public int? PupilsYear9 { get; set; }
    public int? PupilsYear10 { get; set; }
    public int? PupilsYear11 { get; set; }
    public decimal? PupilsYear12 { get; set; }
    public decimal? PupilsYear13 { get; set; }
    public decimal? PupilsNursery { get; set; }
    public int? PupilsMixedReceptionYear1 { get; set; }
    public int? PupilsMixedYear1Year2 { get; set; }
    public int? PupilsMixedYear2Year3 { get; set; }
    public int? PupilsMixedYear3Year4 { get; set; }
    public int? PupilsMixedYear4Year5 { get; set; }
    public int? PupilsMixedYear5Year6 { get; set; }
    public int? PupilsReception { get; set; }
    public int? PupilsYear1 { get; set; }
    public int? PupilsYear2 { get; set; }
    public int? PupilsYear3 { get; set; }
    public int? PupilsYear4 { get; set; }
    public int? PupilsYear5 { get; set; }
    public int? PupilsYear6 { get; set; }
}