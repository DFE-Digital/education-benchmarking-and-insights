using System.Threading.Tasks;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;

public interface IEducationHealthCarePlansService
{
    Task<History<LocalAuthorityNumberOfPlansYear>> GetHistory(string[] codes);
}