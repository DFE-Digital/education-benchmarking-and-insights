using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ServiceName = "Education benchmarking and insights";
    public const string CorrelationIdHeader = "X-Correlation-ID";
    
    public const string SchoolOrganisationType = "school";
    public const string TrustOrganisationType = "trust";
    
    public const string InsightApi = "Insight";
    public const string EstablishmentApi = "Establishment";
    public const string BenchmarkApi = "Benchmark";
    
    public const string SectionInsightApi = "Apis:Insight";
    public const string SectionEstablishmentApi = "Apis:Establishment";
    public const string SectionBenchmarkApi = "Apis:Benchmark";
   
    public static string GiasSchoolUrl(string? urn) => $"https://www.get-information-schools.service.gov.uk/establishments/establishment/details/{urn}";
    public static string GiasTrustUrl(string? uid) => $"https://www.get-information-schools.service.gov.uk/groups/group/details/{uid}";
    
    public static int CurrentYear => DateTime.UtcNow.Month < 9 ? DateTime.UtcNow.Year - 1 : DateTime.UtcNow.Year;
    public static IEnumerable<int> AvailableYears => Enumerable.Range(CurrentYear, 4).ToArray();
}

public static class Referrers
{
    public const string SchoolComparison = "school-comparison";
    public const string SchoolWorkforce = "school-workforce";
}

public static class SessionKeys
{
    public static string SchoolComparatorSet(string urn) => $"school-comparator-set-{urn}";
}
public static class ViewDataConstants
{
    public const string Backlink = nameof(Backlink);
    public const string Title = nameof(Title);
    public const string BreadcrumbNode = nameof(BreadcrumbNode);
}

public static class PageTitleConstants
{
    public const string ServiceHome = "Home";
    public const string ErrorNotFound = "Page not found";
    public const string ErrorProblem = "Sorry, there is a problem with the service";
    public const string ErrorAccessDenied = "Access denied"; 
    public const string FindOrganisation = "Find an educational organisation";
    public const string ChooseOrganisation = "Choose your school, academy trust, local authority";
    public const string ContactDetails = "Contact details";
    public const string SchoolHome = "Your school";
    public const string SchoolComparison = "Compare your costs";
    public const string SchoolWorkforce = "Benchmark workforce data";
    public const string SchoolPlanning = "Curriculum and financial planning (CFP)";
    public const string SchoolPlanningYearSelect = "Select academic year to plan";
    public const string SchoolPlanningYear = "Prepopulated data";
    public const string SchoolPlanningHelp = "Data required for curriculum and financial planning (CFP)";
    public const string SchoolPlanningTotalIncome = "Total income";
    public const string SchoolPlanningTotalExpenditure = "Total expenditure";
    public const string SchoolPlanningTotalTeacherCosts = "Total teacher costs";
    public const string SchoolPlanningTotalNumberTeachers = "Total number of teachers";
    public const string SchoolPlanningTotalEducationSupport = "What is your total spend on education support staff?";
    public const string SchoolComparatorSet = "Comparator set";
    public const string TrustHome = "Your trust";
    public const string HistoricData = "Historic data";
}

public static class FeatureFlags
{
    public const string CurriculumFinancialPlanning = nameof(CurriculumFinancialPlanning);
}