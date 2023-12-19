using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;

namespace EducationBenchmarking.Web.Services;

public interface IFinanceService
{
    Task<ApiResult> GetFinances(School school);
}

public class FinanceService : IFinanceService
{
    private readonly IInsightApi _insightApi;

    public FinanceService(IInsightApi insightApi)
    {
        _insightApi = insightApi;
    }

    public async Task<ApiResult> GetFinances(School school)
    {
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return await _insightApi.GetAcademyFinances(school.Urn);
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return await _insightApi.GetMaintainedSchoolFinances(school.Urn);
            default:
                throw new ArgumentOutOfRangeException(nameof(school.Kind));
        }
    }
}