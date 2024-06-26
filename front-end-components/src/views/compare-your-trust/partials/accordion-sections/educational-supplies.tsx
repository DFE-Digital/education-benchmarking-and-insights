import React, { useCallback, useEffect, useMemo, useState } from "react";
import { EducationalSuppliesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import {
  ChartDimensionContext,
  useCentralServicesBreakdownContext,
} from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import {
  ExpenditureApi,
  EducationalSuppliesTrustExpenditure,
} from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";

export const EducationalSupplies: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<
    EducationalSuppliesTrustExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<EducationalSuppliesTrustExpenditure>(
      id,
      dimension.value,
      "EducationalSupplies",
      breakdown === BreakdownExclude
    );
  }, [id, dimension, breakdown]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const tableHeadings = useMemo(() => {
    const headings = ["Trust name", `Total ${dimension.heading}`];
    if (breakdown === BreakdownInclude) {
      headings.push(
        `School ${dimension.heading}`,
        `Central ${dimension.heading}`
      );
    }
    return headings;
  }, [dimension, breakdown]);

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
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalEducationalSuppliesCosts ?? 0,
                  schoolValue: trust.schoolTotalEducationalSuppliesCosts ?? 0,
                  centralValue: trust.centralTotalEducationalSuppliesCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const examinationFeesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.examinationFeesCosts ?? 0,
                  schoolValue: trust.schoolExaminationFeesCosts ?? 0,
                  centralValue: trust.centralExaminationFeesCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalSuppliesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.learningResourcesNonIctCosts ?? 0,
                  schoolValue: trust.schoolLearningResourcesNonIctCosts ?? 0,
                  centralValue: trust.centralLearningResourcesNonIctCosts ?? 0,
                };
              })
            : [],
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
            trust
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
            trust
          >
            <h3 className="govuk-heading-s">Examination fees costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={learningResourcesBarData}
            chartName="learning resource (not ICT equipment) costs"
            trust
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
