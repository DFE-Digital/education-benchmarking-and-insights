using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record WorkforceResponseModel
{
    public string? Urn { get; private set; }
    public string? Name { get; private set; }
    public string? SchoolType { get; private set; }
    public string? LocalAuthority { get; private set; }
    public decimal NumberOfPupils { get; private set; }
    public int YearEnd { get; private set; }
    public WorkforceDimension Dimension { get; private set; }
    public decimal? WorkforceFte { get; private set; }
    public decimal? TeachersFte { get; private set; }
    public decimal? SeniorLeadershipFte { get; private set; }
    public decimal? TeachingAssistantsFte { get; private set; }
    public decimal? NonClassroomSupportStaffFte { get; private set; }
    public decimal? AuxiliaryStaffFte { get; private set; }
    public decimal? WorkforceHeadcount { get; private set; }
    public decimal? TeachersQualified { get; private set; }

    private static WorkforceResponseModel CreateEmpty(int term, WorkforceDimension dimension)
    {
        return new WorkforceResponseModel
        {
            YearEnd = term,
            Dimension = dimension
        };
    }

    public static WorkforceResponseModel Create(WorkforceDataObject? dataObject, int term, WorkforceDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                TeachersQualified = dataObject.PercentageQualifiedTeachers,
                WorkforceFte = CalcValue(dataObject.WorkforceTotal, dataObject, dimension),
                TeachersFte = CalcValue(dataObject.TeachersTotal, dataObject, dimension),
                SeniorLeadershipFte = CalcValue(dataObject.TeachersLeader, dataObject, dimension),
                TeachingAssistantsFte = CalcValue(dataObject.FullTimeTa, dataObject, dimension),
                NonClassroomSupportStaffFte = CalcValue(dataObject.FullTimeOther, dataObject, dimension),
                AuxiliaryStaffFte = CalcValue(dataObject.AuxStaff, dataObject, dimension),
                WorkforceHeadcount = CalcValue(dataObject.WorkforceHeadcount, dataObject, dimension)
            };
    }

    public static WorkforceResponseModel CreateTeachersQualified(WorkforceDataObject? dataObject, int term)
    {
        return dataObject is null
            ? CreateEmpty(term, WorkforceDimension.Percentage)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = WorkforceDimension.Percentage,
                TeachersQualified = dataObject.PercentageQualifiedTeachers,
            };
    }

    public static WorkforceResponseModel CreateWorkforceFte(WorkforceDataObject? dataObject, int term,
        WorkforceDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                WorkforceFte = CalcValue(dataObject.WorkforceTotal, dataObject, dimension),
            };
    }

    public static WorkforceResponseModel CreateTeachersFte(WorkforceDataObject? dataObject, int term,
        WorkforceDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                TeachersFte = CalcValue(dataObject.TeachersTotal, dataObject, dimension)
            };
    }


    public static WorkforceResponseModel CreateSeniorLeadershipFte(WorkforceDataObject? dataObject, int term,
        WorkforceDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                SeniorLeadershipFte = CalcValue(dataObject.TeachersLeader, dataObject, dimension)
            };
    }

    public static WorkforceResponseModel CreateTeachingAssistantsFte(WorkforceDataObject? dataObject, int term,
        WorkforceDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                TeachingAssistantsFte = CalcValue(dataObject.FullTimeTa, dataObject, dimension)
            };
    }

    public static WorkforceResponseModel CreateNonClassroomSupportStaffFte(WorkforceDataObject? dataObject, int term,
        WorkforceDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                NonClassroomSupportStaffFte = CalcValue(dataObject.FullTimeOther, dataObject, dimension)
            };
    }

    public static WorkforceResponseModel CreateAuxiliaryStaffFte(WorkforceDataObject? dataObject, int term,
        WorkforceDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                AuxiliaryStaffFte = CalcValue(dataObject.AuxStaff, dataObject, dimension),
            };
    }

    public static WorkforceResponseModel CreateWorkforceHeadcount(WorkforceDataObject? dataObject, int term,
        WorkforceDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new WorkforceResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                WorkforceHeadcount = CalcValue(dataObject.WorkforceHeadcount, dataObject, dimension)
            };
    }

    private static decimal CalcValue(decimal value, WorkforceDataObject dataObject, WorkforceDimension dimension)
    {
        return dimension switch
        {
            WorkforceDimension.Total => value,
            WorkforceDimension.HeadcountPerFte => value != 0 ? dataObject.WorkforceHeadcount / value : 0,
            WorkforceDimension.PercentWorkforce => dataObject.WorkforceTotal != 0
                ? value / dataObject.WorkforceTotal * 100
                : 0,
            WorkforceDimension.PupilsPerStaffRole => value != 0 ? dataObject.NoPupils / value : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}