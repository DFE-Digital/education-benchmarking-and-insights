import React, { useCallback, useEffect, useMemo, useState } from "react";
import { AdministrativeSuppliesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
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
  AdministrativeSuppliesTrustExpenditure,
} from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";

export const AdministrativeSupplies: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<
    AdministrativeSuppliesTrustExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<AdministrativeSuppliesTrustExpenditure>(
      id,
      dimension.value,
      "AdministrationSupplies",
      breakdown === BreakdownExclude
    );
  }, [id, dimension, breakdown]);

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
      const tableHeadings = ["Trust name", `Total ${dimension.heading}`];
      if (breakdown === BreakdownInclude) {
        tableHeadings.push(
          `School ${dimension.heading}`,
          `Central ${dimension.heading}`
        );
      }

      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue:
                    trust.administrativeSuppliesNonEducationalCosts ?? 0,
                  schoolValue:
                    trust.schoolAdministrativeSuppliesNonEducationalCosts ?? 0,
                  centralValue:
                    trust.centralAdministrativeSuppliesNonEducationalCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, dimension, breakdown]);

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
            trust
          >
            <h3 className="govuk-heading-s">
              Administrative supplies (Non-educational)
            </h3>
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="administrative-supplies-non-eductional"
              value={dimension.value}
            />
          </HorizontalBarChartWrapper>
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
