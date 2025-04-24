import {
  LocalAuthoritySection251,
  LocalAuthorityEducationHealthCarePlan,
} from "src/services";
import {
  BenchmarkChartSection251Section,
  BenchmarkChartSend2Section,
} from "../types";

/* eslint-disable react-refresh/only-export-components */
export * from "src/views/benchmark-data-high-needs/partials/section-251-section.tsx";
export * from "src/views/benchmark-data-high-needs/partials/send-2-section.tsx";

export const section251Sections: BenchmarkChartSection251Section<LocalAuthoritySection251>[] =
  [
    {
      heading: "High needs amount per head 2-18 population",
      charts: [
        {
          name: "Total place funding for special schools and AP/PRUs",
          field: "highNeedsAmountTotalPlaceFunding",
          lineCodes: ["1.0.2"],
        },
        {
          name: "Top up funding (maintained schools, academies, free schools and colleges)",
          field: "highNeedsAmountTopUpFundingMaintained",
          lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        },
        {
          name: "Top up funding (non-maintained and independent schools and colleges)",
          field: "highNeedsAmountTopUpFundingNonMaintained",
          lineCodes: ["1.2.3"],
        },
        {
          name: "SEN support and inclusion services",
          field: "highNeedsAmountSenServices",
          lineCodes: ["1.2.5", "1.2.8", "1.2.9"],
        },
        {
          name: "Alternative provision services",
          field: "highNeedsAmountAlternativeProvisionServices",
          lineCodes: ["1.2.7"],
        },
        {
          name: "Hospital education services",
          field: "highNeedsAmountHospitalServices",
          lineCodes: ["1.2.6"],
        },
        {
          name: "Therapies and other health related services",
          field: "highNeedsAmountOtherHealthServices",
          lineCodes: ["1.2.13"],
        },
      ],
    },
    {
      heading:
        "High needs amount per head of 2 to 18 population: place funding split by phase (for mainstream and type of institution (for specialist provision)",
      charts: [
        {
          name: "Primary place funding per head 2-18 population",
          field: "placeFundingPrimary",
          lineCodes: ["1.0.2"],
        },
        {
          name: "Secondary place funding per head 2-18 population",
          field: "placeFundingSecondary",
          lineCodes: ["1.0.2"],
        },
        {
          name: "Special place funding per head 2-18 population",
          field: "placeFundingSpecial",
          lineCodes: ["1.0.2"],
        },
        {
          name: "PRU and alternative provision place funding per head 2-18",
          field: "placeFundingAlternativeProvision",
          lineCodes: ["1.0.2"],
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
          lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        },
        {
          name: "Primary top up funding per head 2-18 population (maintained)",
          field: "maintainedPrimary",
          lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        },
        {
          name: "Secondary top up funding per head 2-18 population (maintained)",
          field: "maintainedSecondary",
          lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        },
        {
          name: "Special top up funding per head 2-18 population (maintained)",
          field: "maintainedSpecial",
          lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        },
        {
          name: "Alternative provision top up funding per head 2-18 population (maintained)",
          field: "maintainedAlternativeProvision",
          lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        },
        {
          name: "Post-school top up funding per head 2-18 population (maintained)",
          field: "maintainedPostSchool",
          lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
        },
        {
          name: "Top up funding income per head 2-18 population (maintained)",
          field: "maintainedIncome",
          lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
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
          lineCodes: ["1.2.3"],
        },
        {
          name: "Primary top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedPrimary",
          lineCodes: ["1.2.3"],
        },
        {
          name: "Secondary top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedSecondary",
          lineCodes: ["1.2.3"],
        },
        {
          name: "Special top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedSpecial",
          lineCodes: ["1.2.3"],
        },
        {
          name: "Alternative provision top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedAlternativeProvision",
          lineCodes: ["1.2.3"],
        },
        {
          name: "Post-school top up funding per head 2-18 population (non-maintained)",
          field: "nonMaintainedPostSchool",
          lineCodes: ["1.2.3"],
        },
        {
          name: "Top up funding income per head 2-18 population (non-maintained)",
          field: "nonMaintainedIncome",
          lineCodes: ["1.2.3"],
        },
      ],
    },
  ];

export const send2Sections: BenchmarkChartSend2Section<LocalAuthorityEducationHealthCarePlan>[] =
  [
    {
      heading:
        "Number aged up to 25 with SEN statement or EHC plan (per 1000 of 2 to 18 population)",
      charts: [
        {
          name: "Total",
          field: "total",
        },
      ],
    },
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
    },
  ];
