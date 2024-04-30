using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Web.App.Identity;
using Web.App.Identity.Models;

namespace Web.App.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string UserId(this ClaimsPrincipal principal)
    {
        var email = principal.Claims.SingleOrDefault(c => c.Type == "email");

        return email != null
            ? email.Value
            : "anonymous";
    }

    public static Guid UserGuid(this ClaimsPrincipal principal)
    {
        if (principal.Identity is ClaimsIdentity identity)
        {
            return Guid.Parse(identity.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value)
                .Single());
        }

        throw new ArgumentNullException(nameof(identity));
    }

    public static Organisation Organisation(this ClaimsPrincipal principal)
    {
        if (principal.Identity is ClaimsIdentity identity)
        {
            var organisation = identity.Claims.Where(c => c.Type == ClaimNames.Organisation)
                .Select(c => c.Value)
                .FirstOrDefault();

            return string.IsNullOrWhiteSpace(organisation)
                ? throw new ArgumentNullException(nameof(organisation))
                : JsonConvert.DeserializeObject<Organisation>(organisation) ??
                  throw new ArgumentNullException(nameof(organisation));
        }

        throw new ArgumentNullException(nameof(identity));
    }

    public static ClaimsPrincipal SetOrganisation(this ClaimsPrincipal principal, Organisation org)
    {
        if (principal.Identity is ClaimsIdentity identity)
        {
            var organisations = identity.Claims.Where(c => c.Type == ClaimNames.Organisation).ToArray();
            if (organisations.Any())
            {
                foreach (var o in organisations)
                {
                    identity.RemoveClaim(o);
                }
            }

            identity.AddClaim(new Claim(ClaimNames.Organisation, JsonConvert.SerializeObject(org)));
        }

        return principal;
    }

    private static string UserName(this ClaimsPrincipal principal)
    {
        var firstName = principal.Claims.SingleOrDefault(c => c.Type == ClaimNames.GivenName)?.Value;
        var familyName = principal.Claims.SingleOrDefault(c => c.Type == ClaimNames.FamilyName)?.Value;

        return $"{firstName} {familyName}";
    }

    public static async Task<bool> SetActiveRole(this ClaimsPrincipal principal, HttpContext httpContext, string roleName)
    {
        if (principal.Identity is not ClaimsIdentity identity)
        {
            return false;
        }

        var roles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).ToArray();
        if (roles.All(role => role.Value != roleName))
        {
            return false;
        }

        var currentActiveRoles = principal.Claims.Where(c => c.Type == ClaimNames.ActiveRole || c.Type == ClaimNames.Operation).ToArray();
        if (currentActiveRoles.Any())
        {
            foreach (var activePrincipal in currentActiveRoles)
            {
                identity.RemoveClaim(activePrincipal);
            }
        }

        identity.AddClaim(new Claim(ClaimNames.ActiveRole, roleName));

        foreach (var op in Operations.ForRole(roleName))
        {
            identity.AddClaim(op);
        }

        await httpContext.SignInAsync(principal);
        return true;
    }

    private static IEnumerable<OperationClaim> GetOperations(this ClaimsPrincipal principal)
    {
        return principal.Claims.Where(c => c.Type == ClaimNames.Operation)
            .Select(c => Operations.Get(c.Value))
            .Distinct();
    }

    public static bool HasAllOperations(this ClaimsPrincipal principal, params string[] operationClaims)
    {
        var ops = principal.GetOperations().Select(r => r.Name).ToHashSet();
        return ops.IsProperSupersetOf(operationClaims);
    }

    public static bool HasOperation(this ClaimsPrincipal principal, OperationClaim operationClaim)
    {
        return principal.GetOperations().Any(o => o.Equals(operationClaim));
    }


    public static ClaimsPrincipal ApplyClaims(this ClaimsPrincipal principal, string? accessToken, IEnumerable<string> schools, IEnumerable<string> trusts)
    {
        if (principal.Identity is ClaimsIdentity identity)
        {
            foreach (var school in schools)
            {
                identity.AddClaim(new Claim(ClaimNames.Schools, school));
            }

            foreach (var trust in trusts)
            {
                identity.AddClaim(new Claim(ClaimNames.Trusts, trust));
            }

            identity.AddClaim(new Claim(ClaimNames.UserId, principal.UserId()));
            identity.AddClaim(new Claim(ClaimNames.Name, principal.UserName()));
            if (accessToken != null)
            {
                identity.AddClaim(new Claim(ClaimNames.AccessToken, accessToken));
            }

        }

        return principal;
    }
}