import React, { useCallback, useEffect, useMemo, useState } from "react";
import { UtilitiesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import {
  PoundsPerMetreSq,
  PremisesCategories,
  ChartDimensions,
} from "src/components";
import { ChartDimensionContext } from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import { ExpenditureApi, UtilitiesTrustExpenditure } from "src/services";

export const Utilities: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const [data, setData] = useState<UtilitiesTrustExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<UtilitiesTrustExpenditure>(
      id,
      dimension.value,
      "Utilities",
      true
    );
  }, [id, dimension]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const tableHeadings = useMemo(
    () => [
      "Trust name",
      `Total ${dimension.heading}`,
      `School ${dimension.heading}`,
      `Central ${dimension.heading}`,
    ],
    [dimension]
  );

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      PremisesCategories.find((x) => x.value === event.target.value) ??
      PoundsPerMetreSq;
    setDimension(dimension);
  };

  const totalUtilitiesCostsBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.totalUtilitiesCosts ?? 0,
                  schoolValue: trust.schoolTotalUtilitiesCosts ?? 0,
                  centralValue: trust.centralTotalUtilitiesCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const energyBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.energyCosts ?? 0,
                  schoolValue: trust.schoolEnergyCosts ?? 0,
                  centralValue: trust.centralEnergyCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const waterSewerageBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.waterSewerageCosts ?? 0,
                  schoolValue: trust.schoolWaterSewerageCosts ?? 0,
                  centralValue: trust.centralWaterSewerageCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "utilities";
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
              id="accordion-heading-6"
            >
              Utilities
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-6"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-6"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={totalUtilitiesCostsBarData}
            chartName="total utilities costs"
          >
            <h3 className="govuk-heading-s">Total utilities costs</h3>
            <ChartDimensions
              dimensions={PremisesCategories}
              handleChange={handleSelectChange}
              elementId="total-utilities-costs"
              value={dimension.value}
            />
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={energyBarData}
            chartName="energy costs"
          >
            <h3 className="govuk-heading-s">Energy costs</h3>
          </HorizontalBarChartWrapper>
          <HorizontalBarChartWrapper
            data={waterSewerageBarData}
            chartName="water and sewerage costs"
          >
            <h3 className="govuk-heading-s">Water and sewerage costs</h3>
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
