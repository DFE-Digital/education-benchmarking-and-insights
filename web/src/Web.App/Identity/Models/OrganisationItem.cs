using System.Diagnostics.CodeAnalysis;

namespace Web.App.Identity.Models;

[ExcludeFromCodeCoverage]
public class OrganisationItem
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int Code { get; set; }
}