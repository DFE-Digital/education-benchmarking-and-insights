using EducationBenchmarking.Web.Identity.Models;

namespace EducationBenchmarking.Web.Identity;

internal class RoleOperationMap : Dictionary<string, List<OperationClaim>>
{
    public void Add(string role, params OperationClaim[] ops)
    {
        Add(role, ops.Distinct().ToList());
    }
}

public static class Operations
{
    public static readonly OperationClaim AClaim = new(nameof(AClaim), "Context", "Some claim");
    
    private static readonly RoleOperationMap RoleOperationMap = new()
    {
        {
            RoleNames.SomeUserRole,
            AClaim
        }
    };

    public static readonly string[] AllRoles = RoleOperationMap.Keys.ToArray();
        
    public static readonly OperationClaim[] All = RoleOperationMap.Values.SelectMany(v => v).ToArray();


    public static IEnumerable<OperationClaim> ForRole(string role)
    {
        if (RoleOperationMap.TryGetValue(role, out var ops))
        {
            return ops;
        }

        return Enumerable.Empty<OperationClaim>();
    }

    public static OperationClaim Get(string op)
    {
        var opAsClaim = (OperationClaim) op;
        return All.FirstOrDefault(o => o.Equals(opAsClaim)) ??
               throw new ArgumentOutOfRangeException($"Unable to find operation {op}");
    }
        
    public static IEnumerable<Role> GetAllRoles()
    {
        return RoleOperationMap.Keys.Select(r => new Role { Name = r, Code = r });
    }
}