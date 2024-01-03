using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;

namespace EducationBenchmarking.Platform.Api.Insight.Models;

[ExcludeFromCodeCoverage]
public class SchoolWorkforce
{
    public string Urn { get; set; }
    public string Name { get; set; }
    public string FinanceType { get; set; }
    public string LocalAuthority { get; set; }
}


