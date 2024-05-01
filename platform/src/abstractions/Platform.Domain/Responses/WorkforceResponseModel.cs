using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record CensusResponseModel
{
    public string? Urn { get; private set; }
    public string? Name { get; private set; }
    public string? SchoolType { get; private set; }
    public string? LocalAuthority { get; private set; }
    public decimal NumberOfPupils { get; private set; }
    public int YearEnd { get; private set; }
    public string Term => $"{YearEnd - 1} to {YearEnd}";
    public CensusDimension Dimension { get; private set; }
    public decimal? WorkforceFte { get; private set; }
    public decimal? TeachersFte { get; private set; }
    public decimal? SeniorLeadershipFte { get; private set; }
    public decimal? TeachingAssistantsFte { get; private set; }
    public decimal? NonClassroomSupportStaffFte { get; private set; }
    public decimal? AuxiliaryStaffFte { get; private set; }
    public decimal? WorkforceHeadcount { get; private set; }
    public decimal? TeachersQualified { get; private set; }
    public bool HasIncompleteData { get; private set; }

    private static CensusResponseModel CreateEmpty(int term, CensusDimension dimension)
    {
        return new CensusResponseModel
        {
            YearEnd = term,
            Dimension = dimension
        };
    }

    public static CensusResponseModel Create(CensusDataObject? dataObject, int term, CensusDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new CensusResponseModel
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
                WorkforceHeadcount = CalcValue(dataObject.WorkforceHeadcount, dataObject, dimension),
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }

    public static CensusResponseModel CreateTeachersQualified(CensusDataObject? dataObject, int term)
    {
        return dataObject is null
            ? CreateEmpty(term, CensusDimension.Percentage)
            : new CensusResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = CensusDimension.Percentage,
                TeachersQualified = dataObject.PercentageQualifiedTeachers,
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }

    public static CensusResponseModel CreateWorkforceFte(CensusDataObject? dataObject, int term,
        CensusDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new CensusResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                WorkforceFte = CalcValue(dataObject.WorkforceTotal, dataObject, dimension),
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }

    public static CensusResponseModel CreateTeachersFte(CensusDataObject? dataObject, int term,
        CensusDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new CensusResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                TeachersFte = CalcValue(dataObject.TeachersTotal, dataObject, dimension),
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }


    public static CensusResponseModel CreateSeniorLeadershipFte(CensusDataObject? dataObject, int term,
        CensusDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new CensusResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                SeniorLeadershipFte = CalcValue(dataObject.TeachersLeader, dataObject, dimension),
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }

    public static CensusResponseModel CreateTeachingAssistantsFte(CensusDataObject? dataObject, int term,
        CensusDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new CensusResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                TeachingAssistantsFte = CalcValue(dataObject.FullTimeTa, dataObject, dimension),
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }

    public static CensusResponseModel CreateNonClassroomSupportStaffFte(CensusDataObject? dataObject, int term,
        CensusDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new CensusResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                NonClassroomSupportStaffFte = CalcValue(dataObject.FullTimeOther, dataObject, dimension),
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }

    public static CensusResponseModel CreateAuxiliaryStaffFte(CensusDataObject? dataObject, int term,
        CensusDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new CensusResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                AuxiliaryStaffFte = CalcValue(dataObject.AuxStaff, dataObject, dimension),
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }

    public static CensusResponseModel CreateWorkforceHeadcount(CensusDataObject? dataObject, int term,
        CensusDimension dimension)
    {
        return dataObject is null
            ? CreateEmpty(term, dimension)
            : new CensusResponseModel
            {
                Urn = dataObject.Urn.ToString(),
                Name = dataObject.SchoolName,
                SchoolType = dataObject.Type,
                LocalAuthority = dataObject.La.ToString(),
                NumberOfPupils = dataObject.NoPupils,
                YearEnd = term,
                Dimension = dimension,
                WorkforceHeadcount = CalcValue(dataObject.WorkforceHeadcount, dataObject, dimension),
                HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
            };
    }

    private static decimal CalcValue(decimal value, CensusDataObject dataObject, CensusDimension dimension)
    {
        return dimension switch
        {
            CensusDimension.Total => value,
            CensusDimension.HeadcountPerFte => value != 0 ? dataObject.WorkforceHeadcount / value : 0,
            CensusDimension.PercentWorkforce => dataObject.WorkforceTotal != 0
                ? value / dataObject.WorkforceTotal * 100
                : 0,
            CensusDimension.PupilsPerStaffRole => value != 0 ? dataObject.NoPupils / value : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}