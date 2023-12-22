using System;
using System.Collections.Generic;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;

namespace EducationBenchmarking.Platform.Api.Insight.Models;

public class SchoolExpenditure
{
    public readonly string[] AllDimensions = { Constants.DimensionActual, Constants.DimensionPerPupil, Constants.DimensionPercentExpenditure, Constants.DimensionPercentIncome };
    
    public SectionDimensions Dimensions { get; set; }
    public Dictionary<string, School> Schools { get; set; } = new();
    public Dictionary<string, decimal> TotalExpenditure { get; set; } = new();
    public Dictionary<string, decimal> TotalTeachingSupportStaffCosts { get; set; } = new();
    public Dictionary<string, decimal> TeachingStaffCosts { get; set; } = new();
    public Dictionary<string, decimal> SupplyTeachingStaffCosts { get; set; } = new();
    public Dictionary<string, decimal> EducationalConsultancyCosts { get; set; } = new();
    public Dictionary<string, decimal> EducationSupportStaffCosts { get; set; } = new();
    public Dictionary<string, decimal> AgencySupplyTeachingStaffCosts { get; set; } = new();

    public static SchoolExpenditure Create(IEnumerable<SchoolTrustFinancialDataObject> results, SectionDimensions dimensions)
    {
        var expenditureResult = new SchoolExpenditure { Dimensions = dimensions };
        foreach (var result in results)
        {
            var key = result.URN.ToString();
            var noOfPupils = result.NoPupils;
            var totalExpenditure = result.TotalExpenditure;
            var totalIncome = result.TotalIncome;
            var totalTeachingSupportStaffCosts = result.TeachingStaff + result.SupplyTeachingStaff + result.EducationalConsultancy + result.EducationSupportStaff + result.AgencyTeachingStaff;
            
            expenditureResult.Schools[key] = new School { Urn = result.URN.ToString(), Name = result.SchoolName, FinanceType = result.FinanceType, LocalAuthority = result.LA.ToString() };
            
            expenditureResult.TotalExpenditure[key] = CalculateValue(dimensions.TotalExpenditure, result.TotalExpenditure, noOfPupils, totalExpenditure, totalIncome);
            
            expenditureResult.TotalTeachingSupportStaffCosts[key] = CalculateValue(dimensions.TeachingSupportStaff, totalTeachingSupportStaffCosts, noOfPupils, totalExpenditure, totalIncome);
            expenditureResult.TeachingStaffCosts[key] = CalculateValue(dimensions.TeachingSupportStaff, result.TeachingStaff, noOfPupils, totalExpenditure, totalIncome);
            expenditureResult.SupplyTeachingStaffCosts[key] = CalculateValue(dimensions.TeachingSupportStaff, result.SupplyTeachingStaff, noOfPupils, totalExpenditure, totalIncome);
            expenditureResult.EducationalConsultancyCosts[key] = CalculateValue(dimensions.TeachingSupportStaff, result.EducationalConsultancy, noOfPupils, totalExpenditure, totalIncome);
            expenditureResult.EducationSupportStaffCosts[key] = CalculateValue(dimensions.TeachingSupportStaff, result.EducationSupportStaff, noOfPupils, totalExpenditure, totalIncome);
            expenditureResult.AgencySupplyTeachingStaffCosts[key] = CalculateValue(dimensions.TeachingSupportStaff, result.AgencyTeachingStaff, noOfPupils, totalExpenditure, totalIncome);
            

        }
        
        

        return expenditureResult;
    }
    
    private static decimal CalculateValue(string dimension, decimal raw, decimal numberOfPupils, decimal totalExpenditure, decimal totalIncome)
    {
        return dimension switch
        {
            Constants.DimensionActual => raw,
            Constants.DimensionPerPupil => raw / numberOfPupils,
            Constants.DimensionPercentExpenditure => (raw / totalExpenditure) * 100,
            Constants.DimensionPercentIncome => (raw / totalIncome) * 100,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension))
        };
    }
}


