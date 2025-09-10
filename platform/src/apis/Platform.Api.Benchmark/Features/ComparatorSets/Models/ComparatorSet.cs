﻿using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.Features.ComparatorSets.Models;

[ExcludeFromCodeCoverage]
public record ComparatorSetSchool
{
    public string? URN { get; set; }

    public ComparatorSetIds? Pupil { get; set; }
    public ComparatorSetIds? Building { get; set; }
}

[ExcludeFromCodeCoverage]
[Table("UserDefinedSchoolComparatorSet")]
public record ComparatorSetUserDefinedSchool
{
    [ExplicitKey]
    public string? RunType { get; set; }

    [ExplicitKey]
    public string? RunId { get; set; }

    [ExplicitKey]
    public string? URN { get; set; }

    public ComparatorSetIds? Set { get; set; }
}

[ExcludeFromCodeCoverage]
[Table("UserDefinedTrustComparatorSet")]
public record ComparatorSetUserDefinedTrust
{
    [ExplicitKey]
    public string? RunType { get; set; }

    [ExplicitKey]
    public string? RunId { get; set; }

    [ExplicitKey]
    public string? CompanyNumber { get; set; }

    public ComparatorSetIds? Set { get; set; }
}