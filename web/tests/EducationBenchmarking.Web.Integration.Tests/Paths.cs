namespace EducationBenchmarking.Web.Integration.Tests;

public static class Paths
{
    public const string ServiceHome = "/";
    public const string FindOrganisation = "/find-organisation";
    public const string ServiceHelp = "/help-with-this-service";
    public const string SubmitEnquiry = "/submit-an-enquiry";
    public const string AskForHelp = "/ask-for-help";
    public const string CommercialResources = "/find-commercial-resources";
    public const string Error = "/error";

    public static string StatusError(int statusCode) => $"/error/{statusCode}";
    public static string SchoolHome(string? urn) => $"/school/{urn}";
    public static string TrustHome(string? companyNumber) => $"/trust/{companyNumber}";

    public static string SchoolComparatorSet(string? urn, string referrer) =>
        $"/school/{urn}/comparator-set?referrer={referrer}";
    public static string SchoolComparison(string? urn) => $"/school/{urn}/comparison";
    public static string SchoolWorkforce(string? urn) => $"/school/{urn}/workforce";
    public static string SchoolInvestigation(string? urn) => $"/school/{urn}/investigation";
    public static string SchoolFinancialPlanning(string? urn) => $"/school/{urn}/financial-planning";
    public static string SchoolHistory(string? urn) => $"/school/{urn}/history";
    public static string SchoolDetails(string? urn) => $"/school/{urn}/details";

    public static string SchoolFinancialPlanningStart(string? urn) => $"/school/{urn}/financial-planning/create?step=start";
    public static string SchoolFinancialPlanningSelectYear(string? urn) =>
        $"/school/{urn}/financial-planning/create?step=select-year";
    public static string SchoolFinancialPlanningTotalIncome(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create?year={year}&step=total-income";
    public static string SchoolFinancialPlanningPrePopulatedData(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create?year={year}&step=pre-populate-data";
    public static string SchoolFinancialPlanningTimetableCycle(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create?year={year}&step=timetable-cycle";
    public static string SchoolFinancialPlanningHelp(string? urn) => $"/school/{urn}/financial-planning/create?step=help";
    public static string SchoolFinancialPlanningTotalExpenditure(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create?year={year}&step=total-expenditure";
    public static string SchoolFinancialPlanningTotalTeacherCost(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create?year={year}&step=total-teacher-costs";
    public static string SchoolFinancialPlanningTotalNumberTeachers(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create?year={year}&step=total-number-teachers";
    public static string SchoolFinancialPlanningTotalEducationSupport(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create?year={year}&step=total-education-support";
    public static string SchoolFinancialPlanningHasMixedAgeClasses(string? urn, int year) => $"/school/{urn}/financial-planning/create?year={year}&step=primary-has-mixed-age-classes";
    public static string SchoolFinancialPlanningMixedAgeClasses(string? urn, int year) => $"/school/{urn}/financial-planning/create?year={year}&step=primary-mixed-age-classes";
    public static string SchoolFinancialPlanningPupilFigures(string? urn, int year) => $"/school/{urn}/financial-planning/create?year={year}&step=pupil-figures";
    public static string SchoolFinancialPlanningPrimaryPupilFigures(string? urn, int year) => $"/school/{urn}/financial-planning/create?year={year}&step=primary-pupil-figures";

    public static string ApiEstablishmentSuggest(string search, string type) => $"api/establishments/suggest?search={search}&type={type}";
    public static string ApiEstablishmentExpenditure(string? type, string? id) => $"api/establishments/expenditure?type={type}&id={id}";
    public static string ApiEstablishmentWorkforce(string? type, string? id) => $"api/establishments/workforce?type={type}&id={id}";

    public static string ToAbsolute(this string path)
    {
        return $"https://localhost{path}";
    }
}