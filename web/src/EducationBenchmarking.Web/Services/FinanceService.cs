using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Services;

public interface IFinanceService
{
    Task<Finances> GetFinances(School school);
    Task<Finances> GetFinances(Trust trust);
    Task<FinanceYears> GetYears();
}

public class FinanceService : IFinanceService
{
    private readonly IInsightApi _insightApi;

    public FinanceService(IInsightApi insightApi)
    {
        _insightApi = insightApi;
    }

    public async Task<FinanceYears> GetYears()
    {
        return await _insightApi.GetFinanceYears().GetResultOrThrow<FinanceYears>();
    }
    
    public async Task<Finances> GetFinances(School school)
    {
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return await _insightApi.GetAcademyFinances(school.Urn).GetResultOrThrow<Finances>();
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return await _insightApi.GetMaintainedSchoolFinances(school.Urn).GetResultOrThrow<Finances>();
            default:
                throw new ArgumentOutOfRangeException(nameof(school.Kind));
        }
    }
    
    public async Task<Finances> GetFinances(Trust trust)
    {
        return new Finances();
    }
}