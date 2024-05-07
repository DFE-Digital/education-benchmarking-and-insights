using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Establishment.Db;

[ExcludeFromCodeCoverage]
[Table("LocalAuthority")]
public record LocalAuthorityDataObject
{
    [ExplicitKey] public string? Code { get; set; }
    public string? Name { get; set; }
}