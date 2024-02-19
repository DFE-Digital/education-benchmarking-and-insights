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
    [JsonProperty("nixedAgeYear5Year6")] public bool MixedAgeYear5Year6 { get; set; }
}