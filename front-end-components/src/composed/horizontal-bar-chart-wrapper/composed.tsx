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
import { TrustDataTooltip } from "src/components/charts/trust-data-tooltip";
import { CartesianTickItem } from "recharts/types/util/types";
import { TooltipProps } from "recharts";
import {
  NameType,
  Payload,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { SchoolExpenditure } from "src/services";
import { ShareContent } from "src/components/share-content";

export function HorizontalBarChartWrapper<
  TData extends SchoolChartData | TrustChartData,
>(props: HorizontalBarChartWrapperProps<TData>) {
  const { chartTitle, children, data, sort, trust, valueUnit } = props;
  const { chartMode } = useChartModeContext();
  const dimension = useContext(ChartDimensionContext);
  const selectedEstabishment = useContext(SelectedEstablishmentContext);
  const chartRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();
  const [imageCopied, setImageCopied] = useState<boolean>();
  const [tickFocused, setTickFocused] = useState<Record<string, boolean>>({});
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
    let dataPoint = "value" as keyof (SchoolChartData | TrustChartData);
    if (trust) {
      dataPoint = "totalValue" as keyof (SchoolChartData | TrustChartData);
    }

    return data.dataPoints
      .map((d) =>
        isFinite(d[dataPoint] as number) && !isNaN(d[dataPoint] as number)
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
      <SchoolDataTooltip {...props} {...payloadProps} />
    );
  };

  const getSpecialItemFlags = (key: string) => {
    const flags: SpecialItemFlag[] = [];
    if (partYearKeys.indexOf(key) > -1) {
      flags.push("partYear");
    }

    return flags;
  };

  const handleTickFocused = (key: string, focused: boolean) => {
    setTickFocused(Object.assign(tickFocused, { [key]: focused }));
  };

  const handleImageCopied = () => {
    setImageCopied(true);
    setTimeout(() => {
      setImageCopied(false);
    }, 2000);
  };

  const hasData = sortedDataPoints.length > 0;

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">{children}</div>
        {chartMode == ChartModeChart && (
          <div className="govuk-grid-column-one-third">
            <ShareContent
              copied={imageCopied}
              disabled={imageLoading || !hasData}
              onCopyClick={() => chartRef.current?.download("copy")}
              onSaveClick={() => chartRef.current?.download("save")}
              copyEventId="copy-chart-as-image"
              saveEventId="save-chart-as-image"
              showCopy
              showSave
              title={chartTitle}
            />
          </div>
        )}
      </div>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-full">
          {hasData ? (
            <>
              {chartMode == ChartModeChart && (
                <HorizontalBarChart
                  barCategoryGap={3}
                  chartTitle={chartTitle}
                  data={sortedDataPoints}
                  highlightActive
                  highlightedItemKeys={
                    selectedEstabishment ? [selectedEstabishment] : undefined
                  }
                  specialItemKeys={{ partYear: partYearKeys }}
                  keyField={keyField}
                  onImageCopied={handleImageCopied}
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
                        onFocused={handleTickFocused}
                      />
                    );
                  }}
                  tooltip={(p) =>
                    Object.values(tickFocused).some((t) => t)
                      ? null
                      : renderTooltip(p)
                  }
                  valueFormatter={shortValueFormatter}
                  valueLabel={dimension.label}
                  valueUnit={valueUnit ?? dimension.unit}
                  trust={trust}
                />
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
            <Loading />
          )}
        </div>
      </div>
    </>
  );
}
