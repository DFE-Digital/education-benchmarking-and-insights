using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Domain.DataObjects;

[ExcludeFromCodeCoverage]
public record FinancialPlanDataObject
{
    [JsonProperty("id")] public string? Id { get; set; }
    [JsonProperty("partitionKey")] public string? PartitionKey { get; set; }
    [JsonProperty("created")] public DateTimeOffset Created { get; set; }
    [JsonProperty("updatedAt")] public DateTimeOffset? UpdatedAt { get; set; }
    [JsonProperty("updatedBy")] public string? UpdatedBy { get; set; }
    [JsonProperty("createdBy")] public string? CreatedBy { get; set; }
    [JsonProperty("version")] public int Version { get; set; }
    [JsonProperty("totalIncome")] public decimal? TotalIncome { get; set; }
    [JsonProperty("totalExpenditure")] public decimal? TotalExpenditure { get; set; }
    [JsonProperty("totalTeacherCosts")] public decimal? TotalTeacherCosts { get; set; }
    [JsonProperty("totalNumberOfTeachersFte")] public decimal? TotalNumberOfTeachersFte { get; set; }
    [JsonProperty("educationSupportStaffCosts")] public decimal? EducationSupportStaffCosts { get; set; }
    [JsonProperty("useFigures")] public bool? UseFigures { get; set; }
    [JsonProperty("timetablePeriods")] public int? TimetablePeriods { get; set; }
    [JsonProperty("hasMixedAgeClasses")] public bool? HasMixedAgeClasses { get; set; }
    [JsonProperty("mixedAgeReceptionYear1")] public bool MixedAgeReceptionYear1 { get; set; }
    [JsonProperty("mixedAgeYear1Year2")] public bool MixedAgeYear1Year2 { get; set; }
    [JsonProperty("mixedAgeYear2Year3")] public bool MixedAgeYear2Year3 { get; set; }
    [JsonProperty("mixedAgeYear3Year4")] public bool MixedAgeYear3Year4 { get; set; }
    [JsonProperty("mixedAgeYear4Year5")] public bool MixedAgeYear4Year5 { get; set; }
    [JsonProperty("mixedAgeYear5Year6")] public bool MixedAgeYear5Year6 { get; set; }
    [JsonProperty("pupilsNursery")] public decimal? PupilsNursery { get; set; }
    [JsonProperty("pupilsMixedReceptionYear1")] public int? PupilsMixedReceptionYear1 { get; set; }
    [JsonProperty("pupilsMixedYear1Year2")] public int? PupilsMixedYear1Year2 { get; set; }
    [JsonProperty("pupilsMixedYear2Year3")] public int? PupilsMixedYear2Year3 { get; set; }
    [JsonProperty("pupilsMixedYear3Year4")] public int? PupilsMixedYear3Year4 { get; set; }
    [JsonProperty("pupilsMixedYear4Year5")] public int? PupilsMixedYear4Year5 { get; set; }
    [JsonProperty("pupilsMixedYear5Year6")] public int? PupilsMixedYear5Year6 { get; set; }
    [JsonProperty("pupilsReception")] public int? PupilsReception { get; set; }
    [JsonProperty("pupilsYear1")] public int? PupilsYear1 { get; set; }
    [JsonProperty("pupilsYear2")] public int? PupilsYear2 { get; set; }
    [JsonProperty("pupilsYear3")] public int? PupilsYear3 { get; set; }
    [JsonProperty("pupilsYear4")] public int? PupilsYear4 { get; set; }
    [JsonProperty("pupilsYear5")] public int? PupilsYear5 { get; set; }
    [JsonProperty("pupilsYear6")] public int? PupilsYear6 { get; set; }
    [JsonProperty("pupilsYear7")] public int? PupilsYear7 { get; set; }
    [JsonProperty("pupilsYear8")] public int? PupilsYear8 { get; set; }
    [JsonProperty("pupilsYear9")] public int? PupilsYear9 { get; set; }
    [JsonProperty("pupilsYear10")] public int? PupilsYear10 { get; set; }
    [JsonProperty("pupilsYear11")] public int? PupilsYear11 { get; set; }
    [JsonProperty("pupilsYear12")] public decimal? PupilsYear12 { get; set; }
    [JsonProperty("pupilsYear13")] public decimal? PupilsYear13 { get; set; }
    [JsonProperty("teachersNursery")] public int? TeachersNursery { get; set; }
    [JsonProperty("teachersMixedReceptionYear1")] public int? TeachersMixedReceptionYear1 { get; set; }
    [JsonProperty("teachersMixedYear1Year2")] public int? TeachersMixedYear1Year2 { get; set; }
    [JsonProperty("teachersMixedYear2Year3")] public int? TeachersMixedYear2Year3 { get; set; }
    [JsonProperty("teachersMixedYear3Year4")] public int? TeachersMixedYear3Year4 { get; set; }
    [JsonProperty("teachersMixedYear4Year5")] public int? TeachersMixedYear4Year5 { get; set; }
    [JsonProperty("teachersMixedYear5Year6")] public int? TeachersMixedYear5Year6 { get; set; }
    [JsonProperty("teachersReception")] public int? TeachersReception { get; set; }
    [JsonProperty("teachersYear1")] public int? TeachersYear1 { get; set; }
    [JsonProperty("teachersYear2")] public int? TeachersYear2 { get; set; }
    [JsonProperty("teachersYear3")] public int? TeachersYear3 { get; set; }
    [JsonProperty("teachersYear4")] public int? TeachersYear4 { get; set; }
    [JsonProperty("teachersYear5")] public int? TeachersYear5 { get; set; }
    [JsonProperty("teachersYear6")] public int? TeachersYear6 { get; set; }
    [JsonProperty("teachersYear7")] public int? TeachersYear7 { get; set; }
    [JsonProperty("teachersYear8")] public int? TeachersYear8 { get; set; }
    [JsonProperty("teachersYear9")] public int? TeachersYear9 { get; set; }
    [JsonProperty("teachersYear10")] public int? TeachersYear10 { get; set; }
    [JsonProperty("teachersYear11")] public int? TeachersYear11 { get; set; }
    [JsonProperty("teachersYear12")] public int? TeachersYear12 { get; set; }
    [JsonProperty("teachersYear13")] public int? TeachersYear13 { get; set; }
    [JsonProperty("assistantsMixedReceptionYear1")] public decimal? AssistantsMixedReceptionYear1 { get; set; }
    [JsonProperty("assistantsMixedYear1Year2")] public decimal? AssistantsMixedYear1Year2 { get; set; }
    [JsonProperty("assistantsMixedYear2Year3")] public decimal? AssistantsMixedYear2Year3 { get; set; }
    [JsonProperty("assistantsMixedYear3Year4")] public decimal? AssistantsMixedYear3Year4 { get; set; }
    [JsonProperty("assistantsMixedYear4Year5")] public decimal? AssistantsMixedYear4Year5 { get; set; }
    [JsonProperty("assistantsMixedYear5Year6")] public decimal? AssistantsMixedYear5Year6 { get; set; }
    [JsonProperty("assistantsNursery")] public decimal? AssistantsNursery { get; set; }
    [JsonProperty("assistantsReception")] public decimal? AssistantsReception { get; set; }
    [JsonProperty("assistantsYear1")] public decimal? AssistantsYear1 { get; set; }
    [JsonProperty("assistantsYear2")] public decimal? AssistantsYear2 { get; set; }
    [JsonProperty("assistantsYear3")] public decimal? AssistantsYear3 { get; set; }
    [JsonProperty("assistantsYear4")] public decimal? AssistantsYear4 { get; set; }
    [JsonProperty("assistantsYear5")] public decimal? AssistantsYear5 { get; set; }
    [JsonProperty("assistantsYear6")] public decimal? AssistantsYear6 { get; set; }
    [JsonProperty("otherTeachingPeriods")] public OtherTeachingPeriod[]? OtherTeachingPeriods { get; set; }

    public class OtherTeachingPeriod
    {
        [JsonProperty("periodName")] public string? PeriodName { get; set; }
        [JsonProperty("periodsPerTimetable")] public string? PeriodsPerTimetable { get; set; }
    }
}