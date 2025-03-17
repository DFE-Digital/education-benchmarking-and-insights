// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;

public record LocalAuthorityNumberOfPlans
{
    [JsonProperty("code")] // OpenApi model spec
    [JsonPropertyName("code")] // actual payload
    public string? LaCode { get; set; }
    public string? Name { get; set; }
    public decimal? Total { get; set; }
    public decimal? Mainstream { get; set; }
    public decimal? Resourced { get; set; }
    public decimal? Special { get; set; }
    public decimal? Independent { get; set; }
    public decimal? Hospital { get; set; }
    public decimal? Post16 { get; set; }
    public decimal? Other { get; set; }
}