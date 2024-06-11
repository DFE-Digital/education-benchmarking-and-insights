using System;
using Newtonsoft.Json;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Trusts;

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
    public string[] Phases => PhasesCovered == null ? Array.Empty<string>() : PhasesCovered.FromJson<string[]>();

}