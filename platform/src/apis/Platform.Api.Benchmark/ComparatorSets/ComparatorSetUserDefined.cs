﻿using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.ComparatorSets;

[ExcludeFromCodeCoverage]
[Table("UserDefinedComparatorSet")]
public record ComparatorSetUserDefined
{
    [ExplicitKey] public string? RunType { get; set; }
    [ExplicitKey] public string? RunId { get; set; }
    [ExplicitKey] public string? URN { get; set; }
    public string[] Set { get; set; } = Array.Empty<string>();
    public string? Status { get; set; } = "pending";
};