using System;
using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Api.Establishment.Models;

[ExcludeFromCodeCoverage]
public class Trust : IEquatable<Trust>
{
    public string? CompanyNumber { get; set; }
    public string? Name { get; set; }

    public bool Equals(Trust? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return CompanyNumber == other.CompanyNumber && Name == other.Name;
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Trust)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CompanyNumber, Name);
    }
}