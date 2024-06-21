import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { OtherCostsData } from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import {
  ChartDimensionContext,
  PhaseContext,
  CustomDataContext,
} from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import { ExpenditureApi, OtherCostsDataExpenditure } from "src/services";

export const OtherCosts: React.FC<{
  type: string;
  id: string;
}> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<OtherCostsDataExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<OtherCostsDataExpenditure>(
      type,
      id,
      dimension.value,
      "Other",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const tableHeadings = useMemo(
    () => [
      "School name",
      "Local Authority",
      "School type",
      "Number of pupils",
      dimension.heading,
    ],
    [dimension]
  );

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  const totalOtherCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalOtherCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherInsurancePremiumsCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.otherInsurancePremiumsCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const directRevenueFinancingCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.directRevenueFinancingCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const groundsMaintenanceCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.groundsMaintenanceCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const indirectEmployeeExpensesBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.indirectEmployeeExpenses,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const interestChargesLoanBankBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.interestChargesLoanBank,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const privateFinanceInitiativeChargesBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.privateFinanceInitiativeCharges,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const rentRatesCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.rentRatesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const specialFacilitiesCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.specialFacilitiesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const staffDevelopmentTrainingCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.staffDevelopmentTrainingCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const staffRelatedInsuranceCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.staffRelatedInsuranceCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const supplyTeacherInsurableCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.supplyTeacherInsurableCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const communityFocusedSchoolStaffBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.communityFocusedSchoolStaff,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const communityFocusedSchoolCostsBarData: HorizontalBarChartWrapperData<OtherCostsData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.communityFocusedSchoolCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "other";
  const [hash] = useHash();

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <div
        className={classNames("govuk-accordion__section", {
          "govuk-accordion__section--expanded": hash === `#${elementId}`,
        })}
        id={elementId}
      >
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
              value={dimension.value}
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
