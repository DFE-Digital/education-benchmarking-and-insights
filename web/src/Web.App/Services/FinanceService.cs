using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services
{
    public interface IFinanceService
    {
        Task<PagedResults<SchoolExpenditure>> GetExpenditure(IEnumerable<School> schools);
        Task<PagedResults<SchoolWorkforce>> GetWorkforce(IEnumerable<School> schools);
        Task<Finances> GetFinances(School school);
        Task<FinanceYears> GetYears();
    }

    public class FinanceService(IInsightApi insightApi) : IFinanceService
    {
        public async Task<FinanceYears> GetYears()
        {
            return await insightApi.GetFinanceYears().GetResultOrThrow<FinanceYears>();
        }

        public async Task<PagedResults<SchoolExpenditure>> GetExpenditure(IEnumerable<School> schools)
        {
            var query = BuildApiQueryFromComparatorSet(schools);
            return await insightApi.GetSchoolsExpenditure(query).GetPagedResultOrThrow<SchoolExpenditure>();
        }

        public async Task<PagedResults<SchoolWorkforce>> GetWorkforce(IEnumerable<School> schools)
        {
            var query = BuildApiQueryFromComparatorSet(schools);
            return await insightApi.GetSchoolsWorkforce(query).GetPagedResultOrThrow<SchoolWorkforce>();
        }

        public async Task<Finances> GetFinances(School school)
        {
            switch (school.FinanceType)
            {
                case EstablishmentTypes.Academies:
                    return await insightApi.GetAcademyFinances(school.Urn).GetResultOrThrow<Finances>();
                case EstablishmentTypes.Federation:
                case EstablishmentTypes.Maintained:
                    return await insightApi.GetMaintainedSchoolFinances(school.Urn).GetResultOrThrow<Finances>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(school.Kind));
            }
        }

        private static ApiQuery BuildApiQueryFromComparatorSet(IEnumerable<School> schools)
        {
            var array = schools.ToArray();
            var query = new ApiQuery().Page(1, array.Length);
            foreach (var school in array)
            {
                query.AddIfNotNull("urns", school.Urn);
            }

            return query;
        }
    }
}