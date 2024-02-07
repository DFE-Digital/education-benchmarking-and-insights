using System;
using EducationBenchmarking.Platform.Domain.DataObjects;

namespace EducationBenchmarking.Platform.Domain.Responses;

public record FinancialPlan
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public int Version { get; set; }
    public string? TotalIncome { get; set; }
    public string? TotalExpenditure { get; set; }
    public string? TotalTeacherCosts { get; set; }
    public string? TotalNumberOfTeachersFte { get; set; }
    public string? EducationSupportStaffCosts { get; set; }
    public bool UseFigures { get; set; }

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
            EducationSupportStaffCosts = dataObject.EducationSupportStaffCosts
        };
    }
}