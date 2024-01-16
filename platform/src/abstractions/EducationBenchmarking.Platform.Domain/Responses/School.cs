using System;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Domain.DataObjects;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public class School : IEquatable<School>
{
    public string? Urn { get; set; }
    public string? Name { get; set; }
    public string? FinanceType { get; set; }
    public string? Kind { get; set; }
    public string? LaEstab { get; set; }
    public string? Street { get; set; }
    public string? Locality { get; set; }
    public string? Address3 { get; set; }
    public string? Town { get; set; }
    public string? County { get; set; }
    public string? Postcode { get; set; }
    public string? CompanyNumber { get; set; }
    public string? TrustOrCompanyName { get; set; }

    public bool Equals(School? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Urn == other.Urn && Name == other.Name && FinanceType == other.FinanceType && Kind == other.Kind;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((School)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Urn, Name, FinanceType, Kind);
    }

    public static School Create(EdubaseDataObject edubase)
    {
        return new School
        {
            Urn = edubase.URN.ToString(),
            Kind = edubase.TypeOfEstablishment,
            FinanceType = edubase.FinanceType,
            Name = edubase.EstablishmentName,
            CompanyNumber = edubase.CompanyNumber.ToString(),
            TrustOrCompanyName = edubase.TrustOrCompanyName
        };
    }
}