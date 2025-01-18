using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.Schools.Models;

[ExcludeFromCodeCoverage]
public record SchoolComparators
{
    public long? TotalSchools { get; init; }
    public IEnumerable<string> Schools { get; init; } = Array.Empty<string>();
}