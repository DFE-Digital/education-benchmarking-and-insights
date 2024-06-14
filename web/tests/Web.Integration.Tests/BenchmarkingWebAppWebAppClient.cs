using System.Collections.Concurrent;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit.Abstractions;
namespace Web.Integration.Tests;

public class SchoolBenchmarkingWebAppClient : BenchmarkingWebAppClient
{
    public SchoolBenchmarkingWebAppClient(IMessageSink messageSink) : base(messageSink, auth =>
    {
        auth.URN = 12345;
    })
    {
    }
}

public abstract class BenchmarkingWebAppClient(IMessageSink messageSink, Action<TestAuthOptions>? authCfg = null)
    : WebAppClientBase<Program>(messageSink, authCfg)
{
    public Mock<IInsightApi> InsightApi { get; } = new();
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
    public Mock<IHttpContextAccessor> HttpContextAccessor { get; } = new();

    protected override void Configure(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSingleton(InsightApi.Object);
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
        services.AddSingleton(HttpContextAccessor.Object);
    }

    public BenchmarkingWebAppClient SetupEstablishment(SuggestOutput<Trust> trustTestData)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.SuggestTrusts(It.IsAny<string>(), It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(trustTestData));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(School school)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(school.URN)).ReturnsAsync(ApiResult.Ok(school));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(Trust trust)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetTrust(trust.CompanyNumber)).ReturnsAsync(ApiResult.Ok(trust));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(Trust trust, School[] schools)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetTrust(trust.CompanyNumber)).ReturnsAsync(ApiResult.Ok(trust));
        EstablishmentApi
            .Setup(api =>
                api.QuerySchools(It.Is<ApiQuery>(x =>
                    x.Any(q => q.Key == "companyNumber" && q.Value == trust.CompanyNumber))))
            .ReturnsAsync(ApiResult.Ok(schools));

        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(LocalAuthority authority)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetLocalAuthority(authority.Code)).ReturnsAsync(ApiResult.Ok(authority));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(LocalAuthority authority, School[] schools)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetLocalAuthority(authority.Code)).ReturnsAsync(ApiResult.Ok(authority));
        EstablishmentApi
            .Setup(api =>
                api.QuerySchools(It.Is<ApiQuery>(x => x.Any(q => q.Key == "laCode" && q.Value == authority.Code))))
            .ReturnsAsync(ApiResult.Ok(schools));

        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithNotFound()
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(It.IsAny<string>())).ReturnsAsync(ApiResult.NotFound);
        EstablishmentApi.Setup(api => api.GetTrust(It.IsAny<string>())).ReturnsAsync(ApiResult.NotFound);
        EstablishmentApi.Setup(api => api.GetLocalAuthority(It.IsAny<string>())).ReturnsAsync(ApiResult.NotFound);
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithException()
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(It.IsAny<string>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.GetTrust(It.IsAny<string>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.GetLocalAuthority(It.IsAny<string>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SuggestSchools(It.IsAny<string>(), It.IsAny<ApiQuery?>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SuggestTrusts(It.IsAny<string>(), It.IsAny<ApiQuery?>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SuggestLocalAuthorities(It.IsAny<string>(), It.IsAny<ApiQuery?>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupCensus(School school, Census census)
    {
        CensusApi.Reset();
        CensusApi.Setup(api => api.Get(school.URN, It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(census));
        return this;
    }

    public BenchmarkingWebAppClient SetupCensusWithException()
    {
        CensusApi.Reset();
        CensusApi.Setup(api => api.History(It.IsAny<string?>(), It.IsAny<ApiQuery?>())).Throws(new Exception());
        CensusApi.Setup(api => api.Query(It.IsAny<ApiQuery?>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupInsights(School school, Finances? finances = null,
        SchoolExpenditure? expenditure = null, FloorAreaMetric? floorAreaMetric = null)
    {
        InsightApi.Reset();
        MetricRagRatingApi.Reset();
        CustomDataApi.Reset();
        ExpenditureApi.Reset();

        CustomDataApi.Setup(api => api.UpsertSchoolAsync(It.IsAny<string>(), It.IsAny<PutCustomDataRequest>()))
            .ReturnsAsync(ApiResult.Ok);

        InsightApi.Setup(api => api.GetSchoolFinances(school.URN)).ReturnsAsync(ApiResult.Ok(finances));
        InsightApi.Setup(api => api.GetSchoolsExpenditure(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        InsightApi.Setup(api => api.GetCurrentReturnYears())
            .ReturnsAsync(ApiResult.Ok(new FinanceYears
            {
                Aar = 2022,
                Cfr = 2021
            }));
        MetricRagRatingApi.Setup(api => api.GetDefaultAsync(It.IsAny<ApiQuery?>()))
            .ReturnsAsync(ApiResult.Ok(Array.Empty<RagRating>()));
        ExpenditureApi.Setup(api => api.School(school.URN, It.IsAny<ApiQuery?>()))
            .ReturnsAsync(ApiResult.Ok(expenditure));
        InsightApi.Setup(api => api.GetSchoolFloorAreaMetric(school.URN)).ReturnsAsync(ApiResult.Ok(floorAreaMetric));
        return this;
    }

    public BenchmarkingWebAppClient SetupIncome(School school, Income? income = null)
    {
        IncomeApi.Reset();
        IncomeApi.Setup(api => api.School(school.URN, It.IsAny<ApiQuery?>()))
            .ReturnsAsync(ApiResult.Ok(income));
        return this;
    }

    public BenchmarkingWebAppClient SetupBalance(Trust trust, TrustBalance? balance = null)
    {
        BalanceApi.Reset();
        BalanceApi.Setup(api => api.Trust(trust.CompanyNumber, It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(balance ?? new TrustBalance()));
        return this;
    }

    public BenchmarkingWebAppClient SetupInsights(IEnumerable<RagRating>? ratings = null)
    {
        InsightApi.Reset();
        MetricRagRatingApi.Reset();
        InsightApi.Setup(api => api.GetSchoolFinances(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        MetricRagRatingApi.Setup(api => api.GetDefaultAsync(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(ratings ?? Array.Empty<RagRating>()));
        InsightApi.Setup(api => api.GetCurrentReturnYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears
        {
            Aar = 2022,
            Cfr = 2021
        }));
        return this;
    }

    public BenchmarkingWebAppClient SetupSchoolInsightApi(IEnumerable<SchoolCharacteristic>? characteristics = null)
    {
        SchoolInsightApi.Reset();
        SchoolInsightApi.Setup(api => api.GetCharacteristicsAsync(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(characteristics ?? Array.Empty<SchoolCharacteristic>()));
        return this;
    }

    public BenchmarkingWebAppClient SetupTrustInsightApi(IEnumerable<TrustCharacteristic>? characteristics = null)
    {
        TrustInsightApi.Reset();
        TrustInsightApi.Setup(api => api.GetCharacteristicsAsync(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(characteristics ?? Array.Empty<TrustCharacteristic>()));
        return this;
    }

    public BenchmarkingWebAppClient SetupComparatorApi(ComparatorSchools? comparatorSchools = null, ComparatorTrusts? comparatorTrusts = null)
    {
        ComparatorApi.Reset();
        ComparatorApi.Setup(api => api.CreateSchoolsAsync(It.IsAny<PostSchoolComparatorsRequest>())).ReturnsAsync(ApiResult.Ok(comparatorSchools));
        ComparatorApi.Setup(api => api.CreateTrustsAsync(It.IsAny<PostTrustComparatorsRequest>())).ReturnsAsync(ApiResult.Ok(comparatorTrusts));
        return this;
    }

    public BenchmarkingWebAppClient SetupComparatorSetApi()
    {
        ComparatorApi.Reset();
        ComparatorSetApi.Setup(api => api.UpsertUserDefinedSchoolAsync(It.IsAny<string>(), It.IsAny<PutComparatorSetUserDefinedRequest>())).ReturnsAsync(ApiResult.Ok());
        ComparatorSetApi.Setup(api => api.UpsertUserDefinedTrustAsync(It.IsAny<string>(), It.IsAny<PutComparatorSetUserDefinedRequest>())).ReturnsAsync(ApiResult.Ok());
        return this;
    }

    public BenchmarkingWebAppClient SetupBenchmarkWithException()
    {
        FinancialPlanApi.Reset();
        ComparatorSetApi.Reset();
        UserDataApi.Reset();
        ComparatorSetApi.Setup(api => api.GetDefaultSchoolAsync(It.IsAny<string>())).Throws(new Exception());
        FinancialPlanApi.Setup(api => api.UpsertAsync(It.IsAny<PutFinancialPlanRequest>())).Throws(new Exception());
        FinancialPlanApi.Setup(api => api.GetAsync(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupBenchmarkWithNotFound()
    {
        FinancialPlanApi.Reset();
        FinancialPlanApi.Setup(api => api.GetAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(ApiResult.NotFound());
        return this;
    }

    public BenchmarkingWebAppClient SetupBenchmark(School[] schools, FinancialPlanInput? plan = null)
    {
        FinancialPlanApi.Reset();
        ComparatorSetApi.Reset();
        UserDataApi.Reset();

        UserDataApi.Setup(api => api.GetAsync(new ApiQuery())).ReturnsAsync(ApiResult.Ok(Array.Empty<UserData>()));

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

        ComparatorSetApi
            .Setup(api => api.GetDefaultSchoolAsync(It.IsAny<string>()))
            .ReturnsAsync(ApiResult.Ok(new SchoolComparatorSet
            {
                Building = schools.Select(x => x.URN ?? "Missing urn"),
                Pupil = schools.Select(x => x.URN ?? "Missing urn")
            }));

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

        public Task LoadAsync(CancellationToken cancellationToken = new()) => throw new NotImplementedException();

        public Task CommitAsync(CancellationToken cancellationToken = new()) => throw new NotImplementedException();

        public bool TryGetValue(string key, out byte[]? value) => _items.TryGetValue(key, out value);

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