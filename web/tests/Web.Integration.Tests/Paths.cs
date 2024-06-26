namespace Web.Integration.Tests;

public static class Paths
{
    public const string ServiceHome = "/";
    public const string FindOrganisation = "/find-organisation";
    public const string ServiceHelp = "/help-with-this-service";
    public const string SubmitEnquiry = "/submit-an-enquiry";
    public const string AskForHelp = "/ask-for-help";
    public const string Cookies = "/cookies";
    public const string Privacy = "/privacy";
    public const string Contact = "/contact";
    public const string Accessibility = "/accessibility";
    public const string Error = "/error";
    public const string DataSources = "/data-sources";

    public static string StatusError(int statusCode) => $"/error/{statusCode}";
    public static string SchoolHome(string? urn) => $"/school/{urn}";
    public static string SchoolSpendingComparison(string? urn) => $"/school/{urn}/spending-comparison";
    public static string TrustHome(string? companyNumber) => $"/trust/{companyNumber}";
    public static string TrustDetails(string? companyNumber) => $"/trust/{companyNumber}/details";
    public static string TrustComparison(string? companyNumber) => $"/trust/{companyNumber}/comparison";
    public static string TrustCensus(string? companyNumber) => $"/trust/{companyNumber}/census";
    public static string TrustFinancialPlanning(string? companyNumber) => $"/trust/{companyNumber}/financial-planning";
    public static string TrustForecast(string? companyNumber) => $"/trust/{companyNumber}/forecast";
    public static string SchoolComparatorSet(string? urn, string referrer) =>
        $"/school/{urn}/comparator-set?referrer={referrer}";
    public static string SchoolComparison(string? urn) => $"/school/{urn}/comparison";
    public static string SchoolComparisonCustomData(string? urn) => $"/school/{urn}/comparison/custom-data";
    public static string SchoolCensus(string? urn) => $"/school/{urn}/census";
    public static string SchoolCensusCustomData(string? urn) => $"/school/{urn}/census/custom-data";
    public static string SchoolSpendingCustomData(string? urn) => $"/school/{urn}/spending-and-costs/custom-data";
    public static string SchoolInvestigation(string? urn) => $"/school/{urn}/investigation";
    public static string SchoolFinancialPlanning(string? urn) => $"/school/{urn}/financial-planning";
    public static string SchoolHistory(string? urn) => $"/school/{urn}/history";
    public static string SchoolDetails(string? urn) => $"/school/{urn}/details";
    public static string SchoolCustomData(string? urn) => $"/school/{urn}/custom-data";
    public static string SchoolCustomisedData(string? urn) => $"/school/{urn}/customised-data";

