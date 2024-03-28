using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record FinanceWorkforceResponseModel
{
    public int YearEnd { get; set; }
    public string? Urn { get; private set; }
    public string? Name { get; private set; }
    public string? SchoolType { get; private set; }
    public string? LocalAuthority { get; private set; }
    public decimal NumberPupils { get; private set; }
    public WorkforcePayloadResponseModel? Payload { get; private set; }

    public static FinanceWorkforceResponseModel Create(SchoolTrustFinancialDataObject dataObject, int term, Dimension dimension = Dimension.Total)
    {
        return new FinanceWorkforceResponseModel
        {
            YearEnd = term,
            Urn = dataObject.Urn.ToString(),
            Name = dataObject.SchoolName,
            SchoolType = dataObject.Type,
            LocalAuthority = dataObject.La.ToString(),
            NumberPupils = dataObject.NoPupils,
            Payload = WorkforcePayloadResponseModel.Create(dataObject, dimension)
        };
    }
}

[ExcludeFromCodeCoverage]
public record WorkforcePayloadResponseModel
{
    public Dimension Dimension { get; private set; }
    public decimal WorkforceFte { get; private set; }
    public decimal TeachersFte { get; private set; }
    public decimal SeniorLeadershipFte { get; private set; }
    public decimal TeachingAssistantsFte { get; private set; }
    public decimal NonClassroomSupportStaffFte { get; private set; }
    public decimal AuxiliaryStaffFte { get; private set; }
    public decimal WorkforceHeadcount { get; private set; }
    public decimal TeachersWithQts { get; private set; }

    public static WorkforcePayloadResponseModel Create(SchoolTrustFinancialDataObject dataObject, Dimension dimension)
    {
        return new WorkforcePayloadResponseModel
        {
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

    private static decimal CalculationValue(decimal value, SchoolTrustFinancialDataObject dataObject, Dimension dimension)
    {
        return dimension switch
        {
            Dimension.Total => value,
            Dimension.HeadcountPerFte => dataObject.WorkforceHeadcount / value,
            Dimension.PercentWorkforce => value / dataObject.WorkforceTotal * 100,
            Dimension.PupilsPerStaffRole => dataObject.NoPupils / value,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}