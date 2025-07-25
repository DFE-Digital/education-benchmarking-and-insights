using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Moq;
using Web.App;
using Web.App.Cache;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.Content;
using Web.App.Domain.LocalAuthorities;
using Web.App.Domain.NonFinancial;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Apis.Content;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Apis.NonFinancial;
using Web.App.Infrastructure.Storage;
using Web.App.Services;
using Xunit.Abstractions;
using File = Web.App.Domain.Content.File;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Web.Integration.Tests;

public class SchoolBenchmarkingWebAppClient : BenchmarkingWebAppClient
{
    public SchoolBenchmarkingWebAppClient(IMessageSink messageSink) : base(messageSink, auth =>
    {
        auth.URN = 123456;
        auth.CompanyNumber = 87654321;
    })
    {
    }
}

public abstract class BenchmarkingWebAppClient(IMessageSink messageSink, Action<TestAuthOptions>? authCfg = null)
    : WebAppClientBase<Program>(messageSink, authCfg)
{
    public Mock<IYearsApi> YearsApi { get; } = new();
    public Mock<IEstablishmentApi> EstablishmentApi { get; } = new();
    public Mock<IFinancialPlanApi> FinancialPlanApi { get; } = new();
    public Mock<IComparatorApi> ComparatorApi { get; } = new();
    public Mock<IComparatorSetApi> ComparatorSetApi { get; } = new();
    public Mock<ICensusApi> CensusApi { get; } = new();
    public Mock<IIncomeApi> IncomeApi { get; } = new();
    public Mock<IMetricRagRatingApi> MetricRagRatingApi { get; } = new();
    public Mock<IUserDataApi> UserDataApi { get; } = new();
    public Mock<IBalanceApi> BalanceApi { get; } = new();
    public Mock<ISchoolInsightApi> SchoolInsightApi { get; } = new();
    public Mock<ITrustInsightApi> TrustInsightApi { get; } = new();
    public Mock<ICustomDataApi> CustomDataApi { get; } = new();
    public Mock<IExpenditureApi> ExpenditureApi { get; } = new();
    public Mock<IBudgetForecastApi> BudgetForecastApi { get; } = new();
    public Mock<IHttpContextAccessor> HttpContextAccessor { get; } = new();
    public Mock<IDataSourceStorage> DataSourceStorage { get; } = new();
    public Mock<IFeatureManager> FeatureManager { get; } = new();
    public Mock<IFilesApi> FilesApi { get; } = new();
    public Mock<ILocalAuthoritiesApi> LocalAuthoritiesApi { get; } = new();
    public Mock<IEducationHealthCarePlansApi> EducationHealthCarePlansApi { get; } = new();
    public Mock<ILocalAuthorityComparatorSetService> LocalAuthorityComparatorSetService { get; } = new();
    public Mock<IChartRenderingApi> ChartRenderingApi { get; } = new();
    public Mock<ICommercialResourcesApi> CommercialResourcesApi { get; } = new();
    public Mock<IBannerApi> BannerApi { get; } = new();
    public IOptions<CacheOptions> CacheOptions { get; } = Options.Create(new CacheOptions
    {
        ReturnYears = new CacheSettings { Disabled = true },
        CommercialResources = new CacheSettings { Disabled = true },
        Banners = new CacheSettings { Disabled = true }
    });

    protected override void Configure(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSingleton(YearsApi.Object);
        services.AddSingleton(EstablishmentApi.Object);
        services.AddSingleton(FinancialPlanApi.Object);
        services.AddSingleton(ComparatorApi.Object);
        services.AddSingleton(ComparatorSetApi.Object);
        services.AddSingleton(UserDataApi.Object);
        services.AddSingleton(CensusApi.Object);
        services.AddSingleton(IncomeApi.Object);
        services.AddSingleton(MetricRagRatingApi.Object);
        services.AddSingleton(BalanceApi.Object);
        services.AddSingleton(SchoolInsightApi.Object);
        services.AddSingleton(TrustInsightApi.Object);
        services.AddSingleton(CustomDataApi.Object);
        services.AddSingleton(ExpenditureApi.Object);
        services.AddSingleton(BudgetForecastApi.Object);
        services.AddSingleton(HttpContextAccessor.Object);
        services.AddSingleton(DataSourceStorage.Object);
        services.AddSingleton(FeatureManager.Object);
        services.AddSingleton(FilesApi.Object);
        services.AddSingleton(LocalAuthoritiesApi.Object);
        services.AddSingleton(EducationHealthCarePlansApi.Object);
        services.AddSingleton(LocalAuthorityComparatorSetService.Object);
        services.AddSingleton(ChartRenderingApi.Object);
        services.AddSingleton(CommercialResourcesApi.Object);
        services.AddSingleton(BannerApi.Object);
        services.AddSingleton(CacheOptions);

        EnableFeatures();
    }

    private void EnableFeatures(params string[] ignoreFeatures)
    {
        var features = new[]
        {
            FeatureFlags.CurriculumFinancialPlanning,
            FeatureFlags.CustomData,
            FeatureFlags.UserDefinedComparators,
            FeatureFlags.TrustComparison,
            FeatureFlags.Trusts,
            FeatureFlags.LocalAuthorities,
            FeatureFlags.ForecastRisk,
            FeatureFlags.FinancialBenchmarkingInsightsSummary,
            FeatureFlags.HistoricalTrends,
            FeatureFlags.HighExecutivePay,
            FeatureFlags.HighNeeds,
            FeatureFlags.FilteredSearch,
            FeatureFlags.SchoolSpendingPrioritiesSsrCharts,
            FeatureFlags.CfrItSpendBreakdown
        };

        foreach (var feature in features.Where(x => !ignoreFeatures.Contains(x)))
        {
            FeatureManager.Setup(fm => fm.IsEnabledAsync(feature))
                .ReturnsAsync(true);
        }
    }

    public BenchmarkingWebAppClient SetupDisableFeatureFlags(params string[] features)
    {
        FeatureManager.Reset();

        EnableFeatures(features);

        foreach (var feature in features)
        {
            FeatureManager.Setup(fm => fm.IsEnabledAsync(feature))
                .ReturnsAsync(false);
        }

        return this;
    }

    public BenchmarkingWebAppClient SetupStorage()
    {
        var sharedAccessTokenModel = new SharedAccessTokenModel
        {
            ContainerUri = new Uri("https://teststorageaccount.net/testcontainer"),
            SasToken = "test"
        };

        DataSourceStorage.Reset();
        DataSourceStorage.Setup(storage => storage.GetAccessToken()).Returns(sharedAccessTokenModel);

        return this;
    }

    public BenchmarkingWebAppClient SetupFiles(File[]? files = null)
    {
        FilesApi.Reset();
        FilesApi.Setup(api => api.GetTransparencyFiles()).ReturnsAsync(ApiResult.Ok(files));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(SuggestOutput<Trust> trustTestData)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.SuggestTrusts(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(trustTestData));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(School school)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(school.URN, It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(school));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(Trust trust)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetTrust(trust.CompanyNumber, It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(trust));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(Trust trust, TrustSchool[] schools)
    {
        EstablishmentApi.Reset();
        trust.Schools = schools;
        EstablishmentApi.Setup(api => api.GetTrust(trust.CompanyNumber, It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(trust));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(LocalAuthority authority)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetLocalAuthority(authority.Code, It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(authority));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(LocalAuthority authority, LocalAuthoritySchool[] schools)
    {
        EstablishmentApi.Reset();
        authority.Schools = schools;
        EstablishmentApi.Setup(api => api.GetLocalAuthority(authority.Code, It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(authority));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(
        LocalAuthority authority,
        LocalAuthorityRanking ranking,
        LocalAuthorityStatisticalNeighbours statisticalNeighbours,
        LocalAuthority[] authorities)
    {
        SetupEstablishment(authority, []);
        EstablishmentApi.Setup(api => api.GetLocalAuthoritiesNationalRank(It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(ranking));
        EstablishmentApi.Setup(api => api.GetLocalAuthorityStatisticalNeighbours(authority.Code, It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(statisticalNeighbours));
        EstablishmentApi.Setup(api => api.GetLocalAuthorities(It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(authorities));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(LocalAuthorityStatisticalNeighbours authority, LocalAuthority[] authorities)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetLocalAuthorityStatisticalNeighbours(authority.Code, It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(authority));
        EstablishmentApi.Setup(api => api.GetLocalAuthorities(It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(authorities));
        EstablishmentApi.Setup(api => api.GetLocalAuthority(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(authorities.First()));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithNotFound()
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.NotFound);
        EstablishmentApi.Setup(api => api.GetTrust(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.NotFound);
        EstablishmentApi.Setup(api => api.GetLocalAuthority(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.NotFound);
        EstablishmentApi.Setup(api => api.GetLocalAuthorityStatisticalNeighbours(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.NotFound);
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithException()
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.GetTrust(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.GetLocalAuthority(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.GetLocalAuthorityStatisticalNeighbours(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.GetLocalAuthorities(It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SuggestTrusts(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SuggestLocalAuthorities(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SearchSchools(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SearchTrusts(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SearchLocalAuthorities(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithTaskCanceledException()
    {
        EstablishmentApi.Reset();
        EstablishmentApi
            .Setup(api => api.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>(), It.IsAny<CancellationToken>()))
            .Throws(new TaskCanceledException());
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(SearchResponse<SchoolSummary> schools)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.SearchSchools(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(schools));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(SearchResponse<TrustSummary> trusts)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.SearchTrusts(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(trusts));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(SearchResponse<LocalAuthoritySummary> localAuthorities)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.SearchLocalAuthorities(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(localAuthorities));
        return this;
    }

    public BenchmarkingWebAppClient SetupCensus(School school, Census census)
    {
        CensusApi.Reset();
        CensusApi.Setup(api => api.Get(school.URN, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(census));
        return this;
    }

    public BenchmarkingWebAppClient SetupCensus(Census[] censuses)
    {
        CensusApi.Reset();
        CensusApi.Setup(api => api.Query(It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(censuses));
        return this;
    }

    public BenchmarkingWebAppClient SetupCensus(School school, CensusHistoryRows historySchool, CensusHistoryRows? historyComparatorSet = null, CensusHistoryRows? historyNational = null)
    {
        CensusApi.Reset();
        CensusApi
            .Setup(api => api.SchoolHistory(school.URN, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(historySchool));
        CensusApi
            .Setup(api => api.SchoolHistoryComparatorSetAverage(school.URN, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(historyComparatorSet));
        CensusApi
            .Setup(api => api.SchoolHistoryNationalAverage(It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(historyNational));
        return this;
    }

    public BenchmarkingWebAppClient SetupCensusWithException()
    {
        CensusApi.Reset();
        CensusApi
            .Setup(api => api.SchoolHistory(It.IsAny<string>(), It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());
        CensusApi.Setup(api => api.Query(It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        return this;
    }

    // public BenchmarkingWebAppClient SetupInsights(School school,SchoolBalance? balance = null, SchoolExpenditure? expenditure = null, SchoolCharacteristic? characteristic = null)
    // {
    //     InsightApi.Reset();
    //     MetricRagRatingApi.Reset();
    //     CustomDataApi.Reset();
    //     ExpenditureApi.Reset();
    //     BalanceApi.Reset();
    //     SchoolInsightApi.Reset();
    //     
    //     
    //     CustomDataApi.Setup(api => api.UpsertSchoolAsync(It.IsAny<string>(), It.IsAny<PutCustomDataRequest>()))
    //         .ReturnsAsync(ApiResult.Ok);
    //
    //     BalanceApi.Setup(api => api.School(school.URN, It.IsAny<ApiQuery?>())).ReturnsAsync();
    //     ExpenditureApi.Setup(api => api.School(school.URN, It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(expenditure));
    //     ExpenditureApi.Setup(api => api.QuerySchools(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
    //     InsightApi.Setup(api => api.GetCurrentReturnYears())
    //         .ReturnsAsync(ApiResult.Ok(new FinanceYears
    //         {
    //             Aar = 2022,
    //             Cfr = 2021
    //         }));
    //     MetricRagRatingApi.Setup(api => api.GetDefaultAsync(It.IsAny<ApiQuery?>()))
    //         .ReturnsAsync(ApiResult.Ok(Array.Empty<RagRating>()));
    //     ExpenditureApi.Setup(api => api.School(school.URN, It.IsAny<ApiQuery?>()))
    //         .ReturnsAsync(ApiResult.Ok(expenditure));
    //     SchoolInsightApi.Setup(api => api.GetCharacteristicsAsync(school.URN ?? "")).ReturnsAsync(ApiResult.Ok(characteristic));
    //     return this;
    // }

    public BenchmarkingWebAppClient SetupIncome(School school, SchoolIncome? income = null)
    {
        IncomeApi.Reset();
        IncomeApi.Setup(api => api.School(school.URN, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(income));
        return this;
    }

    public BenchmarkingWebAppClient SetupBalance(School school, SchoolBalance? balance = null)
    {
        BalanceApi.Reset();
        BalanceApi.Setup(api => api.School(school.URN, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(balance ?? new SchoolBalance()));
        return this;
    }

    public BenchmarkingWebAppClient SetupBalance(Trust trust, TrustBalance? balance = null)
    {
        BalanceApi.Reset();
        BalanceApi.Setup(api => api.Trust(trust.CompanyNumber, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(balance ?? new TrustBalance()));
        return this;
    }

    public BenchmarkingWebAppClient SetupBudgetForecast(
        Trust trust,
        BudgetForecastReturn[]? returns = null,
        BudgetForecastReturnMetric[]? metrics = null,
        int? currentYear = null)
    {
        BudgetForecastApi.Reset();
        BudgetForecastApi.Setup(api => api.BudgetForecastReturns(trust.CompanyNumber, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(returns ?? []));
        BudgetForecastApi.Setup(api => api.BudgetForecastReturnsMetrics(trust.CompanyNumber, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(metrics ?? []));
        BudgetForecastApi.Setup(api => api.GetCurrentBudgetForecastYear(trust.CompanyNumber, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(currentYear ?? 2022));
        return this;
    }

    public BenchmarkingWebAppClient SetupInsights()
    {
        YearsApi.Reset();

        YearsApi.Setup(api => api.GetCurrentReturnYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears
        {
            Aar = 2022,
            Cfr = 2021,
            S251 = 2024
        }));

        return this;
    }

    public BenchmarkingWebAppClient SetupBalance(SchoolBalance? balance = null)
    {
        BalanceApi.Reset();

        BalanceApi.Setup(api => api.School(It.IsAny<string?>(), It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(balance ?? new SchoolBalance
        {
            PeriodCoveredByReturn = 12
        }));

        return this;
    }

    public BenchmarkingWebAppClient SetupMetricRagRating(IEnumerable<RagRating>? ratings = null)
    {
        MetricRagRatingApi.Reset();

        MetricRagRatingApi.Setup(api => api.GetDefaultAsync(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(ratings ?? []));

        return this;
    }

    public BenchmarkingWebAppClient SetupMetricRagRatingUserDefined(IEnumerable<RagRating>? ratings = null)
    {
        MetricRagRatingApi.Reset();
        MetricRagRatingApi.Setup(api => api.UserDefinedAsync(It.IsAny<string>())).ReturnsAsync(ApiResult.Ok(ratings ?? []));
        return this;
    }

    public BenchmarkingWebAppClient SetupMetricRagRatingIncCustom(string customData, IEnumerable<RagRating> customRatings, IEnumerable<RagRating>? originalRatings = null)
    {
        MetricRagRatingApi.Reset();

        MetricRagRatingApi.Setup(api => api.GetDefaultAsync(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(originalRatings ?? []));
        MetricRagRatingApi.Setup(api => api.CustomAsync(customData)).ReturnsAsync(ApiResult.Ok(customRatings));

        return this;
    }

    public BenchmarkingWebAppClient SetupExpenditure(School school, SchoolExpenditure? expenditure = null)
    {
        ExpenditureApi.Reset();
        ExpenditureApi
            .Setup(api => api.School(school.URN, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(expenditure ?? new SchoolExpenditure
            {
                PeriodCoveredByReturn = 12
            }));
        return this;
    }

    public BenchmarkingWebAppClient SetupExpenditure(SchoolExpenditure[] expenditures)
    {
        ExpenditureApi.Reset();
        ExpenditureApi
            .Setup(api => api.QuerySchools(It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(expenditures));
        return this;
    }

    public BenchmarkingWebAppClient SetupExpenditureForCustomData(School school, string identifier, SchoolExpenditure expenditure)
    {
        ExpenditureApi.Reset();
        ExpenditureApi
            .Setup(api => api.SchoolCustom(school.URN, identifier, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(expenditure));
        return this;
    }

    public BenchmarkingWebAppClient SetupExpenditureForTrusts(TrustExpenditure[] expenditure)
    {
        ExpenditureApi.Reset();
        ExpenditureApi
            .Setup(api => api.QueryTrusts(It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(expenditure));
        return this;
    }

    public BenchmarkingWebAppClient SetupExpenditure(School school, ExpenditureHistoryRows historySchool, ExpenditureHistoryRows? historyComparatorSet = null, ExpenditureHistoryRows? historyNational = null)
    {
        ExpenditureApi.Reset();
        ExpenditureApi
            .Setup(api => api.SchoolHistory(school.URN, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(historySchool));
        ExpenditureApi
            .Setup(api => api.SchoolHistoryComparatorSetAverage(school.URN, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(historyComparatorSet));
        ExpenditureApi
            .Setup(api => api.SchoolHistoryNationalAverage(It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(historyNational));
        return this;
    }

    public BenchmarkingWebAppClient SetupSchoolInsight(School school, SchoolCharacteristic? characteristic = null)
    {
        SchoolInsightApi.Reset();
        SchoolInsightApi
            .Setup(api => api.GetCharacteristicsAsync(school.URN ?? ""))
            .ReturnsAsync(ApiResult.Ok(characteristic ?? new SchoolCharacteristic()));
        return this;
    }

    public BenchmarkingWebAppClient SetUpCustomData(CustomDataSchool? customData = null)
    {
        CustomDataApi.Reset();
        CustomDataApi.Setup(api => api.UpsertSchoolAsync(It.IsAny<string>(), It.IsAny<PostCustomDataRequest>())).ReturnsAsync(ApiResult.Ok());
        CustomDataApi.Setup(api => api.GetSchoolAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(ApiResult.Ok(customData));
        CustomDataApi.Setup(api => api.RemoveSchoolAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(ApiResult.Ok());
        return this;
    }

    public BenchmarkingWebAppClient SetupSchoolInsight(IEnumerable<SchoolCharacteristic>? characteristics = null)
    {
        SchoolInsightApi.Reset();
        SchoolInsightApi.Setup(api => api.GetCharacteristicsAsync(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(characteristics ?? []));
        return this;
    }

    public BenchmarkingWebAppClient SetupTrustInsightApi(IEnumerable<TrustCharacteristic>? characteristics = null)
    {
        TrustInsightApi.Reset();
        TrustInsightApi.Setup(api => api.GetCharacteristicsAsync(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(characteristics ?? []));
        return this;
    }

    public BenchmarkingWebAppClient SetupComparatorApi(ComparatorSchools? comparatorSchools = null, ComparatorTrusts? comparatorTrusts = null)
    {
        ComparatorApi.Reset();
        ComparatorApi.Setup(api => api.CreateSchoolsAsync(It.IsAny<string>(), It.IsAny<PostSchoolComparatorsRequest>())).ReturnsAsync(ApiResult.Ok(comparatorSchools));
        ComparatorApi.Setup(api => api.CreateTrustsAsync(It.IsAny<string>(), It.IsAny<PostTrustComparatorsRequest>())).ReturnsAsync(ApiResult.Ok(comparatorTrusts));
        return this;
    }

    public BenchmarkingWebAppClient SetupComparatorSetApi()
    {
        ComparatorApi.Reset();
        ComparatorSetApi.Setup(api => api.UpsertUserDefinedSchoolAsync(It.IsAny<string>(), It.IsAny<PostComparatorSetUserDefinedRequest>())).ReturnsAsync(ApiResult.Ok());
        ComparatorSetApi.Setup(api => api.UpsertUserDefinedTrustAsync(It.IsAny<string>(), It.IsAny<PostComparatorSetUserDefinedRequest>())).ReturnsAsync(ApiResult.Ok());
        ComparatorSetApi.Setup(api => api.RemoveUserDefinedSchoolAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(ApiResult.Ok());
        ComparatorSetApi.Setup(api => api.RemoveUserDefinedTrustAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(ApiResult.Ok());
        return this;
    }

    public BenchmarkingWebAppClient SetupComparatorSetApi(UserDefinedTrustComparatorSet trustComparatorSet)
    {
        ComparatorApi.Reset();
        ComparatorSetApi.Setup(api => api.GetUserDefinedTrustAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(trustComparatorSet));
        return this;
    }

    public BenchmarkingWebAppClient SetupComparatorSetApiWithException()
    {
        ComparatorSetApi.Reset();
        ComparatorSetApi.Setup(api => api.GetDefaultSchoolAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupComparatorSetApiWithTaskCanceledException()
    {
        ComparatorSetApi.Reset();
        ComparatorSetApi.Setup(api => api.GetDefaultSchoolAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws(new TaskCanceledException());
        return this;
    }

    public BenchmarkingWebAppClient SetupComparatorSet(School school, SchoolComparatorSet? comparatorSet)
    {
        ComparatorSetApi.Reset();
        ComparatorSetApi.Setup(api => api.GetDefaultSchoolAsync(school.URN!, It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(comparatorSet));
        return this;
    }

    // public BenchmarkingWebAppClient SetupBenchmarkWithNotFound()
    // {
    //     FinancialPlanApi.Reset();
    //     FinancialPlanApi.Setup(api => api.GetAsync(It.IsAny<string>(), It.IsAny<int>()))
    //         .ReturnsAsync(ApiResult.NotFound());
    //     return this;
    // }


    public BenchmarkingWebAppClient SetupFinancialPlan(FinancialPlanInput? plan = null)
    {
        FinancialPlanApi.Reset();

        if (plan == null)
        {
            FinancialPlanApi.Setup(api => api.GetAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(ApiResult.NotFound());
        }
        else
        {
            ArgumentNullException.ThrowIfNull(plan.Urn);
            FinancialPlanApi.Setup(api => api.GetAsync(plan.Urn, plan.Year)).ReturnsAsync(ApiResult.Ok(plan));
        }

        FinancialPlanApi
            .Setup(api => api.QueryAsync(It.IsAny<ApiQuery?>()))
            .ReturnsAsync(ApiResult.Ok(Array.Empty<FinancialPlanInput>()));

        FinancialPlanApi
            .Setup(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>()))
            .ReturnsAsync(ApiResult.Ok())
            .Callback<PutFinancialPlanRequest>(request =>
                FinancialPlanApi.Setup(api => api.GetAsync(request.Urn ?? "", request.Year))
                    .ReturnsAsync(ApiResult.Ok(new FinancialPlanInput
                    {
                        Urn = request.Urn,
                        Year = request.Year.GetValueOrDefault(),
                        UseFigures = request.UseFigures,
                        EducationSupportStaffCosts = request.EducationSupportStaffCosts.ToString(),
                        TotalIncome = request.TotalIncome.ToString(),
                        TotalExpenditure = request.TotalExpenditure.ToString(),
                        TotalTeacherCosts = request.TotalTeacherCosts.ToString(),
                        TotalNumberOfTeachersFte = request.TotalNumberOfTeachersFte,
                        HasMixedAgeClasses = request.HasMixedAgeClasses,
                        MixedAgeReceptionYear1 = request.MixedAgeReceptionYear1,
                        MixedAgeYear1Year2 = request.MixedAgeYear1Year2,
                        MixedAgeYear2Year3 = request.MixedAgeYear2Year3,
                        MixedAgeYear3Year4 = request.MixedAgeYear3Year4,
                        MixedAgeYear4Year5 = request.MixedAgeYear4Year5,
                        MixedAgeYear5Year6 = request.MixedAgeYear5Year6,
                        TimetablePeriods = request.TimetablePeriods.ToString()
                    })));
        return this;
    }

    public BenchmarkingWebAppClient SetupUserData(UserData[]? data = null)
    {
        UserDataApi.Reset();
        UserDataApi.Setup(api => api.GetAsync(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(data ?? []));
        return this;
    }

    public BenchmarkingWebAppClient SetupLocalAuthoritiesNationalRank(LocalAuthorityRanking ranking)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetLocalAuthoritiesNationalRank(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(ranking));
        return this;
    }

    public BenchmarkingWebAppClient SetupLocalAuthoritiesWithException()
    {
        LocalAuthoritiesApi.Reset();
        LocalAuthoritiesApi.Setup(api => api.GetHighNeeds(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        LocalAuthoritiesApi.Setup(api => api.GetHighNeedsHistory(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupHighNeeds(LocalAuthority<HighNeeds>[]? highNeeds, HighNeedsHistory<HighNeedsYear>? history)
    {
        LocalAuthoritiesApi.Reset();
        LocalAuthoritiesApi.Setup(api => api.GetHighNeeds(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(highNeeds ?? []));
        LocalAuthoritiesApi.Setup(api => api.GetHighNeedsHistory(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(history));
        return this;
    }

    public BenchmarkingWebAppClient SetupEducationHealthCarePlans(LocalAuthorityNumberOfPlans[]? plans, EducationHealthCarePlansHistory<LocalAuthorityNumberOfPlansYear>? history)
    {
        EducationHealthCarePlansApi.Reset();
        EducationHealthCarePlansApi.Setup(api => api.GetEducationHealthCarePlans(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(plans ?? []));
        EducationHealthCarePlansApi.Setup(api => api.GetEducationHealthCarePlansHistory(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(history));
        return this;
    }

    public BenchmarkingWebAppClient SetupEducationHealthCarePlansWithException()
    {
        EducationHealthCarePlansApi.Reset();
        EducationHealthCarePlansApi.Setup(api => api.GetEducationHealthCarePlans(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        EducationHealthCarePlansApi.Setup(api => api.GetEducationHealthCarePlansHistory(It.IsAny<ApiQuery>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupLocalAuthoritiesComparators(string code, string[] set)
    {
        LocalAuthorityComparatorSetService.Reset();
        LocalAuthorityComparatorSetService.Setup(s => s.ReadUserDefinedComparatorSetFromSession(code)).Returns(new UserDefinedLocalAuthorityComparatorSet { Set = set });
        return this;
    }

    public BenchmarkingWebAppClient SetupChartRendering<T>(ChartResponse verticalBarChart)
    {
        ChartResponse[] verticalBarCharts = [];
        ChartRenderingApi.Reset();
        ChartRenderingApi
            .Setup(api => api.PostVerticalBarCharts(It.IsAny<PostVerticalBarChartsRequest<T>>(), It.IsAny<CancellationToken>()))
            .Callback<PostVerticalBarChartsRequest<T>, CancellationToken>((request, _) =>
            {
                // cross-reference POST-ed Id with response Id
                verticalBarCharts = request
                    .Select(r => new ChartResponse { Id = r.Id, Html = verticalBarChart.Html })
                    .ToArray();
            })
            .ReturnsAsync(() => ApiResult.Ok(verticalBarCharts));
        ChartRenderingApi.Setup(api => api.PostVerticalBarChart(It.IsAny<PostVerticalBarChartRequest<T>>(), It.IsAny<CancellationToken>())).ReturnsAsync(ApiResult.Ok(verticalBarChart));
        return this;
    }

    //TODO: what kind of assertions are needed
    public BenchmarkingWebAppClient SetupCommercialResources(CommercialResourceCategorised[] resources)
    {
        CommercialResourcesApi.Reset();
        CommercialResourcesApi.Setup(api => api.GetCommercialResources()).ReturnsAsync(ApiResult.Ok(resources));
        return this;
    }

    public BenchmarkingWebAppClient SetupBanner(Banner? banner)
    {
        BannerApi.Reset();
        BannerApi.Setup(api => api.GetBanner(It.IsAny<string>())).ReturnsAsync(banner == null ? ApiResult.NotFound() : ApiResult.Ok(banner));
        return this;
    }

    public BenchmarkingWebAppClient SetupChartRenderingWithException<T>()
    {
        CensusApi.Reset();
        ChartRenderingApi.Setup(api => api.PostVerticalBarChart(It.IsAny<PostVerticalBarChartRequest<T>>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        ChartRenderingApi.Setup(api => api.PostVerticalBarCharts(It.IsAny<PostVerticalBarChartsRequest<T>>(), It.IsAny<CancellationToken>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupHttpContextAccessor(ConcurrentDictionary<string, byte[]>? items = null)
    {
        HttpContextAccessor.Reset();
        var session = new SessionStub(items);
        var context = new DefaultHttpContext
        {
            Session = session
        };

        HttpContextAccessor.Setup(a => a.HttpContext).Returns(context);
        return this;
    }

    private class SessionStub(ConcurrentDictionary<string, byte[]>? items = null) : ISession
    {
        private readonly ConcurrentDictionary<string, byte[]> _items = items ?? new ConcurrentDictionary<string, byte[]>();

        public Task LoadAsync(CancellationToken cancellationToken = new())
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new())
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out byte[] value)
        {
            return _items.TryGetValue(key, out value);
        }

        public void Set(string key, byte[] value)
        {
            _items[key] = value;
        }

        public void Remove(string key)
        {
            _items.Remove(key, out _);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool IsAvailable => true;

        public string Id => nameof(SessionStub);

        public IEnumerable<string> Keys => _items.Keys;
    }
}