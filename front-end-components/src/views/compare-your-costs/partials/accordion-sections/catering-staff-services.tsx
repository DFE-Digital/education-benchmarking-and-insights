import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CateringStaffServicesData,
  CompareYourCostsProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import {
  ChartDimensionContext,
  CustomDataContext,
  PhaseContext,
} from "src/contexts";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { useHash } from "src/hooks/useHash";
import classNames from "classnames";
import {
  ExpenditureApi,
  CateringStaffServicesExpenditure,
  TotalCateringCostsField,
} from "src/services";
import { TotalCateringCostsType } from "src/components/total-catering-costs-type";
import { ErrorBanner } from "src/components/error-banner";

export const CateringStaffServices: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<CateringStaffServicesExpenditure[] | null>();
  const [totalCateringCostsField, setTotalCateringCostsField] =
    useState<TotalCateringCostsField>("totalGrossCateringCosts");
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<CateringStaffServicesExpenditure>(
      type,
      id,
      dimension.value,
      "CateringStaffServices",
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

  const totalCateringBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value:
                (totalCateringCostsField == "totalGrossCateringCosts"
                  ? school.totalGrossCateringCosts
                  : school.totalNetCateringCosts) ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings, totalCateringCostsField]);

  const cateringStaffBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.cateringStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cateringSuppliesBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.cateringSuppliesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const elementId = "catering-staff-and-supplies";
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
              id="accordion-heading-8"
            >
              Catering staff and services
            </span>
          </h2>
        </div>
        <div
          id="accordion-content-8"
          className="govuk-accordion__section-content"
          aria-labelledby="accordion-heading-8"
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
                data={totalCateringBarData}
                chartName="total catering costs"
              >
                <h3 className="govuk-heading-s">Total catering costs</h3>
                <div className="govuk-grid-row">
                  <div className="govuk-grid-column-one-half">
                    <ChartDimensions
                      dimensions={CostCategories}
                      handleChange={handleSelectChange}
                      elementId="total-catering-costs"
                      value={dimension.value}
                    />
                  </div>
                  <div className="govuk-grid-column-one-half">
                    <TotalCateringCostsType
                      field={totalCateringCostsField}
                      onChange={setTotalCateringCostsField}
                    />
                  </div>
                </div>
              </HorizontalBarChartWrapper>
              <HorizontalBarChartWrapper
                data={cateringStaffBarData}
                chartName="catering staff costs"
              >
                <h3 className="govuk-heading-s">Catering staff costs</h3>
              </HorizontalBarChartWrapper>
              <HorizontalBarChartWrapper
                data={cateringSuppliesBarData}
                chartName="catering supplies costs"
              >
                <h3 className="govuk-heading-s">Catering supplies costs</h3>
              </HorizontalBarChartWrapper>
            </>
          )}
        </div>
      </div>
    </ChartDimensionContext.Provider>
  );
};
