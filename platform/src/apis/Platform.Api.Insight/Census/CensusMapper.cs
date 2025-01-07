using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Api.Insight.Census;

public static class CensusMapper
{
    public static CensusSchoolResponse MapToApiResponse(this CensusSchoolModel model, string? category = null)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");

        return new CensusSchoolResponse
        {
            URN = model.URN,
            SchoolName = model.SchoolName,
            SchoolType = model.SchoolType,
            LAName = model.LAName,
            TotalPupils = model.TotalPupils,
            Workforce = category is null or CensusCategories.WorkforceFte ? model.Workforce : null,
            WorkforceHeadcount = category is null or CensusCategories.WorkforceHeadcount ? model.WorkforceHeadcount : null,
            Teachers = category is null or CensusCategories.TeachersFte ? model.Teachers : null,
            SeniorLeadership = category is null or CensusCategories.SeniorLeadershipFte ? model.SeniorLeadership : null,
            TeachingAssistant = category is null or CensusCategories.TeachingAssistantsFte ? model.TeachingAssistant : null,
            NonClassroomSupportStaff = category is null or CensusCategories.NonClassroomSupportStaffFte ? model.NonClassroomSupportStaff : null,
            AuxiliaryStaff = category is null or CensusCategories.AuxiliaryStaffFte ? model.AuxiliaryStaff : null,
            PercentTeacherWithQualifiedStatus = category is null or CensusCategories.TeachersQualified ? model.PercentTeacherWithQualifiedStatus : null,
        };
    }

    public static IEnumerable<CensusSchoolResponse> MapToApiResponse(this IEnumerable<CensusSchoolModel> models, string? category = null)
    {
        return models.Select(m => MapToApiResponse(m, category));
    }

    public static CensusHistoryResponse MapToApiResponse(this IEnumerable<CensusHistoryModel> models, int startYear, int endYear)
    {
        return new CensusHistoryResponse
        {
            StartYear = startYear,
            EndYear = endYear,
            Rows = models.Select(x => x.MapToApiResponse())
        };
    }

    private static CensusHistoryRowResponse MapToApiResponse(this CensusHistoryModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");

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
}