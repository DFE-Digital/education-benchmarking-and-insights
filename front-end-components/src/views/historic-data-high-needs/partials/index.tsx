import { LocalAuthoritySection251 } from "src/services";
import { HistoricChartSection251Section } from "../types";

/* eslint-disable react-refresh/only-export-components */
export * from "src/views/historic-data-high-needs/partials/section-251-section.tsx";
export * from "src/views/historic-data-high-needs/partials/send-2-section.tsx";

const summary =
  "Split by phase (for mainstream) and type of institution (specialist provision)";

export const section251Sections: HistoricChartSection251Section<LocalAuthoritySection251>[] =
  [
    {
      heading: "High needs amount per head 2-18 population",
      charts: [
        {
          name: "Total place funding for special schools and AP/PRUs",
          field: "highNeedsAmountTotalPlaceFunding",
        },
        {
          name: "Top up funding (maintained schools, academies, free schools and colleges)",
          field: "highNeedsAmountTopUpFundingMaintained",
        },
        {
          name: "Top up funding (non-maintained and independent schools and colleges)",
          field: "highNeedsAmountTopUpFundingNonMaintained",
        },
        {
          name: "SEN support and inclusion services",
          field: "highNeedsAmountSenServices",
        },
        {
          name: "Alternative provision services",
          field: "highNeedsAmountAlternativeProvisionServices",
        },
        {
          name: "Hospital education services",
          field: "highNeedsAmountHospitalServices",
        },
        {
          name: "Therapies and other health related services",
          field: "highNeedsAmountOtherHealthServices",
        },
      ],
    },
    {
      heading: "Place funding",
      summary,
      charts: [
        {
          name: "Primary place funding per head 2-18 population",
          field: "placeFundingPrimary",
        },
        {
          name: "Secondary place funding per head 2-18 population",
          field: "placeFundingSecondary",
        },
        {
          name: "Special place funding per head 2-18 population",
          field: "placeFundingSpecial",
        },
        {
          name: "Alternative provision place funding per head 2-18 population",
          field: "placeFundingAlternativeProvision",
        },
      ],
    },
    {
      heading:
        "Top up funding (maintained schools, academies, free schools and colleges)",
      summary,
      charts: [
        {
          name: "Early years top up funding per head 2-18 population (maintained)",
          field: "maintainedEarlyYears",
        },
        {
          name: "Primary top up funding per head 2-18 population (maintained)",
          field: "maintainedPrimary",
        },
        {
          name: "Secondary top up funding per head 2-18 population (maintained)",
          field: "maintainedSecondary",
        },
        {
          name: "Special top up funding per head 2-18 population (maintained)",
          field: "maintainedSpecial",
        },
        {
          name: "Alternative provision top up funding per head 2-18 population (maintained)",
          field: "maintainedAlternativeProvision",
        },
        {
          name: "Post-school top up funding per head 2-18 population (maintained)",
          field: "maintainedPostSchool",
        },
        {
          name: "Top up funding income per head 2-18 population (maintained)",
          field: "maintainedIncome",
        },
      ],
    },
    {
      heading:
        "Top up funding (non-maintained schools, and independent schools and colleges)",
      summary,
      charts: [
        {
          name: "Early years top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedEarlyYears",
        },
        {
          name: "Primary top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedPrimary",
        },
        {
          name: "Secondary top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedSecondary",
        },
        {
          name: "Special top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedSpecial",
        },
        {
          name: "Alternative provision top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedAlternativeProvision",
        },
        {
          name: "Post-school top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedPostSchool",
        },
        {
          name: "Top up funding income per head 2-18 population (non-maintained)",
          field: "nonMaintainedIncome",
        },
      ],
    },
  ];
