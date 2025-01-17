using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Domain;

namespace Platform.Api.Insight.Features.Census;

//TODO: Consider adding unit test coverage for mapper
[ExcludeFromCodeCoverage]
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
            Workforce = category is null or Categories.Census.WorkforceFte ? model.Workforce : null,
            WorkforceHeadcount = category is null or Categories.Census.WorkforceHeadcount ? model.WorkforceHeadcount : null,
            Teachers = category is null or Categories.Census.TeachersFte ? model.Teachers : null,
            SeniorLeadership = category is null or Categories.Census.SeniorLeadershipFte ? model.SeniorLeadership : null,
            TeachingAssistant = category is null or Categories.Census.TeachingAssistantsFte ? model.TeachingAssistant : null,
            NonClassroomSupportStaff = category is null or Categories.Census.NonClassroomSupportStaffFte ? model.NonClassroomSupportStaff : null,
            AuxiliaryStaff = category is null or Categories.Census.AuxiliaryStaffFte ? model.AuxiliaryStaff : null,
            PercentTeacherWithQualifiedStatus = category is null or Categories.Census.TeachersQualified ? model.PercentTeacherWithQualifiedStatus : null,
        };
    }

    public static IEnumerable<CensusSchoolResponse> MapToApiResponse(this IEnumerable<CensusSchoolModel> models, string? category = null)
    {
        return models.Select(m => MapToApiResponse(m, category));
    }

    public static CensusHistoryResponse MapToApiResponse(this CensusYearsModel years, IEnumerable<CensusHistoryModel> models)
    {
        return new CensusHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
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