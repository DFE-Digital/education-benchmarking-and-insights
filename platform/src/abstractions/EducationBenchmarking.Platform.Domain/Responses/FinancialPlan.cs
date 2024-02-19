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
            MixedAgeYear5Year6 = dataObject.MixedAgeYear5Year6
        };
    }
}