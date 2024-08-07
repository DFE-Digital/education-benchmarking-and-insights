﻿using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Insight.Census;

public static class CensusResponseFactory
{
    public static CensusResponse Create(CensusModel model, string? category, string dimension)
    {

        var response = CreateResponse<CensusResponse>(model, dimension, category);

        response.SchoolName = model.SchoolName;
        response.SchoolType = model.SchoolType;
        response.LAName = model.LAName;

        return response;
    }

    public static CensusHistoryResponse Create(CensusHistoryModel model, string dimension)
    {
        var response = CreateResponse<CensusHistoryResponse>(model, dimension);

        response.Year = model.Year;

        return response;
    }

    private static T CreateResponse<T>(CensusBaseModel model, string dimension, string? category = null) where T : CensusBaseResponse, new()
    {
        var response = new T
        {
            URN = model.URN,
            TotalPupils = model.TotalPupils
        };

        if (category is null or CensusCategories.WorkforceFte)
        {
            response.Workforce = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.WorkforceHeadcount, model.WorkforceFTE)
                : CalculateValue(model.WorkforceFTE, model, dimension);
        }

        if (category is null or CensusCategories.WorkforceHeadcount)
        {
            response.WorkforceHeadcount = CalculateValue(model.WorkforceHeadcount, model, dimension);
        }

        if (category is null or CensusCategories.TeachersQualified)
        {
            response.PercentTeacherWithQualifiedStatus = model.PercentTeacherWithQualifiedStatus;
        }

        if (category is null or CensusCategories.TeachersFte)
        {
            response.Teachers = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.TeachersHeadcount, model.TeachersFTE)
                : CalculateValue(model.TeachersFTE, model, dimension);
        }

        if (category is null or CensusCategories.SeniorLeadershipFte)
        {
            response.SeniorLeadership = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.SeniorLeadershipHeadcount, model.SeniorLeadershipFTE)
                : CalculateValue(model.SeniorLeadershipFTE, model, dimension);
        }

        if (category is null or CensusCategories.TeachingAssistantsFte)
        {
            response.TeachingAssistant = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.TeachingAssistantHeadcount, model.TeachingAssistantFTE)
                : CalculateValue(model.TeachingAssistantFTE, model, dimension);
        }

        if (category is null or CensusCategories.NonClassroomSupportStaffFte)
        {
            response.NonClassroomSupportStaff = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.NonClassroomSupportStaffHeadcount, model.NonClassroomSupportStaffFTE)
                : CalculateValue(model.NonClassroomSupportStaffFTE, model, dimension);
        }

        if (category is null or CensusCategories.AuxiliaryStaffFte)
        {
            response.AuxiliaryStaff = dimension == CensusDimensions.HeadcountPerFte
                ? CalculateValue(model.AuxiliaryStaffHeadcount, model.AuxiliaryStaffFTE)
                : CalculateValue(model.AuxiliaryStaffFTE, model, dimension);
        }

        return response;
    }

    private static decimal? CalculateValue(decimal? value, CensusBaseModel model, string dimension)
    {
        if (value == null)
        {
            return value;
        }

        return dimension switch
        {
            CensusDimensions.Total => value,
            CensusDimensions.HeadcountPerFte => CalculateValue(model.WorkforceHeadcount, value),
            CensusDimensions.PercentWorkforce => CalculateValue(value, model.WorkforceFTE) * 100,
            CensusDimensions.PupilsPerStaffRole => CalculateValue(model.TotalPupils, value),
            _ => null
        };
    }

    private static decimal? CalculateValue(decimal? numerator, decimal? denominator)
    {
        if (denominator == null)
        {
            return denominator;
        }

        return denominator != 0 ? numerator / denominator : 0;
    }
}

[ExcludeFromCodeCoverage]
public abstract record CensusBaseResponse
{
    public string? URN { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? Workforce { get; set; }
    public decimal? WorkforceHeadcount { get; set; }
    public decimal? Teachers { get; set; }
    public decimal? SeniorLeadership { get; set; }
    public decimal? TeachingAssistant { get; set; }
    public decimal? NonClassroomSupportStaff { get; set; }
    public decimal? AuxiliaryStaff { get; set; }
    public decimal? PercentTeacherWithQualifiedStatus { get; set; }
}

[ExcludeFromCodeCoverage]
public record CensusResponse : CensusBaseResponse
{
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

[ExcludeFromCodeCoverage]
public record CensusHistoryResponse : CensusBaseResponse
{
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}