using System.Threading.Tasks;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;

namespace Platform.Api.Establishment.Features.LocalAuthorities.Services;

public interface ILocalAuthorityRankingService
{
    Task<LocalAuthorityRanking> GetRanking(string sort);
}