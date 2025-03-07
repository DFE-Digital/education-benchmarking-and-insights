using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;

public interface IHighNeedsService
{
    Task<LocalAuthority<Models.HighNeeds>[]> Get(string[] codes, CancellationToken cancellationToken = default);
    Task<History<HighNeedsYear>?> GetHistory(string[] codes, CancellationToken cancellationToken = default);
}