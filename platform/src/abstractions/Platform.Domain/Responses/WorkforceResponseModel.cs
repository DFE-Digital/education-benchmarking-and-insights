using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record WorkforceResponseModel
{
    public int YearEnd { get; private set; }
    public Dimension Dimension { get; private set; }
    public decimal? WorkforceFte { get; private set; }
    public decimal? TeachersFte { get; private set; }
    public decimal? SeniorLeadershipFte { get; private set; }
    public decimal? TeachingAssistantsFte { get; private set; }
    public decimal? NonClassroomSupportStaffFte { get; private set; }
    public decimal? AuxiliaryStaffFte { get; private set; }
    public decimal? WorkforceHeadcount { get; private set; }
    public decimal? TeachersWithQts { get; private set; }

    public static WorkforceResponseModel Create(SchoolTrustFinancialDataObject? dataObject, int term,
        Dimension dimension = Dimension.Total)
    {
        return dataObject is null
            ? new WorkforceResponseModel
            {
                YearEnd = term,
                Dimension = dimension,
            }
            : new WorkforceResponseModel
            {
                YearEnd = term,
                Dimension = dimension,
                TeachersWithQts = dataObject.PercentageQualifiedTeachers,
                WorkforceFte = CalculationValue(dataObject.WorkforceTotal, dataObject, dimension),
                TeachersFte = CalculationValue(dataObject.TeachersTotal, dataObject, dimension),
                SeniorLeadershipFte = CalculationValue(dataObject.TeachersLeader, dataObject, dimension),
                TeachingAssistantsFte = CalculationValue(dataObject.FullTimeTa, dataObject, dimension),
                NonClassroomSupportStaffFte = CalculationValue(dataObject.FullTimeOther, dataObject, dimension),
                AuxiliaryStaffFte = CalculationValue(dataObject.AuxStaff, dataObject, dimension),
                WorkforceHeadcount = CalculationValue(dataObject.WorkforceHeadcount, dataObject, dimension)
            };
    }

    private static decimal CalculationValue(decimal value, SchoolTrustFinancialDataObject dataObject,
        Dimension dimension)
    {
        return dimension switch
        {
            Dimension.Total => value,
            Dimension.HeadcountPerFte => value != 0 ? dataObject.WorkforceHeadcount / value : 0,
            Dimension.PercentWorkforce => dataObject.WorkforceTotal != 0 ? value / dataObject.WorkforceTotal * 100 : 0,
            Dimension.PupilsPerStaffRole => value != 0 ? dataObject.NoPupils / value : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}