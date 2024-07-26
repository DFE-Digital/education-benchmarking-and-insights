import { createRef, useContext, useMemo, useState } from "react";
import { HorizontalBarChart } from "src/components/charts/horizontal-bar-chart";
import {
  TableChart,
  SchoolChartData,
  TrustChartData,
} from "src/components/charts/table-chart";
import {
  ChartDimensionContext,
  SelectedEstablishmentContext,
  HasIncompleteDataContext,
  useChartModeContext,
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

export function HorizontalBarChartWrapper<
  TData extends SchoolChartData | TrustChartData,
>(props: HorizontalBarChartWrapperProps<TData>) {
  const { chartName, children, data, sort, trust, valueUnit } = props;
  const { chartMode } = useChartModeContext();
  const dimension = useContext(ChartDimensionContext);
  const selectedEstabishment = useContext(SelectedEstablishmentContext);
  const { hasIncompleteData, hasNoData } = useContext(HasIncompleteDataContext);
  const ref = createRef<ChartHandler>();
  const [imageLoading, setImageLoading] = useState<boolean>();
  const keyField = (trust ? "companyNumber" : "urn") as keyof TData;
  const seriesLabelField = (trust ? "trustName" : "schoolName") as keyof TData;
  const seriesConfig: { [key: string]: ChartSeriesConfigItem } = {
    [trust ? "totalValue" : "value"]: {
      visible: true,
      valueFormatter: (v) =>
        shortValueFormatter(v, {
          valueUnit: valueUnit ?? dimension.unit,
        }),
    },
  };

  // if a `sort` is not provided, the default sorting method will be used (value DESC)
  const sortedDataPoints = useMemo(() => {
    let dataPoint = "value";
    if (trust) {
      dataPoint = "totalValue";
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
  }, [data.dataPoints, sort, trust]);

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">{children}</div>
        {chartMode == ChartModeChart && (
          <div className="govuk-grid-column-one-third">
            <button
              className="govuk-button govuk-button--secondary"
              data-module="govuk-button"
              data-prevent-double-click="true"
              onClick={() => ref.current?.download()}
              disabled={imageLoading}
              aria-disabled={imageLoading}
            >
              Save <span className="govuk-visually-hidden">{chartName}</span> as
              image
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
        message="No financial return data available"
      />
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-full">
          {sortedDataPoints.length > 0 ? (
            <>
              {chartMode == ChartModeChart && (
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
                        href={(id) => `/${trust ? "trust" : "school"}/${id}`}
                        establishmentKeyResolver={(name) => {
                          if (trust) {
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
                      trust ? (
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
                  chartMode == ChartModeTable ? "" : "govuk-visually-hidden"
                }
              >
                <TableChart
                  tableHeadings={data.tableHeadings}
                  data={sortedDataPoints}
                  preventFocus={chartMode !== ChartModeTable}
                  valueUnit={valueUnit ?? dimension.unit}
                  trust={trust}
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
