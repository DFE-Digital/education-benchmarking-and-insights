import {
  CensusHistoryItem,
  ExpenditureHistoryItem,
  IncomeHistoryItem,
} from "src/services";
import { HistoricData2Section, HistoricData2SectionChart } from "../types";
import {
  PoundsPerMetreSq,
  PoundsPerPupil,
  PupilsPerStaffRole,
} from "src/components";

/* eslint-disable react-refresh/only-export-components */
export * from "src/views/historic-data-2/partials/spending-section.tsx";
export * from "src/views/historic-data-2/partials/census-section.tsx";
export * from "src/views/historic-data-2/partials/income-section.tsx";

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

export const incomeSections: HistoricData2Section<IncomeHistoryItem>[] = [
  {
    heading: "Grant funding",
    charts: [
      {
        name: "Grant funding total",
        field: "totalGrantFunding",
        perUnitDimension: PoundsPerPupil,
      },
      {
        name: "Direct grants",
        field: "directGrants",
        perUnitDimension: PoundsPerPupil,
        details: {
          label: "More about direct grants",
          content: (
            <>
              <p>Where there is funding, direct grants include:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>pre-16 funding</li>
                <li>post-16 funding</li>
                <li>
                  Department of Education (DfE)/Education Funding Agency (EFA)
                  revenue grants
                </li>
                <li>other DfE or EFA revenue grants</li>
                <li>
                  other income (local authority and other government grants)
                </li>
                <li>government source (non-grant)</li>
              </ul>
            </>
          ),
        },
      },
      {
        name: "Pre-16 and post-16 funding",
        field: "prePost16Funding",
        perUnitDimension: PoundsPerPupil,
      },
      {
        name: "Other DfE revenue grants",
        field: "otherDfeGrants",
        perUnitDimension: PoundsPerPupil,
      },
      {
        name: "Other income (local authority and other government grants)",
        field: "otherIncomeGrants",
        perUnitDimension: PoundsPerPupil,
      },
      {
        name: "Government source (non-grant)",
        field: "governmentSource",
        perUnitDimension: PoundsPerPupil,
      },
      {
        name: "Community grants",
        field: "communityGrants",
        perUnitDimension: PoundsPerPupil,
      },
      {
        name: "Academies",
        field: "academies",
        perUnitDimension: PoundsPerPupil,
      },
    ],
  },
  {
    heading: "Self-generated",
    charts: [
      {
        name: "Self-generated funding total",
        field: "totalSelfGeneratedFunding",
        perUnitDimension: PoundsPerPupil,
      },
      {
        name: "Income from facilities and services",
        field: "incomeFacilitiesServices",
        perUnitDimension: PoundsPerPupil,
        details: {
          label: "More about income from facilities and services",
          content: (
            <>
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  income from meals provided to external customers, including
                  other schools
                </li>
                <li>
                  income from assets such as the hire of premises, equipment or
                  other facilities
                </li>
                <li>
                  all other income the school receives from facilities and
                  services, like income for consultancy, training courses and
                  examination fees
                </li>
                <li>
                  any interest payments received from bank accounts held in the
                  school's name or used to fund school activities
                </li>
                <li>
                  income from the sale of school uniforms, materials, private
                  phone calls, photocopying, publications, books
                </li>
                <li>income from before and after school clubs</li>
                <li>
                  income from the re-sale of items to pupils, like musical
                  instruments, classroom resources, commission on photographs
                </li>
                <li>income from non-catering vending machines</li>
                <li>income from a pupil-focused special facility</li>
                <li>
                  rental of school premises including deductions from salaries
                  where staff live on site
                </li>
                <li>income from universities for student/teacher placements</li>
                <li>income from energy/feed in tariffs</li>
                <li>
                  income from SEN and alternative provision support services
                  commissioned by a local authority or another school, for
                  delivery under a service level agreement
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  payments received from other schools for which you have not
                  provided a service
                </li>
                <li>income from community-focused special facilities</li>
                <li>high-needs place funding</li>
                <li>high-needs top-up funding</li>
                <li>any balances carried forward from previous years</li>
              </ul>
            </>
          ),
        },
      },
      {
        name: "Income from catering",
        field: "incomeCatering",
        perUnitDimension: PoundsPerPupil,
        details: {
          label: "More about income from catering",
          content: (
            <>
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  income from catering, school milk, and catering vending
                  machines
                </li>
                <li>
                  any payments received from catering contractors, such as where
                  a contractor has previously overcharged the school
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>receipts for catering for external customers</li>
                <li>income from non-catering vending machines</li>
                <li>any balances carried forward from previous years</li>
              </ul>
            </>
          ),
        },
      },
      {
        name: "Donations and/or voluntary funds",
        field: "donationsVoluntaryFunds",
        perUnitDimension: PoundsPerPupil,
        details: {
          label: "More about donations and/or voluntary funds",
          content: (
            <>
              <p>
                This is income from private sources under the control of the
                governing body, including:
              </p>
              <ul className="govuk-list govuk-list--bullet">
                <li>income provided from foundation, diocese or trust funds</li>
                <li>business sponsorship</li>
                <li>income from fundraising activities</li>
                <li>
                  contributions from parents (not directly requested by the
                  school) used to provide educational benefits
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  contributions or donations that are not used for the benefit
                  of students' learning or the school
                </li>
                <li>
                  balances available in trust funds or other private or
                  non-public accounts
                </li>
                <li>balances carried forward from previous years</li>
              </ul>
            </>
          ),
        },
      },
      {
        name: "Receipts from supply teacher insurance claims",
        field: "receiptsSupplyTeacherInsuranceClaims",
        perUnitDimension: PoundsPerPupil,
        details: {
          label: "More about receipts from supply teacher insurance claims",
          content: (
            <>
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  payments from staff absence insurance schemes to cover the
                  cost of supply teachers (including those offered by the local
                  authority)
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  insurance receipts for any other claim, for example absence of
                  non-teaching staff, or building, contents, and public
                  liability
                </li>
                <li>balances carried forward from previous years</li>
              </ul>
            </>
          ),
        },
      },
      {
        name: "Investment income",
        field: "investmentIncome",
        perUnitDimension: PoundsPerPupil,
        details: {
          label: "More about investment income",
          content: (
            <>
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>interest</li>
                <li>dividend income</li>
                <li>other investment income</li>
              </ul>
            </>
          ),
        },
      },
      {
        name: "Other self-generated income",
        field: "otherSelfGeneratedIncome",
        perUnitDimension: PoundsPerPupil,
        details: {
          label: "More about other self-generated income",
          content: (
            <>
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>fundraising activity</li>
                <li>lettings</li>
                <li>non-governmental grants</li>
                <li>commercial sponsorship</li>
                <li>consultancy</li>
              </ul>
            </>
          ),
        },
      },
    ],
  },
  {
    heading: "Direct revenue financing",
    charts: [
      {
        name: "Direct revenue financing (capital reserves transfers)",
        field: "directRevenueFinancing",
        perUnitDimension: PoundsPerPupil,
        details: {
          label: "More about direct revenue financing",
          content: (
            <>
              <p>This includes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  all amounts transferred to CI04 to be accumulated to fund
                  capital works (it may include receipts from insurance claims
                  for capital losses received into income under I11)
                </li>
                <li>
                  any amount transferred to a local authority reserve to part
                  fund a capital scheme delivered by the local authority
                </li>
                <li>
                  any repayment of principal on a capital loan from the local
                  authority
                </li>
              </ul>
              <p>It excludes:</p>
              <ul className="govuk-list govuk-list--bullet">
                <li>funds specifically provided for capital purposes</li>
              </ul>
            </>
          ),
        },
      },
    ],
  },
];
