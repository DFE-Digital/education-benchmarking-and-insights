import { createRef, useContext, useMemo, useState } from "react";
import { HorizontalBarChart } from "src/components/charts/horizontal-bar-chart";
import {
  TableChart,
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import {
  ChartDimensionContext,
  ChartModeContext,
  SelectedEstablishmentContext,
  HasIncompleteDataContext,
  IncludeBreakdownContext,
} from "src/contexts";
import { Loading } from "src/components/loading";
import { ChartHandler, ChartSeriesConfigItem } from "src/components/charts";
import { HorizontalBarChartWrapperProps } from "src/composed/horizontal-bar-chart-wrapper";
import { ChartModeChart, ChartModeTable } from "src/components";
import {
  chartSeriesComparer,
  shortValueFormatter,
} from "src/components/charts/utils";
import { EstablishmentTick } from "src/components/charts/establishment-tick";
import { SchoolCensusTooltip } from "src/components/charts/school-census-tooltip";
import { WarningBanner } from "src/components/warning-banner";
import { ErrorBanner } from "src/components/error-banner";
import { TrustDataTooltip } from "src/components/charts/trust-data-tooltip";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/include-breakdown";

export function HorizontalBarChartWrapper<
  TData extends SchoolChartData | TrustChartData,
>(props: HorizontalBarChartWrapperProps<TData>) {
  const { chartName, children, data, sort, valueUnit } = props;
  const mode = useContext(ChartModeContext);
  const dimension = useContext(ChartDimensionContext);
  const selectedEstabishment = useContext(SelectedEstablishmentContext);
  const { hasIncompleteData, hasNoData } = useContext(HasIncompleteDataContext);
  const breakdown = useContext(IncludeBreakdownContext);
  const ref = createRef<ChartHandler>();
  const [imageLoading, setImageLoading] = useState<boolean>();
  const isTrust = breakdown !== undefined;
  const keyField = (isTrust ? "companyNumber" : "urn") as keyof TData;
  const seriesLabelField = (
    isTrust ? "trustName" : "schoolName"
  ) as keyof TData;
  const seriesConfig: { [key: string]: ChartSeriesConfigItem } = {
    [isTrust ? "schoolValue" : "value"]: {
      visible: true,
      valueFormatter: (v) =>
        shortValueFormatter(v, {
          valueUnit: valueUnit ?? dimension.unit,
        }),
    },
  };

  // stack additional series if they are available in the input data set
  let labelListSeriesName: keyof TData | undefined;
  if (isTrust) {
    if (breakdown === BreakdownInclude) {
      seriesConfig.schoolValue.stackId = 1;
      seriesConfig.centralValue = Object.assign({}, seriesConfig.schoolValue);
      labelListSeriesName = "totalValue" as keyof TData;
    }
  }

  // if a `sort` is not provided, the default sorting method will be used (value DESC)
  const sortedDataPoints = useMemo(() => {
    let dataPoint = "value";
    if (breakdown === BreakdownInclude) {
      dataPoint = "totalValue";
    } else if (breakdown === BreakdownExclude) {
      dataPoint = "schoolValue";
    }

    return data.dataPoints
      .map((d) =>
        isFinite((d as never)[dataPoint] as number) &&
        !isNaN((d as never)[dataPoint] as number)
          ? d
          : { ...d, [dataPoint]: 0 }
      )
      .sort((a, b) =>
        chartSeriesComparer(
          a as TData,
          b as TData,
          sort ?? {
            direction: "desc",
            dataPoint: dataPoint as keyof TData,
          }
        )
      ) as TData[];
  }, [data.dataPoints, sort, breakdown]);

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">{children}</div>
        {mode == ChartModeChart && (
          <div className="govuk-grid-column-one-third">
            <button
              className="govuk-button govuk-button--secondary"
              data-module="govuk-button"
              data-prevent-double-click="true"
              onClick={() => ref.current?.download()}
              disabled={imageLoading}
              aria-disabled={imageLoading}
            >
              Save as image
            </button>
          </div>
        )}
      </div>
      <WarningBanner
        isRendered={hasIncompleteData}
        message="Some schools are missing data for this financial year"
      />
      <ErrorBanner
        isRendered={hasNoData}
        message="Unable to load data for this financial year"
      />
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-full">
          {sortedDataPoints.length > 0 ? (
            <>
              {mode == ChartModeChart && (
                <div style={{ height: 22 * data.dataPoints.length + 75 }}>
                  <HorizontalBarChart
                    barCategoryGap={2}
                    chartName={chartName}
                    data={sortedDataPoints}
                    highlightActive
                    highlightedItemKeys={
                      selectedEstabishment ? [selectedEstabishment] : undefined
                    }
                    keyField={keyField}
                    onImageLoading={setImageLoading}
                    labels
                    labelListSeriesName={labelListSeriesName}
                    margin={20}
                    ref={ref}
                    seriesConfig={seriesConfig as object}
                    seriesLabelField={seriesLabelField}
                    tickWidth={400}
                    tick={(t) => (
                      <EstablishmentTick
                        {...t}
                        highlightedItemKey={selectedEstabishment}
                        linkToEstablishment
                        href={(id) => `/${isTrust ? "trust" : "school"}/${id}`}
                        establishmentKeyResolver={(name) => {
                          if (isTrust) {
                            return (data.dataPoints as TrustChartData[]).find(
                              (d) => d.trustName === name
                            )?.companyNumber;
                          }

                          return (data.dataPoints as SchoolChartData[]).find(
                            (d) => d.schoolName === name
                          )?.urn;
                        }}
                      />
                    )}
                    tooltip={(t) =>
                      isTrust ? (
                        <TrustDataTooltip
                          {...t}
                          valueUnit={valueUnit ?? dimension.unit}
                        />
                      ) : (
                        <SchoolCensusTooltip {...t} />
                      )
                    }
                    valueFormatter={shortValueFormatter}
                    valueLabel={dimension.label}
                    valueUnit={valueUnit ?? dimension.unit}
                  />
                </div>
              )}
              <div
                className={
                  mode == ChartModeTable ? "" : "govuk-visually-hidden"
                }
              >
                <TableChart
                  tableHeadings={data.tableHeadings}
                  data={sortedDataPoints}
                  preventFocus={mode !== ChartModeTable}
                  valueUnit={valueUnit ?? dimension.unit}
                />
              </div>
            </>
          ) : (
            !hasNoData && <Loading />
          )}
        </div>
      </div>
    </>
  );
}
