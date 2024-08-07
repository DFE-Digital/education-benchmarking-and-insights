using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using Platform.Functions.Extensions;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Platform.Api.Insight.Trusts;

[ExcludeFromCodeCoverage]
public record TrustCharacteristic
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? SchoolsInTrust { get; set; }
    public DateTime? OpenDate { get; set; }
    public decimal? PercentFreeSchoolMeals { get; set; }
    public decimal? PercentSpecialEducationNeeds { get; set; }
    public decimal? TotalInternalFloorArea { get; set; }

    [JsonIgnore]
    public string? PhasesCovered { get; set; }
    public TrustPhase[] Phases => PhasesCovered == null
        ? []
        : PhasesCovered.Contains("count")
            ? PhasesCovered.FromJson<TrustPhase[]>()
            : PhasesCovered.FromJson<string[]>().Select(p => new TrustPhase
            {
                Phase = p
            }).ToArray();
}

[ExcludeFromCodeCoverage]
public record TrustPhase
{
    public string? Phase { get; set; }
    public int? Count { get; set; }
}