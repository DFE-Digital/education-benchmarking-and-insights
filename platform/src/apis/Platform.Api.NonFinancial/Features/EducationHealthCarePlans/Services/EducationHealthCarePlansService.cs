using System.Threading;
using System.Threading.Tasks;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;

public interface IEducationHealthCarePlansService
{
    Task<LocalAuthorityNumberOfPlans[]> Get(string[] codes, string dimension, CancellationToken cancellationToken = default);
    Task<History<LocalAuthorityNumberOfPlansYear>> GetHistory(string[] codes, CancellationToken cancellationToken = default);
}