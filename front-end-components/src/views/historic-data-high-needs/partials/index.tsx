import {
  LocalAuthoritySection251,
  LocalAuthorityEducationHealthCarePlan,
} from "src/services";
import {
  HistoricChartSection251Section,
  HistoricChartSend2Section,
} from "../types";

/* eslint-disable react-refresh/only-export-components */
export * from "src/views/historic-data-high-needs/partials/section-251-section.tsx";
export * from "src/views/historic-data-high-needs/partials/send-2-section.tsx";

export const section251Sections: HistoricChartSection251Section<LocalAuthoritySection251>[] =
  [
    {
      heading: "High needs amount per head of 2 to 18 population",
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
      heading:
        "High needs amount per head of 2 to 18 population: place funding split by phase (for mainstream) and type of institution (for specialist provision)",
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
          name: "PRU and alternative provision place funding per head 2-18",
          field: "placeFundingAlternativeProvision",
        },
      ],
    },
    {
      heading:
        "High needs amount per head of 2 to 18 population: top up funding (maintained schools, academies, free schools and colleges) split by phase (for mainstream) and type of institution (for specialist provision)",
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
        "High needs amount per head of 2 to 18 population: top up funding (non-maintained schools and independent schools and colleges) split by phase (for mainstream) and type of institution (for specialist provision)",
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

export const send2LeadSection: HistoricChartSend2Section<LocalAuthorityEducationHealthCarePlan> =
  {
    charts: [
      {
        name: "Number aged up to 25 with SEN statement or EHC plan (per 1000 of 2 to 18 population)",
        field: "total",
      },
    ],
  };
export const send2AccordionSection: HistoricChartSend2Section<LocalAuthorityEducationHealthCarePlan> =
  {
    heading:
      "Placement of pupils aged up to 25 with SEN statement or EHC plan (per 1000 of 2 to 18 population)",
    charts: [
      {
        name: "Mainstream schools or academies",
        field: "mainstream",
      },
      {
        name: "Resourced provision or SEN units",
        field: "resourced",
      },
      {
        name: "Maintained special schools or special academies",
        field: "special",
      },
      {
        name: "NMSS or independent schools",
        field: "independent",
      },
      {
        name: "Hospital schools or alternative provisions",
        field: "hospital",
      },
      {
        name: "Post 16",
        field: "post16",
      },
      {
        name: "Other",
        field: "other",
      },
    ],
  };
