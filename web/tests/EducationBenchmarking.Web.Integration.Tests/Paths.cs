namespace EducationBenchmarking.Web.Integration.Tests;

public static class Paths
{
    public const string ServiceHome = "/";
    public const string FindOrganisation = "/find-organisation";
    public const string Error = "/error";
    
    public static string StatusError(int statusCode) => $"/error/{statusCode}";
    public static string SchoolHome(string urn) => $"/school/{urn}";
    public static string SchoolComparatorSet(string urn, string referrer) => $"/school/{urn}/comparator-set?referrer={referrer}";
    public static string SchoolComparison(string urn) => $"/school/{urn}/comparison";
    public static string SchoolWorkforce(string urn) => $"/school/{urn}/workforce";
    public static string SchoolInvestigation(string urn) => $"/school/{urn}/investigation";
    public static string SchoolCurriculumPlanning(string urn) => $"/school/{urn}/financial-planning";
    public static string SchoolCurriculumPlanningStart(string urn) => $"/school/{urn}/financial-planning/start";
    public static string SchoolCurriculumPlanningSelectYear(string urn) => $"/school/{urn}/financial-planning/select-year";
    public static string SchoolCurriculumPlanningYear(string urn, int year) => $"/school/{urn}/financial-planning/{year}";
    public static string SchoolCurriculumPlanningTotalIncome(string urn, int year) => $"/school/{urn}/financial-planning/{year}/total-income";
    public static string SchoolCurriculumPlanningHelp(string urn) => $"/school/{urn}/financial-planning/help";
    public static string SchoolCurriculumPlanningTotalExpenditure(string urn, int year) => $"/school/{urn}/financial-planning/{year}/total-expenditure";
    public static string SchoolCurriculumPlanningTotalTeacherCost(string urn, int year) => $"/school/{urn}/financial-planning/{year}/total-teacher-cost";
    public static string SchoolCurriculumPlanningTotalNumberTeachers(string urn, int year) => $"/school/{urn}/financial-planning/{year}/total-number-teachers";

    
    public static string ToAbsolute(this string path)
    {
        return $"https://localhost{path}";
    }
}