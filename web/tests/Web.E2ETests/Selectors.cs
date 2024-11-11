namespace Web.E2ETests;

public static class Selectors
{
    public const string H1 = "h1";
    public const string H2 = "h2";
    public const string H3 = "h3";

    //public const string GovBreadcrumbs = ".govuk-breadcrumbs";
    public const string GovShowAllLinkText = ".govuk-accordion__show-all-text";
    public const string GovAccordionSection = ".govuk-accordion__section";
    //public const string GovBackLink = ".govuk-back-link";
    public const string GovButton = "main .govuk-button";
    public const string GovLink = "main .govuk-link";
    public const string GovDetailsSummaryText = ".govuk-details__summary-text";
    public const string GovDetailsText = ".govuk-details__text";
    public const string GovRadios = ".govuk-radios";
    public const string GovTable = ".govuk-table";
    public const string GovHeadingM = ".govuk-heading-m";
    public const string GovWarning = ".govuk-warning-text";

    public const string ChangeSchoolLink = ":text('Change school')";
    public const string ModeChart = "#mode-chart";
    public const string ModeTable = "#mode-table";
    public const string SectionTable = ".govuk-accordion__section .govuk-table";
    public const string SectionHeading1 = "#accordion-heading-1";
    public const string SectionContent1 = "#accordion-content-1";

    public const string SectionHeading2 = "#accordion-heading-2";
    public const string SectionContent2 = "#accordion-content-2";

    public const string SectionHeading3 = "#accordion-heading-3";
    public const string SectionContent3 = "#accordion-content-3";

    public const string SectionHeading4 = "#accordion-heading-4";
    public const string SectionContent4 = "#accordion-content-4";

    public const string SectionHeading5 = "#accordion-heading-5";
    public const string SectionContent5 = "#accordion-content-5";

    public const string SectionHeading6 = "#accordion-heading-6";
    public const string SectionContent6 = "#accordion-content-6";

    public const string SectionHeading7 = "#accordion-heading-7";
    public const string SectionContent7 = "#accordion-content-7";

    public const string SectionHeading8 = "#accordion-heading-8";
    public const string SectionContent8 = "#accordion-content-8";

    public const string SectionHeading9 = "#accordion-heading-9";
    public const string SectionContent9 = "#accordion-content-9";

    public const string ToggleSectionText = ".govuk-accordion__section-toggle-text";
    public const string Charts = ".recharts-surface";
    public const string Table = "table";
    public const string Button = "button";

    public const string ComparisonChartsAndTables = "#compare-your-costs";
    public const string ComparisonTables = "#compare-your-costs table.govuk-table";

    public const string TotalExpenditureSaveAsImage = "xpath=//*[@id='compare-your-costs']/div[2]/div[2]/button";
    public const string TotalExpenditureDimension = "#total-expenditure-dimension";
    public const string TotalExpenditureChart = "//*[@id=\"compare-your-costs\"]/div[3]/div/div[1]/div";

    public const string PremisesDimension = "#total-premises-staff-service-costs-dimension";

    public const string SchoolWorkforceDimension = "#school-workforce-dimension";
    public const string SchoolWorkforceSaveAsImage = "xpath=//*[@id='compare-your-census']/div[2]/div[2]/button";

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
    public const string AdditionalDetailsPopUps = ".recharts-wrapper .recharts-tooltip-wrapper";
    public const string SchoolNamesLinksInCharts = ".recharts-text .govuk-link";
    public const string ChartBars = ".recharts-surface path.recharts-rectangle.chart-cell";
    public const string ChartYTicks = ".recharts-surface .recharts-cartesian-axis.recharts-yAxis .recharts-cartesian-axis-ticks .recharts-cartesian-axis-tick";

    public const string SchoolSuggestDropdown = "#school-input__listbox";
    public const string MainContent = "#main-content";
    public const string GovukTag = ".govuk-tag";

    public const string RecommendedResources = "#recommended";
    public const string AllResources = "#all";
    public const string AllResourcesTab = "#tab_all";
    public const string AllCommercialLinks = "#all .govuk-link";

    public const string SpendingHistoryTab = "#tab_spending";
    public const string ExpenditureDimension = "#expenditure-dimension";
    public const string ExpenditureModeTable = "#expenditure-mode-table";
    public const string ExpenditureModeChart = "#expenditure-mode-chart";
    public const string IncomeModeTable = "#income-mode-table";
    public const string IncomeModeChart = "#income-mode-chart";
    public const string BalanceModeTable = "#balance-mode-table";
    public const string BalanceModeChart = "#balance-mode-chart";
    public const string CensusModeTable = "#census-mode-table";
    public const string CensusModeChart = "#census-mode-chart";

    public const string IncomeDimension = "#income-dimension";
    public const string BalanceDimension = "#balance-dimension";
    public const string CensusDimension = "#census-dimension";
    public const string AccordionHeadingText = ".govuk-accordion__section-heading-text";
    public const string IncomeHistoryTab = "#tab_Income";
    public const string SpendingAccordions = "#accordion-expenditure";
    public const string IncomeAccordions = "#accordion-income";
    public const string SpendingTableMode = "#expenditure-mode-table";
    public const string IncomeTableMode = "#income-mode-table";
    public const string BalanceTableMode = "#balance-mode-table";
    public const string CensusTableMode = "#census-mode-table";
    public const string SpendingPanel = "#spending";
    public const string IncomePanel = "#income";
    public const string BalancePanel = "#balance";
    public const string CensusPanel = "#census";


    public const string SpendingAccordionHeading2 = "#accordion-expenditure-heading-2";
    public const string SpendingAccordionContent2 = "#accordion-expenditure-content-2";

    public const string LineChartStats = ".chart-stat-line-chart";

    public const string CookieBanner = "#cookies-banner";
    public const string CookieBannerButtonFormat = "#{0}-cookies";
    public const string CookieBannerDismissedFormat = "#{0}ed-cookies-banner";
    public const string CookiesSavedBanner = "#cookies-saved-banner";
    public const string CookieFormRadioFormat = "#AnalyticsCookiesEnabled-{0}";
    public const string CookieFormButton = "#cookie-settings-button";

    public const string TypeGross = "#type-gross";
    public const string TypeNet = "#type-net";
    public const string CateringStaffAndServicesDimension = "#total-catering-costs-dimension";
    public const string CateringStaffAndServicesTables = "#catering-staff-and-supplies table";

    public const string ChartTooltips = ".recharts-tooltip-wrapper";
    public const string RunningCostCategoriesTab = "#tab_running";
    public const string BuildingCostCategoriesTab = "#tab_building";

    public const string SignInLink = "header a.app-signin:text-is('Sign in')";
    public const string SignOutLink = "header a.app-signin:text-is('Sign out')";

    public const string BrcIntroduction = "#introduction";
    public const string BrcKeyInformation = "#key-information-section";
    public const string BrcPriorityAreas = "#priority-areas-all-schools-section";
    public const string BrcOtherPriorityAreas = "#priority-areas-other-section";
    public const string BrcPupilWorkforce = "#pupil-workforce-metrics-section";
    public const string BrcNextSteps = "#next-steps-section";
    public const string KeyInformationContent = "ul.app-headline.app-headline-4 li";
    public const string PriorityAreaTeachingSupportStaff = "#spending-priorities-teaching-and-teaching-support-staff";
    public const string PriorityAreaNonEducationSupportStaff = "#spending-priorities-non-educational-support-staff-and-services";
    public const string PriorityAreaAdministrativeSupplies = "#spending-priorities-administrative-supplies";
}