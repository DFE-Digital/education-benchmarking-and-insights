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
    public decimal? WorkforceFte { get; private set; }
    public decimal? TeachersFte { get; private set; }
    public decimal? SeniorLeadershipFte { get; private set; }
    public decimal? TeachingAssistantsFte { get; private set; }
    public decimal? NonClassroomSupportStaffFte { get; private set; }
    public decimal? AuxiliaryStaffFte { get; private set; }
    public decimal? WorkforceHeadcount { get; private set; }
    public decimal? TeachersQualified { get; private set; }
    public bool HasIncompleteData { get; private set; }

    private static CensusResponseModel CreateEmpty(int term)
    {
        return new CensusResponseModel
        {
            YearEnd = term
        };
    }

    public static CensusResponseModel Create(CensusDataObject? dataObject, int term, string dimension, string? category = null)
    {
        if (dataObject is null)
        {
            return CreateEmpty(term);

        }

        var result = new CensusResponseModel
        {
            Urn = dataObject.Urn.ToString(),
            Name = dataObject.SchoolName,
            SchoolType = dataObject.Type,
            LocalAuthority = dataObject.La.ToString(),
            NumberOfPupils = dataObject.NoPupils,
            YearEnd = term,
            HasIncompleteData = dataObject.PeriodCoveredByReturn != 12
        };

        if (category is null or CensusCategories.WorkforceFte)
        {
            result.WorkforceFte = CalcValue(dataObject.WorkforceTotal, dataObject, dimension);
        }

        if (category is null or CensusCategories.TeachersQualified)
        {
            result.TeachersQualified = dataObject.PercentageQualifiedTeachers;
        }

        if (category is null or CensusCategories.TeachersFte)
        {
            result.TeachersFte = CalcValue(dataObject.TeachersTotal, dataObject, dimension);
        }

        if (category is null or CensusCategories.TeachersFte)
        {
            result.TeachersFte = CalcValue(dataObject.TeachersTotal, dataObject, dimension);
        }

        if (category is null or CensusCategories.SeniorLeadershipFte)
        {
            result.SeniorLeadershipFte = CalcValue(dataObject.TeachersLeader, dataObject, dimension);
        }

        if (category is null or CensusCategories.TeachingAssistantsFte)
        {
            result.TeachingAssistantsFte = CalcValue(dataObject.FullTimeTa, dataObject, dimension);
        }

        if (category is null or CensusCategories.NonClassroomSupportStaffFte)
        {
            result.NonClassroomSupportStaffFte = CalcValue(dataObject.FullTimeOther, dataObject, dimension);
        }

        if (category is null or CensusCategories.AuxiliaryStaffFte)
        {
            result.AuxiliaryStaffFte = CalcValue(dataObject.AuxStaff, dataObject, dimension);
        }

        if (category is null or CensusCategories.WorkforceHeadcount)
        {
            result.WorkforceHeadcount = CalcValue(dataObject.WorkforceHeadcount, dataObject, dimension);
        }

        return result;
    }

    private static decimal CalcValue(decimal value, CensusDataObject dataObject, string dimension)
    {
        return dimension switch
        {
            CensusDimensions.Total => value,
            CensusDimensions.HeadcountPerFte => value != 0 ? dataObject.WorkforceHeadcount / value : 0,
            CensusDimensions.PercentWorkforce => dataObject.WorkforceTotal != 0
                ? value / dataObject.WorkforceTotal * 100
                : 0,
            CensusDimensions.PupilsPerStaffRole => value != 0 ? dataObject.NoPupils / value : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}