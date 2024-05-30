using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Establishment.LocalAuthorities;

[ExcludeFromCodeCoverage]
[Table("LocalAuthority")]
public record LocalAuthority
{
    [ExplicitKey] public string? Code { get; set; }
    public string? Name { get; set; }
}