    public static string SchoolFinancialPlanningStart(string? urn) => $"/school/{urn}/financial-planning/create/start";
    public static string SchoolFinancialPlanningSelectYear(string? urn) =>
        $"/school/{urn}/financial-planning/create/select-year";
    public static string SchoolFinancialPlanningTotalIncome(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create/total-income?year={year}";
    public static string SchoolFinancialPlanningPrePopulatedData(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create/pre-populate-data?year={year}";
    public static string SchoolFinancialPlanningTimetableCycle(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create/timetable-cycle?year={year}";
    public static string SchoolFinancialPlanningHelp(string? urn) => $"/school/{urn}/financial-planning/create/help";
    public static string SchoolFinancialPlanningTotalExpenditure(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create/total-expenditure?year={year}";
    public static string SchoolFinancialPlanningTotalTeacherCost(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create/total-teacher-costs?year={year}";
    public static string SchoolFinancialPlanningTotalNumberTeachers(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create/total-number-teachers?year={year}";
    public static string SchoolFinancialPlanningTotalEducationSupport(string? urn, int year) =>
        $"/school/{urn}/financial-planning/create/total-education-support?year={year}";
    public static string SchoolFinancialPlanningHasMixedAgeClasses(string? urn, int year) => $"/school/{urn}/financial-planning/create/primary-has-mixed-age-classes?year={year}";
    public static string SchoolFinancialPlanningMixedAgeClasses(string? urn, int year) => $"/school/{urn}/financial-planning/create/primary-mixed-age-classes?year={year}";
    public static string SchoolFinancialPlanningPupilFigures(string? urn, int year) => $"/school/{urn}/financial-planning/create/pupil-figures?year={year}";
    public static string SchoolFinancialPlanningPrimaryPupilFigures(string? urn, int year) => $"/school/{urn}/financial-planning/create/primary-pupil-figures?year={year}";
    public static string SchoolFinancialPlanningTeacherPeriodAllocation(string? urn, int year) => $"/school/{urn}/financial-planning/create/teacher-period-allocation?year={year}";
    public static string SchoolFinancialPlanningTeachingAssistantFigures(string? urn, int year) => $"/school/{urn}/financial-planning/create/teaching-assistant-figures?year={year}";
    public static string SchoolFinancialPlanningOtherTeachingPeriods(string? urn, int year) => $"/school/{urn}/financial-planning/create/other-teaching-periods?year={year}";
    public static string SchoolFinancialPlanningOtherTeachingPeriodsConfirm(string? urn, int year) => $"/school/{urn}/financial-planning/create/other-teaching-periods-confirmation?year={year}";
    public static string SchoolFinancialPlanningOtherTeachingPeriodsReview(string? urn, int year) => $"/school/{urn}/financial-planning/create/other-teaching-periods-review?year={year}";
    public static string SchoolFinancialPlanningManagementRoles(string? urn, int year) => $"/school/{urn}/financial-planning/create/management-roles?year={year}";
    public static string SchoolFinancialPlanningManagersPerRole(string? urn, int year) => $"/school/{urn}/financial-planning/create/managers-per-role?year={year}";
    public static string SchoolCustomDataFinancialData(string? urn) => $"/school/{urn}/custom-data/financial-data";
    public static string SchoolCustomDataNonFinancialData(string? urn) => $"/school/{urn}/custom-data/school-characteristics";
    public static string SchoolCustomDataWorkforceData(string? urn) => $"/school/{urn}/custom-data/workforce";

    public static string SchoolComparators(string? urn) => $"/school/{urn}/comparators";
    public static string SchoolComparatorsCreate(string? urn) => $"/school/{urn}/comparators/create";
    public static string SchoolComparatorsCreateBy(string? urn) => $"/school/{urn}/comparators/create/by";
    public static string SchoolComparatorsCreateByName(string? urn) => $"/school/{urn}/comparators/create/by/name";
    public static string SchoolComparatorsCreateByCharacteristic(string? urn) => $"/school/{urn}/comparators/create/by/characteristic";
    public static string SchoolComparatorsCreatePreview(string? urn) => $"/school/{urn}/comparators/create/preview";
    public static string SchoolComparatorsCreateSubmit(string? urn) => $"/school/{urn}/comparators/create/submit";
    public static string SchoolComparatorsRevert(string? urn) => $"/school/{urn}/comparators/revert";

    public static string TrustComparators(string? companyNumber) => $"/trust/{companyNumber}/comparators";
    public static string TrustComparatorsCreateBy(string? companyNumber) => $"/trust/{companyNumber}/comparators/create/by";
    public static string TrustComparatorsCreateByName(string? companyNumber) => $"/trust/{companyNumber}/comparators/create/by/name";
    public static string TrustComparatorsCreateByCharacteristic(string? companyNumber) => $"/trust/{companyNumber}/comparators/create/by/characteristic";
    public static string TrustComparatorsCreatePreview(string? companyNumber) => $"/trust/{companyNumber}/comparators/create/preview";
    public static string TrustComparatorsCreateSubmit(string? companyNumber) => $"/trust/{companyNumber}/comparators/create/submit";
    public static string TrustComparatorsRevert(string? companyNumber) => $"/trust/{companyNumber}/comparators/revert";

    public static string ApiSuggest(string search, string type) => $"api/suggest?search={search}&type={type}";
    public static string ApiExpenditure(string? type, string? id, string category, string dimension) => $"api/expenditure?type={type}&id={id}&category={category}&dimension={dimension}";
    public static string ApiCensus(string id, string type, string category, string dimension) => $"api/census?id={id}&type={type}&category={category}&dimension={dimension}";
    public static string LocalAuthorityHome(string? code) => $"/local-authority/{code}";
    public static string LocalAuthorityResources(string? code) => $"/local-authority/{code}/find-ways-to-spend-less";
    public static string ToAbsolute(this string path) => $"https://localhost{path}";
}