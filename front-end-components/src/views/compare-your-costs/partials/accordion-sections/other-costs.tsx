import React, { useState } from "react";
import { OtherCostsProps } from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CalculateCostValue,
  CostCategories,
  DimensionHeading,
  PoundsPerPupil,
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";

export const OtherCosts: React.FC<OtherCostsProps> = ({ schools }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const tableHeadings = [
    "School name",
    "Local Authority",
    "School type",
    "Number of pupils",
    DimensionHeading(dimension),
  ];

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  const totalOtherCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.totalOtherCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const otherInsurancePremiumsCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.otherInsurancePremiumsCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const directRevenueFinancingCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.directRevenueFinancingCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const groundsMaintenanceCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.groundsMaintenanceCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const indirectEmployeeExpensesBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.indirectEmployeeExpenses,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const interestChargesLoanBankBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.interestChargesLoanBank,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const privateFinanceInitiativeChargesBarData: HorizontalBarChartWrapperData =
    {
      dataPoints: schools.map((school) => {
        return {
          school: school.name,
          urn: school.urn,
          value: CalculateCostValue({
            dimension: dimension,
            value: school.privateFinanceInitiativeCharges,
            ...school,
          }),
          additionalData: [
            school.localAuthority,
            school.schoolType,
            school.numberOfPupils,
          ],
        };
      }),
      tableHeadings: tableHeadings,
    };

  const rentRatesCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.rentRatesCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const specialFacilitiesCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.specialFacilitiesCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const staffDevelopmentTrainingCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.staffDevelopmentTrainingCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const staffRelatedInsuranceCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.staffRelatedInsuranceCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const supplyTeacherInsurableCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.supplyTeacherInsurableCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const communityFocusedSchoolStaffBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.communityFocusedSchoolStaff,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  const communityFocusedSchoolCostsBarData: HorizontalBarChartWrapperData = {
    dataPoints: schools.map((school) => {
      return {
        school: school.name,
        urn: school.urn,
        value: CalculateCostValue({
          dimension: dimension,
          value: school.communityFocusedSchoolCosts,
          ...school,
        }),
        additionalData: [
          school.localAuthority,
          school.schoolType,
          school.numberOfPupils,
        ],
      };
    }),
    tableHeadings: tableHeadings,
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <div className="govuk-accordion__section">
        <div className="govuk-accordion__section-header">
          <h2 className="govuk-accordion__section-heading">
            <span
              className="govuk-accordion__section-button"
              id="accordion-heading-9"
            >
              Other costs
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-9"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-9"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalOtherCostsBarData}
            chartName="total other costs"
          >
            <h3 className="govuk-heading-s">Total other costs</h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-otehr-costs"
              defaultValue={dimension}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={otherInsurancePremiumsCostsBarData}
            chartName="other insurance premiums costs"
          >
            <h3 className="govuk-heading-s">Other insurance premiums costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={directRevenueFinancingCostsBarData}
            chartName="direct revenue financing costs"
          >
            <h3 className="govuk-heading-s">Direct revenue financing costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={groundsMaintenanceCostsBarData}
            chartName="ground maintenance costs"
          >
            <h3 className="govuk-heading-s">Ground maintenance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={indirectEmployeeExpensesBarData}
            chartName="indirect employee expenses"
          >
            <h3 className="govuk-heading-s">Indirect employee expenses</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={interestChargesLoanBankBarData}
            chartName="interest charges for loan and bank"
          >
            <h3 className="govuk-heading-s">
              Interest charges for loan and bank
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={privateFinanceInitiativeChargesBarData}
            chartName="PFI charges"
          >
            <h3 className="govuk-heading-s">PFI charges</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={rentRatesCostsBarData}
            chartName="rent and rates costs"
          >
            <h3 className="govuk-heading-s">Rent and rates costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={specialFacilitiesCostsBarData}
            chartName="special facilities costs"
          >
            <h3 className="govuk-heading-s">Special facilities costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={staffDevelopmentTrainingCostsBarData}
            chartName="staff development and training costs"
          >
            <h3 className="govuk-heading-s">
              Staff development and training costs
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={staffRelatedInsuranceCostsBarData}
            chartName="staff-related insurance costs"
          >
            <h3 className="govuk-heading-s">Staff-related insurance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={supplyTeacherInsurableCostsBarData}
            chartName="supply teacher insurance costs"
          >
            <h3 className="govuk-heading-s">Supply teacher insurance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={communityFocusedSchoolStaffBarData}
            chartName="community focused school staff (maintained schools only)"
          >
            <h3 className="govuk-heading-s">
              Community focused school staff (maintained schools only)
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={communityFocusedSchoolCostsBarData}
            chartName="community focused school costs (maintained schools only)"
          >
            <h3 className="govuk-heading-s">
              Community focused school costs (maintained schools only)
            </h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
