import {
  LocalAuthoritySection251,
  LocalAuthorityEducationHealthCarePlan,
} from "src/services";
import {
  BenchmarkChartSection251Section,
  BenchmarkChartSend2Section,
} from "../types";
import { SourceInfoModes } from "src/components/source-info";

/* eslint-disable react-refresh/only-export-components */
export * from "src/views/benchmark-data-high-needs/partials/section-251-section.tsx";
export * from "src/views/benchmark-data-high-needs/partials/send-2-section.tsx";

export const section251Sections: BenchmarkChartSection251Section<LocalAuthoritySection251>[] =
  [
    {
      heading: "High needs amount per pupil",
      charts: [
        {
          name: "Total place funding for special schools and AP/PRUs",
          field: "highNeedsAmountTotalPlaceFunding",
          sourceInfo: {
            lineCodes: ["1.0.2"],
          },
        },
        {
          name: "Top up funding (maintained schools, academies, free schools and colleges)",
          field: "highNeedsAmountTopUpFundingMaintained",
          sourceInfo: {
            lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
          },
        },
        {
          name: "Top up funding (non-maintained and independent schools and colleges)",
          field: "highNeedsAmountTopUpFundingNonMaintained",
          sourceInfo: {
            lineCodes: ["1.2.3"],
          },
        },
        {
          name: "SEN support and inclusion services",
          field: "highNeedsAmountSenServices",
          sourceInfo: {
            lineCodes: ["1.2.5", "1.2.8", "1.2.9"],
          },
        },
        {
          name: "Alternative provision services",
          field: "highNeedsAmountAlternativeProvisionServices",
          sourceInfo: {
            lineCodes: ["1.2.7"],
          },
        },
        {
          name: "Hospital education services",
          field: "highNeedsAmountHospitalServices",
          sourceInfo: {
            lineCodes: ["1.2.6"],
            additionalInfo: SourceInfoModes.Hospital,
          },
        },
        {
          name: "Therapies and other health related services",
          field: "highNeedsAmountOtherHealthServices",
          sourceInfo: {
            lineCodes: ["1.2.13"],
          },
        },
      ],
    },
    {
      heading:
        "High needs amount per pupil: place funding split by phase (for mainstream) and type of institution (for specialist provision)",
      charts: [
        {
          name: "Primary place funding per pupil",
          field: "placeFundingPrimary",
          sourceInfo: {
            lineCodes: ["1.0.2"],
            additionalInfo: SourceInfoModes.Glossary,
          },
        },
        {
          name: "Secondary place funding per pupil",
          field: "placeFundingSecondary",
          sourceInfo: {
            lineCodes: ["1.0.2"],
            additionalInfo: SourceInfoModes.Glossary,
          },
        },
        {
          name: "Special place funding per pupil",
          field: "placeFundingSpecial",
          sourceInfo: {
            lineCodes: ["1.0.2"],
            additionalInfo: SourceInfoModes.Glossary,
          },
        },
        {
          name: "PRU and alternative provision place funding per pupil",
          field: "placeFundingAlternativeProvision",
          sourceInfo: {
            lineCodes: ["1.0.2"],
            additionalInfo: SourceInfoModes.Glossary,
          },
        },
      ],
    },
    {
      heading:
        "High needs amount per pupil: top up funding (maintained schools, academies, free schools and colleges) split by phase (for mainstream) and type of institution (for specialist provision)",
      charts: [
        {
          name: "Early years top up funding per pupil (maintained)",
          field: "maintainedEarlyYears",
          sourceInfo: {
            lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
          },
        },
        {
          name: "Primary top up funding per pupil (maintained)",
          field: "maintainedPrimary",
          sourceInfo: {
            lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
          },
        },
        {
          name: "Secondary top up funding per pupil (maintained)",
          field: "maintainedSecondary",
          sourceInfo: {
            lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
          },
        },
        {
          name: "Special top up funding per pupil (maintained)",
          field: "maintainedSpecial",
          sourceInfo: {
            lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
          },
        },
        {
          name: "Alternative provision top up funding per pupil (maintained)",
          field: "maintainedAlternativeProvision",
          sourceInfo: {
            lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
          },
        },
        {
          name: "Post-school top up funding per pupil (maintained)",
          field: "maintainedPostSchool",
          sourceInfo: {
            lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
          },
        },
        {
          name: "Top up funding income per pupil (maintained)",
          field: "maintainedIncome",
          sourceInfo: {
            lineCodes: ["1.2.1", "1.2.2", "1.2.4", "1.2.11"],
          },
        },
      ],
    },
    {
      heading:
        "High needs amount per pupil: top up funding (non-maintained schools and independent schools and colleges) split by phase (for mainstream) and type of institution (for specialist provision)",
      charts: [
        {
          name: "Early years top up funding per pupil (non-maintained)",
          field: "nonMaintainedEarlyYears",
          sourceInfo: {
            lineCodes: ["1.2.3"],
          },
        },
        {
          name: "Primary top up funding per pupil (non-maintained)",
          field: "nonMaintainedPrimary",
          sourceInfo: {
            lineCodes: ["1.2.3"],
          },
        },
        {
          name: "Secondary top up funding per pupil (non-maintained)",
          field: "nonMaintainedSecondary",
          sourceInfo: {
            lineCodes: ["1.2.3"],
          },
        },
        {
          name: "Special top up funding per pupil (non-maintained)",
          field: "nonMaintainedSpecial",
          sourceInfo: {
            lineCodes: ["1.2.3"],
          },
        },
        {
          name: "Alternative provision top up funding per pupil (non-maintained)",
          field: "nonMaintainedAlternativeProvision",
          sourceInfo: {
            lineCodes: ["1.2.3"],
          },
        },
        {
          name: "Post-school top up funding per pupil (non-maintained)",
          field: "nonMaintainedPostSchool",
          sourceInfo: {
            lineCodes: ["1.2.3"],
          },
        },
        {
          name: "Top up funding income per pupil (non-maintained)",
          field: "nonMaintainedIncome",
          sourceInfo: {
            lineCodes: ["1.2.3"],
          },
        },
      ],
    },
  ];

export const send2Sections: BenchmarkChartSend2Section<LocalAuthorityEducationHealthCarePlan>[] =
  [
    {
      heading:
        "Number aged up to 25 with SEN statement or EHC plan (per 1000 pupils)",
      charts: [
        {
          name: "Total",
          field: "total",
        },
      ],
    },
    {
      heading:
        "Placement of pupils aged up to 25 with SEN statement or EHC plan (per 1000 pupils)",
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
