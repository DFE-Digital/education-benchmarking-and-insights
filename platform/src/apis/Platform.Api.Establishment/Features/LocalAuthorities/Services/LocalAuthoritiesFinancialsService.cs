using System.Threading.Tasks;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;

namespace Platform.Api.Establishment.Features.LocalAuthorities.Services;

public interface ILocalAuthoritiesFinancialsService
{
    Task<LocalAuthorityRanking> GetRanking(string sort);
}