using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.School.Features.Census.Models;
using Platform.Domain;

namespace Platform.Api.School.Features.Census;

//TODO: Consider adding unit test coverage for mapper
[ExcludeFromCodeCoverage]
public static class Mapper
{
    public static CensusResponse MapToApiResponse(this CensusModelDto model, string? category = null)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new CensusResponse
        {
            URN = model.URN,
            SchoolName = model.SchoolName,
            SchoolType = model.SchoolType,
            LAName = model.LAName,
            TotalPupils = model.TotalPupils,
            Workforce = ShouldDisplay(category, Categories.Census.WorkforceFte) ? model.Workforce : null,
            WorkforceHeadcount = ShouldDisplay(category, Categories.Census.WorkforceHeadcount) ? model.WorkforceHeadcount : null,
            Teachers = ShouldDisplay(category, Categories.Census.TeachersFte) ? model.Teachers : null,
            SeniorLeadership = ShouldDisplay(category, Categories.Census.SeniorLeadershipFte) ? model.SeniorLeadership : null,
            TeachingAssistant = ShouldDisplay(category, Categories.Census.TeachingAssistantsFte) ? model.TeachingAssistant : null,
            NonClassroomSupportStaff = ShouldDisplay(category, Categories.Census.NonClassroomSupportStaffFte) ? model.NonClassroomSupportStaff : null,
            AuxiliaryStaff = ShouldDisplay(category, Categories.Census.AuxiliaryStaffFte) ? model.AuxiliaryStaff : null,
            PercentTeacherWithQualifiedStatus = ShouldDisplay(category, Categories.Census.TeachersQualified) ? model.PercentTeacherWithQualifiedStatus : null
        };
    }

    public static IEnumerable<CensusResponse> MapToApiResponse(this IEnumerable<CensusModelDto> models, string? category = null)
    {
        return models.Select(m => MapToApiResponse(m, category));
    }

    public static CensusHistoryResponse MapToApiResponse(this YearsModelDto years, IEnumerable<CensusHistoryModelDto> models)
    {
        return new CensusHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Rows = models.Select(x => x.MapToApiResponse())
        };
    }

    private static CensusHistoryRowResponse MapToApiResponse(this CensusHistoryModelDto model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new CensusHistoryRowResponse
        {
            Year = model.RunId,
            TotalPupils = model.TotalPupils,
            Workforce = model.Workforce,
            WorkforceHeadcount = model.WorkforceHeadcount,
            Teachers = model.Teachers,
            SeniorLeadership = model.SeniorLeadership,
            TeachingAssistant = model.TeachingAssistant,
            NonClassroomSupportStaff = model.NonClassroomSupportStaff,
            AuxiliaryStaff = model.AuxiliaryStaff,
            PercentTeacherWithQualifiedStatus = model.PercentTeacherWithQualifiedStatus
        };
    }

    private static bool ShouldDisplay(string? category, string match) => string.IsNullOrEmpty(category) || category == match;
}