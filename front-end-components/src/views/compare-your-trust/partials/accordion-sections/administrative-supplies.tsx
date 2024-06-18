import React, { useCallback, useEffect, useMemo, useState } from "react";
import { AdministrativeSuppliesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
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
import {
  ExpenditureApi,
  AdministrativeSuppliesTrustExpenditure,
} from "src/services";

export const AdministrativeSupplies: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const [data, setData] = useState<
    AdministrativeSuppliesTrustExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<AdministrativeSuppliesTrustExpenditure>(
      id,
      dimension.value,
      "AdministrationSupplies",
      true
    );
  }, [id, dimension]);

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

  const administrativeSuppliesBarData: HorizontalBarChartWrapperData<AdministrativeSuppliesData> =
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
              totalValue: trust.administrativeSuppliesCosts ?? 0,
              schoolValue: trust.schoolAdministrativeSuppliesCosts ?? 0,
              centralValue: trust.centralAdministrativeSuppliesCosts ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, dimension]);

  const elementId = "administrative-supplies";
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
              id="accordion-heading-7"
            >
              Administrative supplies
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-7"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-7"
          role="region"
        >
          <HorizontalBarChartWrapper
            data={administrativeSuppliesBarData}
            chartName="administrative supplies (non-eductional)"
          >
            <h3 className="govuk-heading-s">
              Administrative supplies (Non-educational)
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="administrative-supplies-non-eductional"
              defaultValue={dimension.value}
            />
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
