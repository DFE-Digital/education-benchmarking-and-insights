import React, { useMemo, useState } from "react";
import {
  OtherCostsData,
  OtherCostsProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CalculateCostValue,
  CostCategories,
  DimensionHeading,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";

export const OtherCosts: React.FC<OtherCostsProps> = ({ schools }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil.value);
  const tableHeadings = useMemo(
    () => [
      "School name",
      "Local Authority",
      "School type",
      "Number of pupils",
      DimensionHeading(dimension),
    ],
    [dimension]
  );

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    setDimension(event.target.value);
  };

  const totalOtherCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.totalOtherCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const otherInsurancePremiumsCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.otherInsurancePremiumsCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const directRevenueFinancingCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.directRevenueFinancingCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const groundsMaintenanceCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.groundsMaintenanceCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const indirectEmployeeExpensesBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.indirectEmployeeExpenses,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const interestChargesLoanBankBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.interestChargesLoanBank,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const privateFinanceInitiativeChargesBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.privateFinanceInitiativeCharges,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const rentRatesCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.rentRatesCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const specialFacilitiesCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.specialFacilitiesCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const staffDevelopmentTrainingCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.staffDevelopmentTrainingCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const staffRelatedInsuranceCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.staffRelatedInsuranceCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const supplyTeacherInsurableCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.supplyTeacherInsurableCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const communityFocusedSchoolStaffBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.communityFocusedSchoolStaff,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

  const communityFocusedSchoolCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints: schools.map((school) => {
          return {
            ...school,
            value: CalculateCostValue({
              dimension,
              value: school.communityFocusedSchoolCosts,
              ...school,
            }),
          };
        }),
        tableHeadings,
      };
    }, [dimension, schools, tableHeadings]);

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
            valueUnit="currency"
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
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Other insurance premiums costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={directRevenueFinancingCostsBarData}
            chartName="direct revenue financing costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Direct revenue financing costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={groundsMaintenanceCostsBarData}
            chartName="ground maintenance costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Ground maintenance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={indirectEmployeeExpensesBarData}
            chartName="indirect employee expenses"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Indirect employee expenses</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={interestChargesLoanBankBarData}
            chartName="interest charges for loan and bank"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">
              Interest charges for loan and bank
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={privateFinanceInitiativeChargesBarData}
            chartName="PFI charges"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">PFI charges</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={rentRatesCostsBarData}
            chartName="rent and rates costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Rent and rates costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={specialFacilitiesCostsBarData}
            chartName="special facilities costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Special facilities costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={staffDevelopmentTrainingCostsBarData}
            chartName="staff development and training costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">
              Staff development and training costs
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={staffRelatedInsuranceCostsBarData}
            chartName="staff-related insurance costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Staff-related insurance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={supplyTeacherInsurableCostsBarData}
            chartName="supply teacher insurance costs"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">Supply teacher insurance costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={communityFocusedSchoolStaffBarData}
            chartName="community focused school staff (maintained schools only)"
            valueUnit="currency"
          >
            <h3 className="govuk-heading-s">
              Community focused school staff (maintained schools only)
            </h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={communityFocusedSchoolCostsBarData}
            chartName="community focused school costs (maintained schools only)"
            valueUnit="currency"
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
