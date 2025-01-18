using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
namespace Platform.Api.Establishment.Features.LocalAuthorities.Models;

[ExcludeFromCodeCoverage]
public record LocalAuthority
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public IEnumerable<LocalAuthoritySchool>? Schools { get; set; }
}