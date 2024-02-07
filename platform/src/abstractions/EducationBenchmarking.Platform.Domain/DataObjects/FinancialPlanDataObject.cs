using System;
using Newtonsoft.Json;

namespace EducationBenchmarking.Platform.Domain.DataObjects;

public record FinancialPlanDataObject
{
    [JsonProperty("id")] public string? Id { get; set; }
    [JsonProperty("partitionKey")] public string? PartitionKey { get; set; }
    [JsonProperty("created")] public DateTimeOffset Created { get; set; }
    [JsonProperty("updatedAt")] public DateTimeOffset? UpdatedAt { get; set; }
    [JsonProperty("updatedBy")] public string? UpdatedBy { get; set; }
    [JsonProperty("createdBy")] public string? CreatedBy { get; set; }
    [JsonProperty("version")] public int Version { get; set; }
    [JsonProperty("totalIncome")] public string? TotalIncome { get; set; }
    [JsonProperty("totalExpenditure")] public string? TotalExpenditure { get; set; }
    [JsonProperty("totalTeacherCosts")] public string? TotalTeacherCosts { get; set; }
    [JsonProperty("totalNumberOfTeachersFte")] public string? TotalNumberOfTeachersFte { get; set; }
    [JsonProperty("educationSupportStaffCosts")] public string? EducationSupportStaffCosts { get; set; }
    [JsonProperty("useFigures")] public bool UseFigures { get; set; }
}