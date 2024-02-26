using System;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Domain.DataObjects;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record FinancialPlan
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public int Version { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeacherCosts { get; set; }
    public decimal? TotalNumberOfTeachersFte { get; set; }
    public decimal? EducationSupportStaffCosts { get; set; }
    public bool? UseFigures { get; set; }
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

    public static FinancialPlan Create(FinancialPlanDataObject dataObject)
    {
        return new FinancialPlan
        {
            Year = int.Parse(dataObject.Id ?? throw new ArgumentNullException(nameof(dataObject.Id))),
            Urn = dataObject.PartitionKey,
            Created = dataObject.Created,
            UpdatedAt = dataObject.UpdatedAt,
            UpdatedBy = dataObject.UpdatedBy,
            CreatedBy = dataObject.CreatedBy,
            Version = dataObject.Version,
            UseFigures = dataObject.UseFigures,
            TotalIncome = dataObject.TotalIncome,
            TotalExpenditure = dataObject.TotalExpenditure,
            TotalTeacherCosts = dataObject.TotalTeacherCosts,
            TotalNumberOfTeachersFte = dataObject.TotalNumberOfTeachersFte,
            EducationSupportStaffCosts = dataObject.EducationSupportStaffCosts,
            TimetablePeriods = dataObject.TimetablePeriods,
            HasMixedAgeClasses = dataObject.HasMixedAgeClasses,
            MixedAgeReceptionYear1 = dataObject.MixedAgeReceptionYear1,
            MixedAgeYear1Year2 = dataObject.MixedAgeYear1Year2,
            MixedAgeYear2Year3 = dataObject.MixedAgeYear2Year3,
            MixedAgeYear3Year4 = dataObject.MixedAgeYear3Year4,
            MixedAgeYear4Year5 = dataObject.MixedAgeYear4Year5,
            MixedAgeYear5Year6 = dataObject.MixedAgeYear5Year6,
            PupilsYear7 = dataObject.PupilsYear7,
            PupilsYear8 = dataObject.PupilsYear8,
            PupilsYear9 = dataObject.PupilsYear9,
            PupilsYear10 = dataObject.PupilsYear10,
            PupilsYear11 = dataObject.PupilsYear11,
            PupilsYear12 = dataObject.PupilsYear12,
            PupilsYear13 = dataObject.PupilsYear13,
            PupilsNursery = dataObject.PupilsNursery,
            PupilsReception = dataObject.PupilsReception,
            PupilsYear1 = dataObject.PupilsYear1,
            PupilsYear2 = dataObject.PupilsYear2,
            PupilsYear3 = dataObject.PupilsYear3,
            PupilsYear4 = dataObject.PupilsYear4,
            PupilsYear5 = dataObject.PupilsYear5,
            PupilsYear6 = dataObject.PupilsYear6,
            PupilsMixedReceptionYear1 = dataObject.PupilsMixedReceptionYear1,
            PupilsMixedYear1Year2 = dataObject.PupilsMixedYear1Year2,
            PupilsMixedYear2Year3 = dataObject.PupilsMixedYear2Year3,
            PupilsMixedYear3Year4 = dataObject.PupilsMixedYear3Year4,
            PupilsMixedYear4Year5 = dataObject.PupilsMixedYear4Year5,
            PupilsMixedYear5Year6 = dataObject.PupilsMixedYear5Year6
        };
    }
}