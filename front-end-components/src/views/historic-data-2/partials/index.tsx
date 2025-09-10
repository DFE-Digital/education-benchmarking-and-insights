import { CensusHistoryItem, ExpenditureHistoryItem } from "src/services";
import { HistoricData2Section, HistoricData2SectionChart } from "../types";
import {
  PoundsPerMetreSq,
  PoundsPerPupil,
  PupilsPerStaffRole,
} from "src/components";

/* eslint-disable react-refresh/only-export-components */
export * from "src/views/historic-data-2/partials/spending-section.tsx";
export * from "src/views/historic-data-2/partials/census-section.tsx";

export const spendingSections: HistoricData2Section<ExpenditureHistoryItem>[] =
  [
    {
      heading: "Teaching and teaching support staff",
      charts: [
        {
          name: "Total teaching and teaching support staff costs",
          field: "totalTeachingSupportStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Teaching staff costs",
          field: "teachingStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Supply teaching staff",
          field: "supplyTeachingStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Educational consultancy",
          field: "educationalConsultancyCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Education support staff",
          field: "educationSupportStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Agency supply teaching staff",
          field: "agencySupplyTeachingStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Non-educational support staff",
      charts: [
        {
          name: "Total non-educational support staff costs",
          field: "totalNonEducationalSupportStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Administrative and clerical staff costs",
          field: "administrativeClericalStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Auditor costs",
          field: "auditorsCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Other staff costs",
          field: "otherStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Professional services (non-curriculum) cost",
          field: "professionalServicesNonCurriculumCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Educational supplies",
      charts: [
        {
          name: "Total educational supplies costs",
          field: "totalEducationalSuppliesCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Examination fees costs",
          field: "examinationFeesCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Learning resources (non ICT equipment) costs",
          field: "learningResourcesNonIctCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Educational ICT",
      charts: [
        {
          name: "ICT learning resources costs",
          field: "learningResourcesIctCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Premises staff and services",
      charts: [
        {
          name: "Total premises staff and services costs",
          field: "totalPremisesStaffServiceCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Cleaning and caretaking costs",
          field: "cleaningCaretakingCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Maintenance of premises costs",
          field: "maintenancePremisesCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Other occupation costs",
          field: "otherOccupationCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Premises staff costs",
          field: "premisesStaffCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
      ],
    },
    {
      heading: "Utilities",
      charts: [
        {
          name: "Total utilities costs",
          field: "totalUtilitiesCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Energy costs",
          field: "energyCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
        {
          name: "Water and sewerage costs",
          field: "waterSewerageCosts",
          perUnitDimension: PoundsPerMetreSq,
        },
      ],
    },
    {
      heading: "Administrative supplies",
      charts: [
        {
          name: "Administration supplies (non educational) costs",
          field: "administrativeSuppliesNonEducationalCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Catering staff and services",
      charts: [
        {
          name: "Total catering costs",
          field: "totalCateringCostsField",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Catering staff costs",
          field: "cateringStaffCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Catering supplies costs",
          field: "cateringSuppliesCosts",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
    {
      heading: "Other costs",
      charts: [
        {
          name: "Total other costs",
          field: "totalOtherCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Grounds maintenance costs",
          field: "groundsMaintenanceCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Indirect employee expenses costs",
          field: "indirectEmployeeExpenses",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Interest charges for loan and bank costs",
          field: "interestChargesLoanBank",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Other insurance premiums costs",
          field: "otherInsurancePremiumsCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "PFI charges costs",
          field: "privateFinanceInitiativeCharges",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Rents and rates costs",
          field: "rentRatesCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Special facilities costs",
          field: "specialFacilitiesCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Staff development and training costs",
          field: "staffDevelopmentTrainingCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Staff-related insurance costs",
          field: "staffRelatedInsuranceCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Supply teacher insurance costs",
          field: "supplyTeacherInsurableCosts",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Community focused school staff (maintained schools only)",
          field: "communityFocusedSchoolStaff",
          type: "school",
          perUnitDimension: PoundsPerPupil,
        },
        {
          name: "Community focused school costs (maintained schools only)",
          field: "communityFocusedSchoolCosts",
          type: "school",
          perUnitDimension: PoundsPerPupil,
        },
      ],
    },
  ];

export const censusCharts: HistoricData2SectionChart<CensusHistoryItem>[] = [
  {
    name: "Pupils on roll",
    field: "totalPupils",
    perUnitDimension: PupilsPerStaffRole,
    valueUnit: "amount",
    axisLabel: "total",
    columnHeading: "Total",
  },
  {
    name: "School workforce (full time equivalent)",
    field: "workforce",
    perUnitDimension: PupilsPerStaffRole,
    details: {
      label: "More about school workforce",
      content: (
        <>
          <p>
            This includes non-classroom based support staff, and full-time
            equivalent:{" "}
          </p>
          <ul className="govuk-list govuk-list--bullet">
            <li>classroom teachers</li>
            <li>senior leadership</li>
            <li>teaching assistants</li>
          </ul>
        </>
      ),
    },
  },
  {
    name: "Total number of teachers (full time equivalent)",
    field: "teachers",
    perUnitDimension: PupilsPerStaffRole,
    details: {
      label: "More about total number of teachers",
      content: (
        <p>
          This is the full-time equivalent of all classroom and leadership
          teachers.
        </p>
      ),
    },
  },
  {
    name: "Teachers with qualified teacher status (percentage)",
    field: "percentTeacherWithQualifiedStatus",
    perUnitDimension: PupilsPerStaffRole,
    details: {
      label: "More about teachers with qualified teacher status",
      content: (
        <p>
          We divided the number of teachers with qualified teacher status by the
          total number of teachers.
        </p>
      ),
    },
    valueUnit: "%",
    axisLabel: "percentage",
    columnHeading: "Percent",
  },
  {
    name: "Senior leadership (full time equivalent)",
    field: "seniorLeadership",
    perUnitDimension: PupilsPerStaffRole,
    details: {
      label: "More about senior leadership",
      content: (
        <>
          <p>
            This is the full-time equivalent of senior leadership roles,
            including:
          </p>
          <ul className="govuk-list govuk-list--bullet">
            <li>headteachers</li>
            <li>deputy headteachers</li>
            <li>assistant headteachers</li>
          </ul>
        </>
      ),
    },
  },
  {
    name: "Teaching assistants (full time equivalent)",
    field: "teachingAssistant",
    perUnitDimension: PupilsPerStaffRole,
    details: {
      label: "More about teaching assistants",
      content: (
        <>
          <p>
            This is the full-time equivalent of teaching assistants, including:
          </p>
          <ul className="govuk-list govuk-list--bullet">
            <li>teaching assistants</li>
            <li>higher level teaching assistants</li>
            <li>education needs support staff</li>
          </ul>
        </>
      ),
    },
  },
  {
    name: "Non-classroom support staff - excluding auxiliary staff (full time equivalent)",
    field: "nonClassroomSupportStaff",
    perUnitDimension: PupilsPerStaffRole,
    details: {
      label: "More about non-classroom support staff",
      content: (
        <>
          <p>
            This is the full-time equivalent of non-classroom-based support
            staff, excluding:
          </p>
          <ul className="govuk-list govuk-list--bullet">
            <li>auxiliary staff</li>
            <li>third party support staff</li>
          </ul>
        </>
      ),
    },
  },
  {
    name: "Auxiliary staff (full time equivalent)",
    field: "auxiliaryStaff",
    perUnitDimension: PupilsPerStaffRole,
    details: {
      label: "More about auxiliary staff",
      content: (
        <>
          <p>This is the full-time equivalent of auxiliary staff, including;</p>
          <ul className="govuk-list govuk-list--bullet">
            <li>catering</li>
            <li>school maintenance staff</li>
          </ul>
        </>
      ),
    },
  },
  {
    name: "School workforce (headcount)",
    field: "workforceHeadcount",
    perUnitDimension: PupilsPerStaffRole,
    details: {
      label: "More about school workforce (headcount)",
      content: (
        <>
          <p>This is the total headcount of the school workforce, including:</p>
          <ul className="govuk-list govuk-list--bullet">
            <li>
              full and part-time teachers (including school leadership teachers)
            </li>
            <li>teaching assistant</li>
            <li>non-classroom based support staff</li>
          </ul>
        </>
      ),
    },
  },
];
