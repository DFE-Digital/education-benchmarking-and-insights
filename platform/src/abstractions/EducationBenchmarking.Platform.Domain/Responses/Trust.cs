using System;
using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record Trust
{
    public string? CompanyNumber { get; set; }
    public string? Name { get; set; }
}