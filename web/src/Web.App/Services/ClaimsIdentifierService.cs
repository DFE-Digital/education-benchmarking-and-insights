using Web.App.Domain;
using Web.App.Identity.Models;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IClaimsIdentifierService
{
    Task<(string[] schools, string[] trusts)> IdentifyValidClaims(Organisation? organisation);
}

public class ClaimsIdentifierService(IEstablishmentApi api) : IClaimsIdentifierService
{
    public async Task<(string[] schools, string[] trusts)> IdentifyValidClaims(Organisation? organisation)
    {
        var schools = Array.Empty<string>();
        var trusts = Array.Empty<string>();

        if (organisation is { Category.Id: { } orgCategoryId })
        {
            switch (orgCategoryId)
            {
                case OrganisationCategories.SingleAcademyTrust:
                case OrganisationCategories.MultiAcademyTrust:
                    (schools, trusts) = await HandleTrustSchoolsAsync(organisation);
                    break;
                case OrganisationCategories.LocalAuthority:
                    schools = await HandleLocalAuthoritySchoolsAsync(organisation);
                    break;
                default:
                    (schools, trusts) = await HandleAcademyTrustAsync(organisation);
                    break;
            }
        }

        return (schools, trusts);
    }

    private async Task<(string[] schools, string[] trusts)> HandleTrustSchoolsAsync(Organisation organisation)
    {
        var schools = Array.Empty<string>();
        var trusts = Array.Empty<string>();

        var companyNumber = organisation.CompanyRegistrationNumber?.ToString("00000000");
        if (companyNumber != null)
        {
            var query = new ApiQuery().AddIfNotNull("companyNumber", companyNumber);
            var trustSchools = await api.QuerySchools(query).GetResultOrDefault<School[]>() ?? [];
            trusts = [companyNumber];
            schools = trustSchools.Select(x => x.URN).OfType<string>().ToArray();
        }

        return (schools, trusts);
    }

    private async Task<string[]> HandleLocalAuthoritySchoolsAsync(Organisation organisation)
    {
        var schools = Array.Empty<string>();

        var laCode = organisation.EstablishmentNumber?.ToString("000");
        if (laCode != null)
        {
            var query = new ApiQuery().AddIfNotNull("laCode", laCode);
            var laSchools = await api.QuerySchools(query).GetResultOrDefault<School[]>() ?? [];
            schools = laSchools.Select(x => x.URN).OfType<string>().ToArray();
        }

        return schools;
    }

    private async Task<(string[] schools, string[] trusts)> HandleAcademyTrustAsync(Organisation organisation)
    {
        var urn = organisation.UrnValue.ToString();
        var schools = new[] { urn };
        var trusts = Array.Empty<string>();
        var school = await api.GetSchool(urn).GetResultOrDefault<School>();
        var companyNumber = school?.TrustCompanyNumber;

        if (companyNumber != null)
        {
            trusts = [companyNumber];
        }

        return (schools, trusts);
    }
}