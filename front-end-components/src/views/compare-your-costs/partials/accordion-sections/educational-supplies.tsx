import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { EducationalSuppliesData } from "src/views/compare-your-costs/partials/accordion-sections/types";
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
import { ExpenditureApi, EducationalSuppliesExpenditure } from "src/services";

export const EducationalSupplies: React.FC<{
  type: string;
  id: string;
}> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const [data, setData] = useState<EducationalSuppliesExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<EducationalSuppliesExpenditure>(
      type,
      id,
      dimension.value,
      "EducationalSupplies",
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

  const totalEducationalSuppliesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalEducationalSuppliesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const examinationFeesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.examinationFeesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.learningResourcesNonIctCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "educational-supplies";
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
              id="accordion-heading-3"
            >
              Educational supplies
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-3"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-3"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalEducationalSuppliesBarData}
            chartName="total educational supplies costs"
          >
            <h3 className="govuk-heading-s">
              Total educational supplies costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-educational-supplies-costs"
              value={dimension.value}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={examinationFeesBarData}
            chartName="examination fees costs"
          >
            <h3 className="govuk-heading-s">Examination fees costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={learningResourcesBarData}
            chartName="learning resource (not ICT equipment) costs"
          >
            <h3 className="govuk-heading-s">
              Learning resources (not ICT equipment) costs
            </h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
