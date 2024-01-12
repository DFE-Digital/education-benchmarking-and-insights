using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Identity.Models;

[ExcludeFromCodeCoverage]
public class DfeIdentity
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public IEnumerable<Role> Roles { get; set; }
}