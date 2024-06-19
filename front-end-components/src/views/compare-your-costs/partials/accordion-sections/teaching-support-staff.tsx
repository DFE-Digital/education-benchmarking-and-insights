import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { TeachingSupportStaffData } from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext, PhaseContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import { ExpenditureApi, TeachingSupportStaffExpenditure } from "src/services";

export const TeachingSupportStaff: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const [data, setData] = useState<TeachingSupportStaffExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<TeachingSupportStaffExpenditure>(
      type,
      id,
      dimension.value,
      "TeachingTeachingSupportStaff",
      phase
    );
  }, [id, dimension, type, phase]);

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

  const totalTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalTeachingSupportStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const teachingStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.teachingStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const supplyTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.supplyTeachingStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const educationalConsultancyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.educationalConsultancyCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const educationSupportStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.educationSupportStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const agencySupplyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.agencySupplyTeachingStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "teaching-and-teaching-support-staff";
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
              id="accordion-heading-1"
            >
              Teaching and teaching support staff
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-1"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-1"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalTeachingBarData}
            chartName="total teaching and support staff cost"
          >
            <h3 className="govuk-heading-s">
              Total teaching and teaching support staff costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-teaching-support-staff-cost"
              value={dimension.value}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={teachingStaffBarData}
            chartName="teaching staff costs"
          >
            <h3 className="govuk-heading-s">Teaching staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={supplyTeachingBarData}
            chartName="supply teaching staff costs"
          >
            <h3 className="govuk-heading-s">Supply teaching staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={educationalConsultancyBarData}
            chartName="educational consultancy costs"
          >
            <h3 className="govuk-heading-s">Educational consultancy costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={educationSupportStaffBarData}
            chartName="educational support staff costs"
          >
            <h3 className="govuk-heading-s">Educational support staff costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={agencySupplyBarData}
            chartName="agency supply teaching staff costs"
          >
            <h3 className="govuk-heading-s">
              Agency supply teaching staff costs
            </h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
