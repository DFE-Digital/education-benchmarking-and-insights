using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
namespace Web.App.Services;

public interface ISchoolBenchmarkingReportCardsService
{
    Task<bool> CanShowBrcForSchool(School school);
}

public class SchoolBenchmarkingReportCardsService(
    IExpenditureApi expenditureApi,
    IMetricRagRatingApi metricRagRatingApi,
    ILogger<SchoolBenchmarkingReportCardsService> logger) : ISchoolBenchmarkingReportCardsService
{
    public async Task<bool> CanShowBrcForSchool(School school)
    {
        using (logger.BeginScope(new
        {
            urn = school.URN
        }))
        {
            if (!string.IsNullOrEmpty(school.FederationLeadURN) && school.FederationLeadURN != school.URN)
            {
                logger.LogInformation("Cannot show BRC for non-lead federated school {urn}", school.URN);
                return false;
            }

            var expenditure = await expenditureApi.School(school.URN).GetResultOrDefault<SchoolExpenditure>();
            if (expenditure == null)
            {
                logger.LogInformation("Cannot show BRC for school {urn} with missing expenditure data", school.URN);
                return false;
            }

            if (expenditure.PeriodCoveredByReturn < 12)
            {
                logger.LogInformation("Cannot show BRC for part-year school {urn}", school.URN);
                return false;
            }

            var ratings = await metricRagRatingApi
                .GetDefaultAsync(new ApiQuery().AddIfNotNull("urns", school.URN))
                .GetResultOrDefault<RagRating[]>() ?? [];

            // ReSharper disable once InvertIf
            if (ratings.Length == 0)
            {
                logger.LogInformation("Cannot show BRC for school {urn} with missing default RAGs", school.URN);
                return false;
            }

            logger.LogInformation("School {urn} is valid for BRC", school.URN);
            return true;
        }
    }
}