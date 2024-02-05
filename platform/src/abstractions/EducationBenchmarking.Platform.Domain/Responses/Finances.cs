using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Domain.DataObjects;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public class Finances
{
    public string Urn { get; set; }
    public string SchoolName { get; set; }
    public int YearEnd { get; set; }
    public string OverallPhase { get; set; }
    public decimal NumberOfPupils { get; set; }
    public bool HasSixthForm { get; set; }
    public decimal TotalExpenditure { get; set; }
    public decimal TeachingStaffCosts { get; set; } 
    public decimal TotalIncome { get; set; }
    public decimal BreakdownEducationalSuppliesCosts { get; set; }
    public decimal SupplyTeachingStaffCosts { get; set; } 
    public decimal EducationSupportStaffCosts { get; set; } 
    public decimal AdministrativeClericalStaffCosts { get; set; }
    public decimal OtherStaffCosts { get; set; }
    public decimal MaintenancePremisesCosts { get; set; }
    public decimal TotalNumberOfTeachersFte { get; set; }

    public static Finances Create(SchoolTrustFinancialDataObject dataObject, int term)
    {
        return new Finances
        {
            YearEnd = term,
            Urn = dataObject.URN.ToString(),
            SchoolName = dataObject.SchoolName,
            OverallPhase = dataObject.OverallPhase,
            NumberOfPupils = dataObject.NoPupils,
            HasSixthForm = bool.TryParse(dataObject.Has6Form, out var value) && value,
            TotalExpenditure = dataObject.TotalExpenditure,
            TeachingStaffCosts = dataObject.TeachingStaff,
            TotalIncome = dataObject.TotalIncome,
            BreakdownEducationalSuppliesCosts = dataObject.EducationalSupplies,
            SupplyTeachingStaffCosts = dataObject.SupplyTeachingStaff,
            EducationSupportStaffCosts = dataObject.EducationSupportStaff,
            AdministrativeClericalStaffCosts = dataObject.AdministrativeClericalStaff,
            OtherStaffCosts = dataObject.OtherStaffCosts,
            MaintenancePremisesCosts = dataObject.Premises,
            TotalNumberOfTeachersFte = dataObject.TeachersTotal
        };
    }
}