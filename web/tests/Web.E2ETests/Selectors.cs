namespace Web.E2ETests;

public static class Selectors
{
    public const string H1 = "h1";
    public const string H2 = "h2";
    public const string H3 = "h3";

    public const string GovBreadcrumbs = ".govuk-breadcrumbs";
    public const string GovShowAllLinkText = ".govuk-accordion__show-all-text";
    public const string GovAccordionSection = ".govuk-accordion__section";
    public const string GovBackLink = ".govuk-back-link";
    public const string GovButton = ".govuk-button";
    public const string GovLink = ".govuk-link";
    public const string GovDetailsSummaryText = ".govuk-details__summary-text";
    public const string GovDetailsText = ".govuk-details__text";
    public const string GovRadios = ".govuk-radios";

    public const string ChangeSchoolLink = ":text('Change school')";
    public const string ModeChart = "#mode-chart";
    public const string ModeTable = "#mode-table";
    public const string SectionTable = ".govuk-accordion__section .govuk-table";
    public const string SectionHeadingTwo = "#accordion-heading-2";
    public const string SectionContentTwo = "#accordion-content-2";
    public const string ToggleSectionText = ".govuk-accordion__section-toggle-text";
    public const string Charts = ".recharts-surface";
    public const string Table = "table";
    public const string Button = "button";

    public const string ComparisonTables = "#compare-your-costs table.govuk-table";

    public const string TotalExpenditureSaveAsImage = "xpath=//*[@id='compare-your-costs']/div[2]/div[2]/button";
    public const string TotalExpenditureDimension = "#total-expenditure-dimension";
    public const string TotalExpenditureChart = "//*[@id=\"compare-your-costs\"]/div[3]/div/div[1]/div";

    public const string PremisesDimension = "#total-premises-staff-service-costs-dimension";

    public const string SchoolWorkforceDimension = "#school-workforce-dimension";
    public const string SchoolWorkforceSaveAsImage = "xpath=//*[@id='compare-your-workforce']/div[2]/div[2]/button";

    public const string TotalNumberOfTeacherDimension = "#total-teachers-dimension";
    public const string SeniorLeadershipDimension = "#senior-leadership-dimension";
    public const string TeachingAssistantDimension = "#teaching-assistants-dimension";
    public const string NonClassRoomSupportStaffDimension = "#nonclassroom-support-dimension";
    public const string AuxiliaryStaffDimension = "#auxiliary-staff-dimension";
    public const string SchoolWorkforceHeadcountDimension = "#headcount-dimension";
    public const string SchoolGiasPageLink = "a[data-id='gias-school-details']";

    public const string SchoolDetailsEmailAddress = ".govuk-summary-list__key:has-text('Contact email') + .govuk-summary-list__value";
    public const string SchoolSearchInput = "#school-input";
    public const string SchoolRadio = ".govuk-radios__input#school";

    public const string ReactChartContainer = ".recharts-responsive-container";
    public const string ReactChartStats = ".chart-stat-summary";


}