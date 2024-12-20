import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  AdministrativeSuppliesData,
  CompareYourCostsProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
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
import {
  ExpenditureApi,
  AdministrativeSuppliesExpenditure,
} from "src/services";
import { ErrorBanner } from "src/components/error-banner";

export const AdministrativeSupplies: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<
    AdministrativeSuppliesExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<AdministrativeSuppliesExpenditure>(
      type,
      id,
      dimension.value,
      "AdministrationSupplies",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

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
        "School name",
        "Local Authority",
        "School type",
        "Number of pupils",
        dimension.heading,
      ];

      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.administrativeSuppliesNonEducationalCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, dimension]);

  const elementId = "administrative-supplies";
  const [hash] = useHash();

  const hasNoData = data?.length === 0;

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
          {hasNoData ? (
            <ErrorBanner
              isRendered={hasNoData}
              message="There isn't enough information available to create a set of similar schools."
            />
          ) : (
            <>
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
                  value={dimension.value}
                />
              </HorizontalBarChartWrapper>
            </>
          )}
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
