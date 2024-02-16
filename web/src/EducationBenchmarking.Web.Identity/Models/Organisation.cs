using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace EducationBenchmarking.Web.Identity.Models;

[ExcludeFromCodeCoverage]
public class Organisation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public OrganisationItem Category { get; set; }
    public OrganisationItem Type { get; set; }
    public int? URN { get; set; }
    public int? UID { get; set; }
    public int? UKPRN { get; set; }
    public int? EstablishmentNumber { get; set; }
    public OrganisationItem Status { get; set; }
    public string ClosedOn { get; set; }
    public string Telephone { get; set; }
    public OrganisationItem Region { get; set; }
    public OrganisationItem LocalAuthority { get; set; }
    public OrganisationItem PhaseOfEducation { get; set; }
    public int? StatutoryLowAge { get; set; }
    public int? StatutoryHighAge { get; set; }
    public int? LegacyId { get; set; }
    public int? CompanyRegistrationNumber { get; set; }

    [JsonIgnore] public UrnValue UrnValue => URN;
}

[ExcludeFromCodeCoverage]
public readonly struct UrnValue(int value)
{
    private int Value { get; init; } = value;

    public override string ToString()
    {
        return Value.ToString("000000");
    }

    public static implicit operator UrnValue(int? urn) => new() { Value = urn ?? default };

    public static implicit operator string(UrnValue urn) => urn.ToString();
}