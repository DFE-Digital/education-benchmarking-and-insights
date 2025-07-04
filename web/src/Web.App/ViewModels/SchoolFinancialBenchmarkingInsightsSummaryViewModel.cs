using Web.App.Domain;
using Web.App.Domain.Content;
using Web.App.Extensions;
namespace Web.App.ViewModels;

public class SchoolFinancialBenchmarkingInsightsSummaryViewModel(
    School school,
    FinanceYears years,
    SchoolBalance? balance,
    IEnumerable<RagRating>? ratings,
    IEnumerable<SchoolExpenditure>? pupilExpenditure,
    IEnumerable<SchoolExpenditure>? areaExpenditure,
    IEnumerable<Census>? census) : ISchoolKeyInformationViewModel
{
    private readonly string[] _allSchoolsCategories = [Category.TeachingStaff, Category.NonEducationalSupportStaff, Category.AdministrativeSupplies];
    private readonly CostCategory[] _categories = CategoryBuilder
        .Build(
            ratings ?? Array.Empty<RagRating>(),
            pupilExpenditure ?? Array.Empty<SchoolExpenditure>(),
            areaExpenditure ?? Array.Empty<SchoolExpenditure>()
        ).ToArray();

    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string Years => IsPartOfTrust ? years.Aar.ToFinanceYear() : years.Cfr.ToFinanceYear();
    public IEnumerable<CostCategory> CostsAllSchools => _categories
        .Where(x => _allSchoolsCategories.Contains(x.Rating.Category))
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);
    public IEnumerable<CostCategory> CostsOtherPriorities => _categories
        .Where(x => !_allSchoolsCategories.Contains(x.Rating.Category))
        .Where(x => x.Rating.Category is not Category.Other)
        .Where(x => x.Rating.RAG is not "green")
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);
    public SchoolFinancialBenchmarkingInsightsSummaryCensusViewModel PupilsPerTeacher => new("teacher", census, school.URN, c => c.Teachers);
    public SchoolFinancialBenchmarkingInsightsSummaryCensusViewModel PupilsPerSeniorLeadership => new("senior leadership role", census, school.URN, c => c.SeniorLeadership);
    public bool HasRagData => ratings?.Any() ?? false;
    public bool HasCensusData => census?.Any(c => c.URN == school.URN && c.TotalPupils.HasValue) ?? false;
    public string? OverallPhase => school.OverallPhase;
    public decimal? InYearBalance => balance?.InYearBalance;
    public decimal? RevenueReserve => balance?.RevenueReserve;
}