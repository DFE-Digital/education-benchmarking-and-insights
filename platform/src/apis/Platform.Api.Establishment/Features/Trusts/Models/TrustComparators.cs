using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.Trusts.Models;

[ExcludeFromCodeCoverage]
public record TrustComparators
{
    public long? TotalTrusts { get; init; }
    public IEnumerable<string> Trusts { get; init; } = Array.Empty<string>();
}