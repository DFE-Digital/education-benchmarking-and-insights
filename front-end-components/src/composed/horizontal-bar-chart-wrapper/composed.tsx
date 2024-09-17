import { useContext, useMemo, useRef, useState } from "react";
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
import {
  ChartHandler,
  ChartSeriesConfigItem,
  SpecialItemFlag,
} from "src/components/charts";
import { HorizontalBarChartWrapperProps } from "src/composed/horizontal-bar-chart-wrapper";
import { ChartModeChart, ChartModeTable } from "src/components";
import {
  chartSeriesComparer,
  shortValueFormatter,
} from "src/components/charts/utils";
import { EstablishmentTick } from "src/components/charts/establishment-tick";
import { SchoolDataTooltip } from "src/components/charts/school-data-tooltip";
import { WarningBanner } from "src/components/warning-banner";
import { ErrorBanner } from "src/components/error-banner";
import { TrustDataTooltip } from "src/components/charts/trust-data-tooltip";
import { CartesianTickItem } from "recharts/types/util/types";
import { TooltipProps } from "recharts";
import {
  NameType,
  Payload,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { SchoolExpenditure } from "src/services";

export function HorizontalBarChartWrapper<
  TData extends SchoolChartData | TrustChartData,
>(props: HorizontalBarChartWrapperProps<TData>) {
  const { chartName, children, data, sort, trust, valueUnit } = props;
  const { chartMode } = useChartModeContext();
  const dimension = useContext(ChartDimensionContext);
  const selectedEstabishment = useContext(SelectedEstablishmentContext);
  const { hasIncompleteData, hasNoData } = useContext(HasIncompleteDataContext);
  const chartRef = useRef<ChartHandler>(null);
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

  const partYearKeys = useMemo(() => {
    if (trust) {
      return [];
    }

    return data.dataPoints
      .filter(
        (d) =>
          (d as SchoolExpenditure).periodCoveredByReturn !== undefined &&
          (d as SchoolExpenditure).periodCoveredByReturn < 12
      )
      .map((d) => (d as SchoolChartData).urn);
  }, [data.dataPoints, trust]);

  const getEstablishmentKey = (name: string) => {
    if (trust) {
      return (data.dataPoints as TrustChartData[]).find(
        (d) => d.trustName === name
      )?.companyNumber;
    }

    return (data.dataPoints as SchoolChartData[]).find(
      (d) => d.schoolName === name
    )?.urn;
  };

  const renderTooltip = (
    props: TooltipProps<ValueType, NameType>,
    tick?: { payload: CartesianTickItem }
  ) => {
    const payloadProps: {
      payload?: Payload<ValueType, NameType>[] | undefined;
    } = {};

    // if tick is valid then resolve payload from sorted data set based on its index
    if (tick !== undefined) {
      if (tick.payload.index == undefined) {
        return null;
      }

      const payload = sortedDataPoints[tick.payload.index];
      if (!payload) {
        return null;
      }

      payloadProps.payload = [{ payload }];
    }

    return trust ? (
      <TrustDataTooltip
        {...props}
        {...payloadProps}
        valueUnit={valueUnit ?? dimension.unit}
      />
    ) : (
      <SchoolDataTooltip
        {...props}
        {...payloadProps}
        specialItemFlags={getSpecialItemFlags}
      />
    );
  };

  const getSpecialItemFlags = (key: string) => {
    const flags: SpecialItemFlag[] = [];
    if (partYearKeys.indexOf(key) > -1) {
      flags.push("partYear");
    }

    return flags;
  };

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
              onClick={() => chartRef.current?.download()}
              disabled={imageLoading}
              aria-disabled={imageLoading}
              data-custom-event-id="save-chart-as-image"
              data-custom-event-chart-name={chartName}
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
                    barCategoryGap={3}
                    chartName={chartName}
                    data={sortedDataPoints}
                    highlightActive
                    highlightedItemKeys={
                      selectedEstabishment ? [selectedEstabishment] : undefined
                    }
                    specialItemKeys={{ partYear: partYearKeys }}
                    keyField={keyField}
                    onImageLoading={setImageLoading}
                    labels
                    margin={20}
                    ref={chartRef}
                    seriesConfig={seriesConfig as object}
                    seriesLabelField={seriesLabelField}
                    tickWidth={400}
                    tick={(t) => {
                      return (
                        <EstablishmentTick
                          {...t}
                          highlightedItemKey={selectedEstabishment}
                          linkToEstablishment
                          href={(id) => `/${trust ? "trust" : "school"}/${id}`}
                          establishmentKeyResolver={getEstablishmentKey}
                          tooltip={(p) => renderTooltip(p, t)}
                          specialItemFlags={getSpecialItemFlags}
                        />
                      );
                    }}
                    tooltip={(p) => renderTooltip(p)}
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
