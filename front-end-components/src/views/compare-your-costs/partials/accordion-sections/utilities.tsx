import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { UtilitiesData } from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  PoundsPerMetreSq,
  PremisesCategories,
  ChartDimensions,
} from "src/components";
import {
  ChartDimensionContext,
  PhaseContext,
  CustomDataContext,
  HasIncompleteDataContext,
} from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import { ExpenditureApi, UtilitiesExpenditure } from "src/services";

export const Utilities: React.FC<{
  type: string;
  id: string;
}> = ({ type, id }) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<UtilitiesExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<UtilitiesExpenditure>(
      type,
      id,
      dimension.value,
      "Utilities",
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
      PremisesCategories.find((x) => x.value === event.target.value) ??
      PoundsPerMetreSq;
    setDimension(dimension);
  };

  const totalUtilitiesCostsBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalUtilitiesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const energyBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.energyCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const waterSewerageBarData: HorizontalBarChartWrapperData<UtilitiesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.waterSewerageCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "utilities";
  const [hash] = useHash();

  const hasIncompleteData = false;
  const hasNoData = data?.length === 0;

  return (
    <HasIncompleteDataContext.Provider value={{ hasIncompleteData, hasNoData }}>
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
    </HasIncompleteDataContext.Provider>
  );
};
