import React, { useCallback, useEffect, useMemo, useState } from "react";
import { EducationalIctData } from "src/views/compare-your-costs-trust/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import { TrustExpenditure, ExpenditureApi } from "src/services";

export const EducationalIct: React.FC<{
  type: string;
  id: string;
}> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const [data, setData] = useState<TrustExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust(
      type,
      id,
      dimension.value,
      "EducationalIct",
      true
    );
  }, [id, dimension, type]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  const learningResourcesBarData: HorizontalBarChartWrapperData<EducationalIctData> =
    useMemo(() => {
      const tableHeadings = [
        "Trust name",
        `Total ${dimension.heading}`,
        `School ${dimension.heading}`,
        `Central ${dimension.heading}`,
      ];

      return {
        dataPoints:
          data?.map((trust) => {
            return {
              ...trust,
              totalValue: trust.learningResourcesIctCosts ?? 0,
              schoolValue: trust.schoolLearningResourcesIctCosts ?? 0,
              centralValue: trust.centralLearningResourcesIctCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [dimension, data]);

  const elementId = "educational-ict";
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
              id="accordion-heading-4"
            >
              Educational ICT
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-4"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-4"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={learningResourcesBarData}
            chartName="eductional learning resources costs"
          >
            <h3 className="govuk-heading-s">
              Educational learning resources costs
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="eductional-learning-resources-costs"
              defaultValue={dimension.value}
            />
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
