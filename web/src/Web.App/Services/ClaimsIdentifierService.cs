using Web.App.Domain;
using Web.App.Identity.Models;
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
                    (schools, trusts) = await HandleSchoolAsync(organisation);
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
            var trust = await api.GetTrust(companyNumber).GetResultOrDefault<Trust>();
            trusts = [companyNumber];
            schools = trust?.Schools.Select(x => x.URN).OfType<string>().ToArray() ?? [];
        }

        return (schools, trusts);
    }

    private async Task<string[]> HandleLocalAuthoritySchoolsAsync(Organisation organisation)
    {
        var schools = Array.Empty<string>();

        var laCode = organisation.EstablishmentNumber?.ToString("000");
        if (laCode != null)
        {
            var la = await api.GetLocalAuthority(laCode).GetResultOrDefault<LocalAuthority>();
            schools = la?.Schools.Select(x => x.URN).OfType<string>().ToArray() ?? [];
        }

        return schools;
    }

    private async Task<(string[] schools, string[] trusts)> HandleSchoolAsync(Organisation organisation)
    {
        var urn = organisation.UrnValue.ToString();
        var schools = new[]
        {
            urn
        };
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