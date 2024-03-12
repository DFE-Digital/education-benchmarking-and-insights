using System.Diagnostics.CodeAnalysis;

namespace Web.Identity.Models;

[ExcludeFromCodeCoverage]
public class Role
{
    public string Name { get; set; }
    public string Code { get; set; }
}